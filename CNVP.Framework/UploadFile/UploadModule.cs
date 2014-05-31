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
		/// 用户语言，用于将来多国语言版的扩展
		/// </summary>
		public static string language = string.Empty;
		bool _showLoad = false;
		public UploadModule()
		{
		}
		/// <summary>
		/// 绑定到HttpApplication
		/// </summary>
		/// <param name="application"></param>
		public void Init(HttpApplication application)
		{
			application.BeginRequest += new EventHandler(this.Application_BeginRequest);
			application.EndRequest += new EventHandler(this.Application_EndRequest);
			application.Error += new EventHandler(this.Application_Error);
		}
		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
		}
		/// <summary>
		/// 请求开始
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
			int bytesRead = 0; // 已读数据大小
			int read; // 当前读取的块的大小
			int count = 8192; // 分块大小
			byte[] buffer; // 保存所有上传的数据
			string uploadId; // 唯一标志当前上传的ID
			Progress progress; // 记录当前上传的进度信息

			if (request != null)
			{
				// 返回 HTTP 请求正文已被读取的部分。
				byte[] tempBuff = request.GetPreloadedEntityBody();
				// 如果是附件上传
				if (
					tempBuff != null 
					&& IsUploadRequest(app.Request)
					)
				{
					// 获取上传大小
					long length = long.Parse(request.GetKnownRequestHeader(HttpWorkerRequest.HeaderContentLength));
					// 当前上传的ID，用来唯一标志当前的上传
					// 用此UploadID，可以通过其他页面获取当前上传的进度
					uploadId = app.Context.Request.QueryString["UploadID"];
					// 开始记录当前上传状态
					progress = new Progress(length, uploadId);
					progress.SetState(UploadState.ReceivingData);
					buffer = new byte[length];
					count = tempBuff.Length; // 分块大小
					// 将已上传数据复制过去
					Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, count);
					// 开始记录已上传大小
					bytesRead = tempBuff.Length;
					progress.SetBytesRead(bytesRead);
					SetProgress(uploadId, progress, app.Application);
					// 循环分块读取，直到所有数据读取结束
					while (request.IsClientConnected() &&
						!request.IsEntireEntityBodyIsPreloaded() &&
						bytesRead < length
						)
					{
						// 如果最后一块大小小于分块大小，则重新分块
						if (bytesRead + count > length)
						{
							count = (int)(length - bytesRead);
							tempBuff = new byte[count];
						}
						// 分块读取
						read = request.ReadEntityBody(tempBuff, count);
						// 复制已读数据块
						Buffer.BlockCopy(tempBuff, 0, buffer, bytesRead, read);
						// 记录已上传大小
						bytesRead += read;
						progress.SetBytesRead(bytesRead);
						SetProgress(uploadId, progress, app.Application);
					}
					if (
						request.IsClientConnected() &&
						!request.IsEntireEntityBodyIsPreloaded()
						)
					{
						// 传入已上传完的数据
						InjectTextParts(request, buffer);
						// 表示上传已结束
						progress.SetBytesRead(bytesRead);
						progress.SetState(UploadState.Complete);
						SetProgress(uploadId, progress, app.Application);
					}
				}
			}
		}
		/// <summary>
		/// 结束请求
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
		/// 错误处理
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
		/// 设置进度信息的状态
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
		/// 设置当前上传的进度信息
		/// 以UploadID为标识保存到在Application中
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
		/// 从Application中移出进度信息
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
		/// 根据UploadID取上传进度信息
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
		/// 已上传的数据
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
		/// 是否为附件上传
		/// 判断的根据是ContentType中有无multipart/form-data
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