
using System.Windows.Forms;

namespace ComfortApp
{
    partial class Register
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtExpire = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_role = new System.Windows.Forms.ComboBox();
            this.txtLic = new System.Windows.Forms.TextBox();
            this.btn_getLic = new System.Windows.Forms.Button();
            this.lbmsg = new System.Windows.Forms.Label();
            this.btn_DelLic = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_createLic = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Enabled = false;
            this.txtMachineCode.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMachineCode.Location = new System.Drawing.Point(125, 26);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(619, 21);
            this.txtMachineCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "过期时间：";
            // 
            // dtExpire
            // 
            this.dtExpire.Location = new System.Drawing.Point(125, 69);
            this.dtExpire.Name = "dtExpire";
            this.dtExpire.Size = new System.Drawing.Size(146, 22);
            this.dtExpire.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(332, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "权限角色：";
            // 
            // cmb_role
            // 
            this.cmb_role.FormattingEnabled = true;
            this.cmb_role.Items.AddRange(new object[] {
            "试用版",
            "1年会员",
            "终身免费会员"});
            this.cmb_role.Location = new System.Drawing.Point(403, 72);
            this.cmb_role.Name = "cmb_role";
            this.cmb_role.Size = new System.Drawing.Size(147, 20);
            this.cmb_role.TabIndex = 7;
            // 
            // txtLic
            // 
            this.txtLic.Location = new System.Drawing.Point(125, 155);
            this.txtLic.Multiline = true;
            this.txtLic.Name = "txtLic";
            this.txtLic.Size = new System.Drawing.Size(618, 158);
            this.txtLic.TabIndex = 8;
            // 
            // btn_getLic
            // 
            this.btn_getLic.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_getLic.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_getLic.Location = new System.Drawing.Point(13, 155);
            this.btn_getLic.Name = "btn_getLic";
            this.btn_getLic.Size = new System.Drawing.Size(106, 35);
            this.btn_getLic.TabIndex = 9;
            this.btn_getLic.Text = "获取当前License";
            this.btn_getLic.UseVisualStyleBackColor = false;
            this.btn_getLic.Click += new System.EventHandler(this.btn_getLic_Click);
            // 
            // lbmsg
            // 
            this.lbmsg.AutoSize = true;
            this.lbmsg.ForeColor = System.Drawing.Color.Red;
            this.lbmsg.Location = new System.Drawing.Point(123, 316);
            this.lbmsg.Name = "lbmsg";
            this.lbmsg.Size = new System.Drawing.Size(0, 12);
            this.lbmsg.TabIndex = 11;
            // 
            // btn_DelLic
            // 
            this.btn_DelLic.BackColor = System.Drawing.Color.YellowGreen;
            this.btn_DelLic.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DelLic.Location = new System.Drawing.Point(13, 215);
            this.btn_DelLic.Name = "btn_DelLic";
            this.btn_DelLic.Size = new System.Drawing.Size(106, 35);
            this.btn_DelLic.TabIndex = 12;
            this.btn_DelLic.Text = "删除license";
            this.btn_DelLic.UseVisualStyleBackColor = false;
            this.btn_DelLic.Click += new System.EventHandler(this.btn_DelLic_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(46, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "本機機器碼：";
            // 
            // btn_createLic
            // 
            this.btn_createLic.BackColor = System.Drawing.Color.Gold;
            this.btn_createLic.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_createLic.Location = new System.Drawing.Point(314, 106);
            this.btn_createLic.Name = "btn_createLic";
            this.btn_createLic.Size = new System.Drawing.Size(106, 35);
            this.btn_createLic.TabIndex = 2;
            this.btn_createLic.Text = "授權License";
            this.btn_createLic.UseVisualStyleBackColor = false;
            this.btn_createLic.Click += new System.EventHandler(this.btn_createLic_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(125, 114);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(146, 22);
            this.txtPwd.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(53, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "授权密码：";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 348);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_DelLic);
            this.Controls.Add(this.lbmsg);
            this.Controls.Add(this.btn_getLic);
            this.Controls.Add(this.txtLic);
            this.Controls.Add(this.cmb_role);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtExpire);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_createLic);
            this.Controls.Add(this.txtMachineCode);
            this.Name = "Register";
            this.Text = "License管理实验";
            this.Load += new System.EventHandler(this.Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private TextBox txtMachineCode;

		private Label label1;

		private DateTimePicker dtExpire;

		private Label label2;

		private ComboBox cmb_role;

		private TextBox txtLic;

		private Button btn_getLic;

		private Label lbmsg;

		private Button btn_DelLic;

		private Label label3;
        #endregion

        private Button btn_createLic;
        private TextBox txtPwd;
        private Label label4;
    }
}

