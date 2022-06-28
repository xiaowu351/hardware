using License.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LicenseApp
{
    public partial class Regist : Form
    {
        public Regist()
        {
            InitializeComponent();
        }
		private void Regist_Load(object sender, EventArgs e)
		{
			if (LicenseManage.ApplicationLicense == null)
			{
				LicenseManage.GetLicense();
			}
			txtMachineCode.Text = LicenseManage.GetMachineCode();
			label5.Text = "当前为" + LicenseManage.RoleTypeToString();
			if (LicenseManage.ApplicationLicense.ExpireTime > DateTime.Now)
			{
				Label label = label5;
				object obj = label.Text;
				label.Text = string.Concat(obj, "，", Math.Ceiling((LicenseManage.ApplicationLicense.ExpireTime - DateTime.Now).TotalDays), "天后到期！");
			}
			else if (LicenseManage.ApplicationLicense.CustomRole != RoleType.Free)
			{
				label5.Text += "，已过期！";
			}
		}

		private void btnRegist_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtLicense.Text))
			{
				if (LicenseManage.VerifyLicense(txtLicense.Text, bSave: true))
				{
					MessageBox.Show("注册成功。请重新打开程序！", "提示");
				}
				else
				{
					MessageBox.Show("License无效", "提示");
				}
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
