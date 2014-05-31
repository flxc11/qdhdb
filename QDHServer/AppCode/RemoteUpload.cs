using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;

namespace QDHServer.AppCode
{
    public class RemoteUpload
    {
        /// <summary>
        /// 文件名
        /// </summary>
        private string FileName;
        public string FileName1
        {
            get { return FileName; }
            set { FileName = value; }
        }

        /// <summary>
        /// 远程服务器地址
        /// </summary>
        private string UrlString;
        public string UrlString1
        {
            get { return UrlString; }
            set { UrlString = value; }
        }

        /// <summary>
        /// 新文件名
        /// </summary>
        private string NewFileName;
        public string NewFileName1
        {
            get { return NewFileName; }
            set { NewFileName = value; }
        }

        /// <summary>
        /// 文件数据
        /// </summary>
        private byte[] FileData;
        public byte[] FileData1
        {
            get { return FileData; }
            set { FileData = value; }
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="fileData">文件数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="urlString">服务器地址</param>
        public RemoteUpload(byte[] fileData, string fileName, string urlString)
        {
            this.FileData = fileData;
            this.FileName = fileName;
            this.UrlString = urlString.EndsWith("/") ? urlString : urlString + "/";
            //string newFileName = DateTime.Now.ToString("yyMMddhhmmss") +
            //  DateTime.Now.Millisecond.ToString() + Path.GetExtension(this.FileName);
            // this.UrlString = this.UrlString + newFileName;
            int position = fileName.LastIndexOf("/");
            string newFileName = FileName.Substring(position + 1);
            this.UrlString = this.UrlString + newFileName;
        }

        /// <summary>
        /// 供子类实现的虚方法，文件上传
        /// </summary>
        /// <returns></returns>
        public virtual bool UploadFile()
        {
            return true;
        }
    }
}