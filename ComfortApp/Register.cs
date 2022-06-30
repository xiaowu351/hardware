using ComfortApp.Common;
using License.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComfortApp
{
    public partial class Register : ABathForm
    {
        public Register()
        {
            InitializeComponent();
			LicenseManage.Init(LicenseStorageMode.Regedit, ConstString.LicenseSavePath, ConstString.LicenseEncodekey);
		}

		private void btn_createLic_Click(object sender, EventArgs e)
		{
			lbmsg.Text = "";
			if (string.IsNullOrEmpty(txtMachineCode.Text) || string.IsNullOrEmpty(dtExpire.Text) || string.IsNullOrEmpty(cmb_role.Text))
			{
				MessageBox.Show("请先生成机器码、选择过期时间、和角色");
				return;
			}
            if (string.IsNullOrWhiteSpace(txtPwd.Text))
            {
				MessageBox.Show("请先输入授权密码");
				return;
			}
			if (txtPwd.Text != ConstString.LicensePwd)
			{
				MessageBox.Show("授权密码不正确，请联系管理员！");
				return;
			}
			LicenseModel licenseModel = new LicenseModel();
			licenseModel.CustomMachineCode = txtMachineCode.Text;
			licenseModel.ExpireTime = dtExpire.Value;
			licenseModel.LastUseTime = DateTime.Now;
			LicenseModel lm = licenseModel;
			if (cmb_role.Text == "试用版")
			{
				lm.CustomRole = RoleType.Trial;
			}
			else if (cmb_role.Text == "1年会员")
			{
				lm.CustomRole = RoleType.Expiration;
				lm.ExpireTime = DateTime.Now.AddYears(1);
			}
			else if (cmb_role.Text == "终身免费会员")
			{
				lm.CustomRole = RoleType.Free;
			}
			var licenseString = LicenseManage.CreateLicenseString(lm);
			LicenseManage.VerifyLicense(licenseString, true);
			PrintLicense();
			lbmsg.Text = "授权License成功！";
		}

		private void btn_getLic_Click(object sender, EventArgs e)
		{
			lbmsg.Text = "";
			var licenseString = LicenseManage.CreateLicenseString(LicenseManage.GetLicense());
			if (LicenseManage.VerifyLicense(licenseString, bSave: false))
			{
				lbmsg.Text = "成功，license可用！";
				PrintLicense();
			}
			else
			{
				lbmsg.Text = "失败，license已过期";
			}
		}

		private void btn_VerifyLic_Click(object sender, EventArgs e)
		{
			//lbmsg.Text = "";
			//if (txtLic.Text.Trim() == "")
			//{
			//	MessageBox.Show("请先在右边框中输入License");
			//}
			//else if (LicenseManage.VerifyLicense(txtLic.Text, bSave: false))
			//{
			//	lbmsg.Text = "成功！";
			//	PrintLicense();
			//}
			//else
			//{
			//	lbmsg.Text = "验证失败，license已过期";
			//}
		}

		private void btn_DelLic_Click(object sender, EventArgs e)
		{
			lbmsg.Text = "";
			LicenseManage.DeleteLicense();
			lbmsg.Text = "删除成功！";
			txtLic.Text = "";
		}

        private void Register_Load(object sender, EventArgs e)
        {
			btn_DelLic.Visible = false;
			txtMachineCode.Text = LicenseManage.GetMachineCode();
		}

		void PrintLicense()
        {
			LicenseManage.GetLicense();
			txtLic.Text = "机器码：" + LicenseManage.ApplicationLicense.CustomMachineCode + "\r\n";
			TextBox textBox = txtLic;
			textBox.Text = textBox.Text + "过期时间：" + LicenseManage.ApplicationLicense.ExpireTime.ToString() + "\r\n";
			TextBox textBox2 = txtLic;
			textBox2.Text = textBox2.Text + "最后使用时间：" + LicenseManage.ApplicationLicense.LastUseTime.ToString() + "\r\n";
			TextBox textBox3 = txtLic;
			textBox3.Text = textBox3.Text + "角色类型：" + LicenseManage.ApplicationLicense.CustomRole.ToString() + "\r\n";
		}
         
    }
}
