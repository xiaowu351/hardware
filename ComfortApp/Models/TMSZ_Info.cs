using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortApp.Models
{
    public  class TMSZ_Info
    {
        /// <summary>
        /// 端口
        /// </summary>
        public int dk { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public int wd { get; set; }
        /// <summary>
        /// 速度
        /// </summary>
        public int sd { get; set; }
        /// <summary>
        /// 左边界
        /// </summary>
        public int le { get; set; }
    }
}
