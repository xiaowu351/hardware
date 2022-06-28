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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
			LicenseManage.Init(LicenseStorageMode.File, "license.lic", "小y密;1wif.");
		}

		private void btn_GetMachineCode_Click(object sender, EventArgs e)
		{
			txtMachineCode.Text = LicenseManage.GetMachineCode();
			txtLic.Text = txtMachineCode.Text;
		}

		private void btn_createLic_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txtMachineCode.Text) || string.IsNullOrEmpty(dtExpire.Text) || string.IsNullOrEmpty(cmb_role.Text))
			{
				MessageBox.Show("请先生成机器码、选择过期时间、和角色");
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
			txtLic.Text = LicenseManage.CreateLicenseString(lm);
		}

		private void btn_getLic_Click(object sender, EventArgs e)
		{
			lbmsg.Text = "";
			txtLic.Text = LicenseManage.CreateLicenseString(LicenseManage.GetLicense());
			lbmsg.Text = "获取成功！";
		}

		private void btn_VerifyLic_Click(object sender, EventArgs e)
		{
			lbmsg.Text = "";
			if (txtLic.Text.Trim() == "")
			{
				MessageBox.Show("请先在右边框中输入License");
			}
			else if (LicenseManage.VerifyLicense(txtLic.Text, bSave: false))
			{
				lbmsg.Text = "成功！";
				txtLic.Text = "机器码：" + LicenseManage.ApplicationLicense.CustomMachineCode + "\r\n";
				TextBox textBox = txtLic;
				textBox.Text = textBox.Text + "过期时间：" + LicenseManage.ApplicationLicense.ExpireTime.ToString() + "\r\n";
				TextBox textBox2 = txtLic;
				textBox2.Text = textBox2.Text + "最后使用时间：" + LicenseManage.ApplicationLicense.LastUseTime.ToString() + "\r\n";
				TextBox textBox3 = txtLic;
				textBox3.Text = textBox3.Text + "角色类型：" + LicenseManage.ApplicationLicense.CustomRole.ToString() + "\r\n";
			}
			else
			{
				lbmsg.Text = "验证失败，license已过期";
			}
		}

		private void btn_DelLic_Click(object sender, EventArgs e)
		{
			lbmsg.Text = "";
			LicenseManage.DeleteLicense();
			lbmsg.Text = "删除成功！";
			txtLic.Text = "";
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Regist r = new Regist();
			r.Show();
		}
	}
}
