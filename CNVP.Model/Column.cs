using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CNVP.Model
{
    public class Column
    {
        /// <summary>
        /// 栏目编号
        /// </summary>
        public string ColumnNumber { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public string ColumnType { get; set; }
        /// <summary>
        /// 栏目排序
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 栏目备注
        /// </summary>
        public string Bz { get; set; }
    }
}