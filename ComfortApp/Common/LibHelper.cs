using ComfortApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortApp.Common
{
    public static class LibHelper
    {
        public static TMSZ_Info ReadXTSZ(int dbIndex)
        {
            TMSZ_Info tmsz_info = null;
            using (var reader = AccessDbHelper.GetOleDbDataReader(dbIndex,"select * from tmsz"))
            {
                if (reader.Read())
                {
                    tmsz_info = new TMSZ_Info();
                    tmsz_info.dk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.dk))));
                    tmsz_info.wd = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.wd))));
                    tmsz_info.sd = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.sd))));
                    tmsz_info.le = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.le))));
                }


            }
            return tmsz_info;
        }
    }
}
