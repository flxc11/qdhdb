using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNVP.Model
{
    public class NewsSourcesModel
    {
        /// <summary>
        /// ID
        /// </summary>
        public int SourceID { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件后缀名
        /// </summary>
        public string FileExtension { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        public string PostTime { get; set; }
        /// <summary>
        /// 上传人ID
        /// </summary>
        public int AdminID { get; set; }
    }
}
