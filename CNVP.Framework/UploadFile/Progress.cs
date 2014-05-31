using System;

namespace CNVP.Framework.UploadFile
{
	/// <summary>
	/// 上传状态
	/// </summary>
	public enum UploadState
	{
		/// <summary>
		/// 正在接收数据
		/// </summary>
		ReceivingData,
		/// <summary>
		/// 已完成
		/// </summary>
		Complete,
		/// <summary>
		/// 上传错误.
		/// </summary>
		Error
	}

	/// <summary>
	/// 上传进度信息
	/// </summary>
	public class Progress
	{
		long contentLength = 0;
		long bytesRead;
		DateTime start;
		string uploadId = "";
		UploadState state;


		/// <summary>
		/// 初始化
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
		/// 设置上传数
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
		/// 设置状态
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
		/// 总大小
		/// </summary>
		public long ContentLength
		{
			get
			{
				return contentLength;
			}
		}

		/// <summary>
		/// 已上传大小
		/// </summary>
		public long BytesRead
		{
			get
			{
				return bytesRead;
			}
		}
		
		/// <summary>
		/// 上传状态
		/// </summary>
		public UploadState State
		{
			get
			{
				return state;
			}
		}

		/// <summary>
		/// 上传开始时间
		/// </summary>
		public DateTime Start
		{
			get
			{
				return start;
			}
		}

		/// <summary>
		/// 唯一标志当前上传的UploadID
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