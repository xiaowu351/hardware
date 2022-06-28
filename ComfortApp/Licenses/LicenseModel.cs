using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace License.Models
{
    /// <summary>
    /// License信息
    /// </summary>
    [Serializable]
    public class LicenseModel
    {
        //客户机器唯一识别码，由客户端生成
        public string CustomMachineCode { get; set; }
        //最后使用时间
        public DateTime LastUseTime { get; set; }
        //过期时间expire
        public DateTime ExpireTime { get; set; }
        //权限类型（如可分为 0： 15天试用版  1：1年版  2：终身版）
        public RoleType CustomRole { get; set; }

        public bool IsExpire()
        {
            var now = DateTime.Now;
            if (ExpireTime > now && now >= LastUseTime)
            {
                LastUseTime = now; //更新最後使用時間
                return false;
            }
            else if (CustomRole!= RoleType.Free) {
                //过期
                return true;
            }
            return true;
        }

    }
    /// <summary>
    /// 几种角色类型
    /// </summary>
    [Serializable]
    public enum RoleType
    {
        /// <summary>
        /// 试用版
        /// </summary>
        Trial = 0,
        /// <summary>
        /// 有期限版
        /// </summary>
        Expiration = 1,
        /// <summary>
        /// 终身免费版
        /// </summary>
        Free = 2
    }


            public enum LicenseStorageMode
    {
        File,
        Regedit
    }
}
