using ComfortApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComfortApp.Common
{
    public enum ImageMode
    {
        No = 0,
        Yes = 1
    }
    public static class LibHelper
    {
        public static TMSZ_Info ReadXTSZ(int dbIndex)
        {
            TMSZ_Info tmsz_info = null;
            bool isAlter = false;
            using (var reader = AccessDbHelper.GetOleDbDataReader(dbIndex,"select * from tmsz"))
            {
                if (reader.Read())
                {
                    tmsz_info = new TMSZ_Info();
                    tmsz_info.dk = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.dk))));
                    tmsz_info.wd = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.wd))));
                    tmsz_info.sd = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.sd))));
                    tmsz_info.le = Convert.ToInt32(reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.le))));
                    if (reader.FieldCount<5)
                    {
                        isAlter = true;                        
                        tmsz_info.gap = 3;

                    }
                    else
                    {
                        var gap = reader.GetValue(reader.GetOrdinal(nameof(tmsz_info.gap)));
                        if (!(gap is DBNull))
                        {
                            tmsz_info.gap = Convert.ToInt32(gap);
                        }
                        else
                        {
                            tmsz_info.gap = 3;
                        }
                    }
                                      
                }


            }
            if (isAlter)
            {
                AccessDbHelper.ExecuteNonQuery(dbIndex, "alter table  tmsz add column gap integer");

            }
            return tmsz_info;
        }


        public static void FindTextBoxControl(Control ctrol)
        {
            foreach (Control ctl in ctrol.Controls)
            {
                if (ctl is TextBox txtbox)
                {
                    txtbox.KeyDown += Txtbox_KeyDown;
                }
                //if(ctl is Control)
                //{
                //    FindTextBoxControl(ctl);
                //}
            }
        }

        private static void Txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }
    }
}
