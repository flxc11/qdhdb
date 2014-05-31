using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNVP.Config
{
    public class UIConfig
    {
        /// <summary>
        /// 安装路径
        /// </summary>
        public static string InstallDir = "/";
        /// <summary>
        /// 管理路径
        /// </summary>
        public static string AdminDir = "admin";
        /// <summary>
        /// 会员路径
        /// </summary>
        public static string UsersDir = "Users";
        /// <summary>
        /// 管理员Cookies名称
        /// </summary>
        public static string AdminCookiesName = "CNVP_HD_Admin";
        /// <summary>
        /// 用户Cookies名称
        /// </summary>
        public static string UsersCookiesName = "CNVP_HD_Users";
        /// <summary>
        /// 自动缩略
        /// </summary>
        public static string AutoThumbnail = "0";
        /// <summary>
        /// 缩略方式
        /// </summary>
        public static string ThumbnailStyle = "w";
        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public static string ThumbnailX = "600";
        /// <summary>
        /// 缩略图高度
        /// </summary>
        public static string ThumbnailY = "300";
        /// <summary>
        /// 自动水印
        /// </summary>
        public static string AutoWatermark = "0";
        /// <summary>
        /// 水印图片
        /// </summary>
        public static string WatermarkSrc = "/Config/Watermark.png";
        /// <summary>
        /// 水印位置
        /// </summary>
        public static string WatermarkStatus = "9";
        /// <summary>
        /// 编辑器名称
        /// </summary>
        public static string EditorName = "FCKEditor";
    }
}