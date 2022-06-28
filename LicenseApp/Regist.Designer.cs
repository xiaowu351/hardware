
using System.Windows.Forms;

namespace LicenseApp
{
    partial class Regist
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.label1 = new System.Windows.Forms.Label();
			this.txtLicense = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMachineCode = new System.Windows.Forms.TextBox();
			this.btnRegist = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 157);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "输入License：";
			this.txtLicense.Location = new System.Drawing.Point(102, 157);
			this.txtLicense.Multiline = true;
			this.txtLicense.Name = "txtLicense";
			this.txtLicense.Size = new System.Drawing.Size(400, 146);
			this.txtLicense.TabIndex = 1;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(19, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "本地机器码：";
			this.txtMachineCode.Location = new System.Drawing.Point(102, 93);
			this.txtMachineCode.Name = "txtMachineCode";
			this.txtMachineCode.Size = new System.Drawing.Size(400, 21);
			this.txtMachineCode.TabIndex = 3;
			this.btnRegist.Location = new System.Drawing.Point(102, 321);
			this.btnRegist.Name = "btnRegist";
			this.btnRegist.Size = new System.Drawing.Size(75, 23);
			this.btnRegist.TabIndex = 4;
			this.btnRegist.Text = "注册";
			this.btnRegist.UseVisualStyleBackColor = true;
			this.btnRegist.Click += new System.EventHandler(btnRegist_Click);
			this.label3.AutoSize = true;
			this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label3.Location = new System.Drawing.Point(102, 121);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(191, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "(申请License时需要提供此机器码)";
			this.btnCancel.Location = new System.Drawing.Point(203, 321);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("微软雅黑", 15.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
			this.label5.ForeColor = System.Drawing.Color.Teal;
			this.label5.Location = new System.Drawing.Point(19, 14);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(290, 28);
			this.label5.TabIndex = 8;
			this.label5.Text = "当前为试用版，29天后到期！";
			this.panel1.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label5);
			this.panel1.Location = new System.Drawing.Point(21, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(481, 59);
			this.panel1.TabIndex = 9;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(516, 369);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.btnRegist);
			base.Controls.Add(this.txtMachineCode);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.txtLicense);
			base.Controls.Add(this.label1);
			base.Name = "Regist";
			this.Text = "注册";
			base.Load += new System.EventHandler(Regist_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
        private Label label1;

        private TextBox txtLicense;

        private Label label2;

        private TextBox txtMachineCode;

        private Button btnRegist;

        private Label label3;

        private Button btnCancel;

        private Label label5;

        private Panel panel1;
        #endregion
    }
}