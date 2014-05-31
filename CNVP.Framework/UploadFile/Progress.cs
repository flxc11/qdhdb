using System;

namespace CNVP.Framework.UploadFile
{
	/// <summary>
	/// �ϴ�״̬
	/// </summary>
	public enum UploadState
	{
		/// <summary>
		/// ���ڽ�������
		/// </summary>
		ReceivingData,
		/// <summary>
		/// �����
		/// </summary>
		Complete,
		/// <summary>
		/// �ϴ�����.
		/// </summary>
		Error
	}

	/// <summary>
	/// �ϴ�������Ϣ
	/// </summary>
	public class Progress
	{
		long contentLength = 0;
		long bytesRead;
		DateTime start;
		string uploadId = "";
		UploadState state;


		/// <summary>
		/// ��ʼ��
		/// </summary>
		/// <param name="contentLength"></param>
		/// <param name="uploadId"></param>
		public Progress(long contentLength, string uploadId)
		{
			this.contentLength = contentLength;
			start = DateTime.Now;
			this.uploadId = uploadId;
		}

		/// <summary>
		/// �����ϴ���
		/// </summary>
		/// <param name="bytesRead"></param>
		public void SetBytesRead(long bytesRead)
		{
			lock (this)
			{
				this.bytesRead = bytesRead;
			}
		}


		/// <summary>
		/// ����״̬
		/// </summary>
		/// <param name="state"></param>
		public void SetState(UploadState state)
		{
			lock (this)
			{
				this.state = state;
			}
		}

		/// <summary>
		/// �ܴ�С
		/// </summary>
		public long ContentLength
		{
			get
			{
				return contentLength;
			}
		}

		/// <summary>
		/// ���ϴ���С
		/// </summary>
		public long BytesRead
		{
			get
			{
				return bytesRead;
			}
		}
		
		/// <summary>
		/// �ϴ�״̬
		/// </summary>
		public UploadState State
		{
			get
			{
				return state;
			}
		}

		/// <summary>
		/// �ϴ���ʼʱ��
		/// </summary>
		public DateTime Start
		{
			get
			{
				return start;
			}
		}

		/// <summary>
		/// Ψһ��־��ǰ�ϴ���UploadID
		/// </summary>
		string UploadId
		{
			get
			{
				return uploadId;
			}
		}
	}
}