using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Reflection;

namespace CNVP.Framework.UploadFile
{
	public class UploadModule: IHttpModule
	{
		/// <summary>
		/// �û����ԣ����ڽ���������԰����չ
		/// </summary>
		public static string language = string.Empty;
		bool _showLoad = false;
		public UploadModule()
		{
		}
		/// <summary>
		/// �󶨵�HttpApplication
		/// </summary>
		/// <param name="application"></param>
		public void Init(HttpApplication application)
		{
			application.BeginRequest += new EventHandler(this.Application_BeginRequest);
			application.EndRequest += new EventHandler(this.Application_EndRequest);
			application.Error += new EventHandler(this.Application_Error);
		}
		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		public void Dispose()
		{
		}
		/// <summary>
		/// ����ʼ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Application_BeginRequest(Object sender, EventArgs e)
		{
			HttpApplication app = sender as HttpApplication;
			HttpWorkerRequest request = GetWorkerRequest(app.Context);
			Encoding encoding = app.Context.Request.ContentEncoding;
			try
			{
				language = app.Context.Request.UserLanguages[0];
			}
			catch
			{
				language = "zh-cn";
			}
			try
			{
				_showLoad = Convert.ToBoolean(app.Context.Request.QueryString["JCmsShowLoading"]);
			}
			catch
			{
				_showLoad = false;
			}
            if (_showLoad)
            {
                showLoad();
                return;
            }
			int bytesRead = 0; // �Ѷ����ݴ�С
			int read; // ��ǰ��ȡ�Ŀ�Ĵ�С
			int count = 8192; // �ֿ��С
			byte[] buffer; // ���������ϴ�������
			string uploadId; // Ψһ��־��ǰ�ϴ���ID
			Progress progress; // ��¼��ǰ�ϴ��Ľ�����Ϣ

			if (request != null)
			{
				// ���� HTTP ���������ѱ���ȡ�Ĳ��֡�
				byte[] tempBuff = request.GetPreloadedEntityBody();
				// ����Ǹ����ϴ�
				if (
					tempBuff != null 
					&& IsUploadRequest(app.Request)
					)
				{
					// ��ȡ�ϴ���С
					long length = long.Parse(request.GetKnownRequestHeader(HttpWorkerRequest.HeaderContentLength));
					// ��ǰ�ϴ���ID������Ψһ��־��ǰ���ϴ�
					// �ô�UploadID������ͨ������ҳ���ȡ��ǰ�ϴ��Ľ���
					uploadId = app.Context.Request.QueryString["UploadID"];
					// ��ʼ��¼��ǰ�ϴ�״̬
					progress = new Progress(length, uploadId);
					progress.SetState(UploadState.ReceivingData);
					buffer = new byte[length];
					count = tempBuff.Length; // �ֿ��С
					// �����ϴ����ݸ��ƹ�ȥ
					Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, count);
					// ��ʼ��¼���ϴ���С
					bytesRead = tempBuff.Length;
					progress.SetBytesRead(bytesRead);
					SetProgress(uploadId, progress, app.Application);
					// ѭ���ֿ��ȡ��ֱ���������ݶ�ȡ����
					while (request.IsClientConnected() &&
						!request.IsEntireEntityBodyIsPreloaded() &&
						bytesRead < length
						)
					{
						// ������һ���СС�ڷֿ��С�������·ֿ�
						if (bytesRead + count > length)
						{
							count = (int)(length - bytesRead);
							tempBuff = new byte[count];
						}
						// �ֿ��ȡ
						read = request.ReadEntityBody(tempBuff, count);
						// �����Ѷ����ݿ�
						Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, read);
						// ��¼���ϴ���С
						bytesRead += read;
						progress.SetBytesRead(bytesRead);
						SetProgress(uploadId, progress, app.Application);
					}
					if (
						request.IsClientConnected() &&
						!request.IsEntireEntityBodyIsPreloaded()
						)
					{
						// �������ϴ��������
						InjectTextParts(request, buffer);
						// ��ʾ�ϴ��ѽ���
						progress.SetBytesRead(bytesRead);
						progress.SetState(UploadState.Complete);
						SetProgress(uploadId, progress, app.Application);
					}
				}
			}
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Application_EndRequest(Object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            if (IsUploadRequest(app.Request))
            {
                SetUploadState(app, UploadState.Complete);
                RemoveFrom(app);
            }
        }
		/// <summary>
		/// ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Application_Error(Object sender, EventArgs e)
		{
			HttpApplication app = sender as HttpApplication;

			if (IsUploadRequest(app.Request))
			{
				SetUploadState(app, UploadState.Error);
			}

		}
		/// <summary>
		/// ���ý�����Ϣ��״̬
		/// </summary>
		/// <param name="app"></param>
		/// <param name="state"></param>
		void SetUploadState(HttpApplication app, UploadState state)
		{
			string uploadId = app.Request.QueryString["UploadID"];
			if (uploadId != null && uploadId.Length > 0)
			{
				Progress progress = GetProgress(uploadId, app.Application);
				if (progress != null)
				{
					progress.SetState(state);
					SetProgress(uploadId, progress, app.Application);
				}		
			}
		}
		/// <summary>
		/// ���õ�ǰ�ϴ��Ľ�����Ϣ
		/// ��UploadIDΪ��ʶ���浽��Application��
		/// </summary>
		/// <param name="uploadId"></param>
		/// <param name="progress"></param>
		/// <param name="application"></param>
		void SetProgress(string uploadId, Progress progress, HttpApplicationState application)
		{
			if (uploadId == null || uploadId == string.Empty || progress == null)
				return;
			application.Lock();
			application["OpenlabUpload_" + uploadId] = progress;
			application.UnLock(); 
		}
		/// <summary>
		/// ��Application���Ƴ�������Ϣ
		/// </summary>
		/// <param name="app"></param>
		void RemoveFrom(HttpApplication app)
		{
			string uploadId = app.Request.QueryString["UploadID"];
			HttpApplicationState application = app.Application;
			if (uploadId != null && uploadId.Length > 0)
			{
				application.Remove("OpenlabUpload_" + uploadId);
			}
		}
		/// <summary>
		/// ����UploadIDȡ�ϴ�������Ϣ
		/// </summary>
		/// <param name="uploadId"></param>
		/// <param name="application"></param>
		/// <returns></returns>
		public static Progress GetProgress(string uploadId, HttpApplicationState application)
		{
			Progress progress = application["OpenlabUpload_" + uploadId] as Progress;
			return progress;
		}
		HttpWorkerRequest GetWorkerRequest(HttpContext context)
		{
			IServiceProvider provider = (IServiceProvider)HttpContext.Current;
			return (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));
		}
		/// <summary>
		/// ���ϴ�������
		/// </summary>
		/// <param name="request"></param>
		/// <param name="textParts"></param>
		void InjectTextParts(HttpWorkerRequest request, byte[] textParts)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic; 
			Type type = request.GetType(); 
			while ((type != null) && (type.FullName != "System.Web.Hosting.ISAPIWorkerRequest"))
			{ 
				type = type.BaseType; 
			}
			if (type != null)
			{
				type.GetField("_contentAvailLength", bindingFlags).SetValue(request, textParts.Length); 
				type.GetField("_contentTotalLength", bindingFlags).SetValue(request, textParts.Length);
				type.GetField("_preloadedContent", bindingFlags).SetValue(request, textParts); 
				type.GetField("_preloadedContentRead", bindingFlags).SetValue(request, true);
			}
		}
		private static bool StringStartsWithAnotherIgnoreCase(string s1, string s2)
		{
			return (string.Compare(s1, 0, s2, 0, s2.Length, true, CultureInfo.InvariantCulture) == 0);
		}
		/// <summary>
		/// �Ƿ�Ϊ�����ϴ�
		/// �жϵĸ�����ContentType������multipart/form-data
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		bool IsUploadRequest(HttpRequest request)
		{
			return StringStartsWithAnotherIgnoreCase(request.ContentType, "multipart/form-data");
		}
		private void showLoad()
		{
			string uploadId = HttpContext.Current.Request.QueryString["UploadID"];
			string scriptText = "";
			string scriptUploading = "pb.setSize({0},{1},{2},{3},{4},{5});";
			
			string scriptClearTimer = "ClearTimer();";
			string scriptUploadComplete = "pb.UploadComplete();" + scriptClearTimer;
			string scriptUploadError = "pb.UploadError();";

			string length = "";
			string read = "";
			long _length = 0;
			long _read = 0;
			long _speed = 0;
			long _leftTime;

            CNVP.Framework.UploadFile.Progress progress = UploadModule.GetProgress(uploadId, HttpContext.Current.Application);
			if (progress != null)
			{
				if (progress.State == UploadState.ReceivingData)
				{
					_length = progress.ContentLength / 1024;
					length = _length.ToString();
					_read = progress.BytesRead / 1024;
					read = _read.ToString();
					_speed = (long)((_read)/(DateTime.Now - progress.Start).TotalSeconds) ;
					_leftTime  =  (long)(_length - _read) /_speed;
					scriptText = string.Format(scriptUploading, length, read,_speed,_leftTime/(60*60),_leftTime/(60),(_leftTime % 60));
				}
				else if(progress.State == UploadState.Complete)
				{
					scriptText = scriptUploadComplete;
				}
				else
				{
					scriptText = scriptUploadError;
				}
			}
			else
			{
				//scriptText = scriptUploadError;
			}
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.Write(scriptText);
			HttpContext.Current.Response.End();
		}
	}
}