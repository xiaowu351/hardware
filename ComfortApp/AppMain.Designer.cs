
namespace ComfortApp
{
    partial class AppMain
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.printFormControl1 = new ComfortApp.Controls.PrintFormControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.printFormControl2 = new ComfortApp.Controls.PrintFormControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(35, 20);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(884, 612);
            this.tabControl1.TabIndex = 100;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.printFormControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 56);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(876, 552);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "無圖片打印";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // printFormControl1
            // 
            this.printFormControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printFormControl1.Location = new System.Drawing.Point(3, 3);
            this.printFormControl1.Name = "printFormControl1";
            this.printFormControl1.Size = new System.Drawing.Size(870, 546);
            this.printFormControl1.TabIndex = 200;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.printFormControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 56);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(876, 552);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "有圖片打印";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // printFormControl2
            // 
            this.printFormControl2.BackColor = System.Drawing.Color.PaleTurquoise;
            this.printFormControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printFormControl2.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.printFormControl2.Location = new System.Drawing.Point(3, 3);
            this.printFormControl2.Name = "printFormControl2";
            this.printFormControl2.Size = new System.Drawing.Size(870, 546);
            this.printFormControl2.TabIndex = 300;
            // 
            // AppMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 612);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AppMain";
            this.Load += new System.EventHandler(this.AppMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Controls.PrintFormControl printFormControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private Controls.PrintFormControl printFormControl2;
    }
}