using License.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComfortApp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += (sender, e) =>{
                if(LicenseManage.ApplicationLicense is null)
                {
                    LicenseManage.GetLicense();
                }
                LicenseManage.VerifyLicense(LicenseManage.ApplicationLicense, true);
            };
            Application.Run(new AppMain());
        }
    }
}
