using ComfortApp.Common;
using ComfortApp.Models;
using EzioDll;
using License.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComfortApp.Controls
{
    public partial class PrintFormControl : UserControl
    {
        private TMSZ_Info _tmsz_info;
        private GodexPrinter Printer;

        private string _license_format = "當前為{0},{1}天後到期！";
       
        private string _tupian;
        private bool _isExpire;
        public AppMain AppMain
        {
            get
            {
                return FindForm() as AppMain;
            }
        }
        public PrintFormControl()
        {
            InitializeComponent();
            RegisterEvent();
            LicenseManage.Init(LicenseStorageMode.Regedit, ConstString.LicenseSavePath, ConstString.LicenseEncodekey);
            _isExpire = false;
        }

        public void FirstFocus()
        {
            txtbihao.Focus();
        }

        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(AppMain?.TabSelectedIndex.ToString());
        //}

        private void RegisterEvent()
        {
            
            txtbihao.LostFocus += Txtbihao_LostFocus;
            txtbihao.KeyPress += Txtbihao_KeyPress;
            txtshuoming1.LostFocus += Txtshuoming1_LostFocus;
            
        }

        private void Txtshuoming1_LostFocus(object sender, EventArgs e)
        {
            if(sender is TextBox txt)
            {
                if(txt.Text.Trim() == "###")
                {
                    txt.Text = string.Empty;
                    using (var reg = new Register())
                    {
                        reg.ShowDialog();
                        register();
                    }
                }
            }
        }

        private void Txtbihao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void Txtbihao_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbihao.Text.Trim()))
            {
                clearInput();
                return;
            }
            register();
            if (_isExpire)
            {
                clearInput();
                return;
            }

            var bihao = txtbihao.Text.Trim().ToUpper();
            var tupianIndex = bihao.LastIndexOf("-");
            if (tupianIndex < 0)
            {
                MessageBox.Show("編號格式不正確!");
                return;
            }
            _tupian = bihao.Substring(0, tupianIndex).Trim();
            using (var reader = AccessDbHelper.GetOleDbDataReader(AppMain.TabSelectedIndex,$"select * from tiaom  where bihao='{bihao}'"))
            {
                if (reader.Read())
                {
                    txtnuber.Text = $"{ reader["nuber"]}".Trim();
                    txttiaoma.Text = $"{ reader["tiaoma"]}".Trim();
                    txtshuoming.Text = $"{ reader["shuming"]}".Trim();
                    txtshuoming1.Text = $"{ reader["shuming1"]}".Trim();
                    //txtdingdan.Text = $"{ reader["dingdan"]}";
                    if(AppMain.ImageMode == ImageMode.Yes)
                    {
                        pbImage.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"./images/{_tupian}.bmp");
                    }
                }
                else
                {
                    clearInput();
                }
            }

            if (string.IsNullOrWhiteSpace(txtPO.Text.Trim()))
            {
                txtPO.Focus();
            }
            else
            {
                if (cblock.Checked)
                {
                    txtNumber.Focus();
                }
            }            
            if(AppMain.ImageMode == ImageMode.Yes)
            {
                ConnectPrinter();
                Printer.Config.Setup(66, 10, 3, 0, 3, 0);
                Printer.Command.UploadImage_Int(pbImage.ImageLocation, _tupian, Image_Type.BMP);
                DisconnectPrinter();
            }
            this.btnPrint.Enabled = true;
        }

        void clearInput()
        {
            txttiaoma.Text = string.Empty;
            txtshuoming.Text = string.Empty;
            txtnuber.Text = string.Empty;
            txtshuoming1.Text = string.Empty;
            if (cblock.Checked == false)
            {
                txtPO.Text = string.Empty;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new TMSZ(AppMain.TabSelectedIndex).ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new CCFD(AppMain.ImageMode).ShowDialog();
        }
        private void PrintFormControl_Load(object sender, EventArgs e)
        {
            Printer = new GodexPrinter();
            _tupian = string.Empty;
            LibHelper.FindTextBoxControl(splitContainer2.Panel1);
            register();
        }
        

        private void ReadXTSZ()
        {
            
            _tmsz_info = LibHelper.ReadXTSZ(AppMain.TabSelectedIndex);
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            register();
            if (_isExpire)
            {
                MessageBox.Show("license已過期無法使用打印功能!");
                return;
            }
            ReadXTSZ();
            if (_tmsz_info == null)
            {
                MessageBox.Show("未找到系統設置信息，請先設置系統信息再試!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtbihao.Text.Trim()))
            {
                MessageBox.Show("請先輸入編號，再點打印!");
                txtbihao.Focus();
                return;
            }
            saveTosa();
            print();
            this.btnPrint.Enabled = false;
        }

        private void saveTosa()
        {

            var existObj = AccessDbHelper.ExecuteScalar(AppMain.TabSelectedIndex,$"select count(*) from sa where  nbr='{txtnuber.Text.Trim()}'");
            if (existObj is int && Convert.ToInt32(existObj) > 0)
            {
                //存在記錄，則忽略
                return;

            }

            //插入新記錄到sa 表中
            var insertSql = $"insert into sa(nbr,bh,tm,sm,sm1) values(" +
                $"'{txtnuber.Text.Trim()}'," +
                $"'{txtbihao.Text.Trim()}'," +
                $"'{txttiaoma.Text.Trim()}'," +
                $"'{txtshuoming.Text.Trim()}'," +
                $"'{txtshuoming1.Text.Trim()}')";

            AccessDbHelper.ExecuteNonQuery(AppMain.TabSelectedIndex, insertSql);


        }       
        //------------------------------------------------------------------------
        // Connect Printer
        //------------------------------------------------------------------------
        private void ConnectPrinter()
        {                        
                Printer.Open("Godex EZ-1100 Plus");
        }
        private void LabelSetup(int number)
        { 
            if(AppMain.ImageMode == ImageMode.Yes)
            {
                Printer.Config.Setup(66, 10, 3, 0, _tmsz_info.gap, 0); //'(紙張高度, 列印明暗度, 列印速度, 0:標籤紙 1:連續紙 2:黑線紙, 標籤間距, 黑線寬度)
                
            }
            else
            {
                Printer.Config.Setup(33, 10, 3, 0, _tmsz_info.gap, 0); //'(紙張高度, 列印明暗度, 列印速度, 0:標籤紙 1:連續紙 2:黑線紙, 標籤間距, 黑線寬度)
                

            }

            Printer.Config.LabelWidth(50);// 標籤寬度設定
            Printer.Config.EndSetting(15);//停歇点设定 
            Printer.Config.Dark(_tmsz_info.wd);//设定黑度(列印深浅)。 值愈大，印表头温度愈高
            Printer.Config.Speed(_tmsz_info.sd); //列印速度设定,实际列印速度请参考各机种规格书
            Printer.Config.LeftBorder(_tmsz_info.le);//设定标签左边界起印点
            Printer.Config.PageNo(number);//列印张数设定,设定一次要列印的张数x = 1 ~ 32767
            //Printer.Config.CopyNo((int)Num_Copy.Value);
        }
        //------------------------------------------------------------------------
        // Disconnect Printer
        //------------------------------------------------------------------------
        private void DisconnectPrinter()
        {
            Printer.Close();
        }
        private void print()
        {
            if (int.TryParse(txtNumber.Text.Trim(), out int number))
            {
                if (number > 32767)
                {
                    MessageBox.Show("打印數量超過了最大值32767");
                    return;
                }
            }
            else
            {
                return;
            }
            // 沒有juli 這個輸入框
            //var juli = txtjuli.Text.Trim();
            var tiaoma = txttiaoma.Text.Trim();
            ConnectPrinter();
            LabelSetup(number); 
            // Print Text
            Printer.Command.Start();

            //Printer.Command.DrawStraightLinecommand(8, 128, 252, 132); // 画黑色长方形
            if (AppMain.ImageMode == ImageMode.Yes)
            {

                printYes();
            }
            else
            {
                printNo();
            }

            Printer.Command.End();         
            DisconnectPrinter();

        }

        void printYes()
        {
            var existObj = AccessDbHelper.ExecuteScalar(AppMain.TabSelectedIndex, $"select COUNT(*) from tiaom  where bihao='{txtbihao.Text.Trim().ToUpper()}'");
            if (existObj is int && Convert.ToInt32(existObj) > 0)
            {
                //存在記錄，則打印圖片
                //_godexPrinter.sendcommand("y10,30," + _tupian); //呼叫图形档命令，将下载之图形列印在标签之选定位置
                Printer.Command.PrintImageByName(_tupian, 10, 30);
            }
            // //输出True Type字型文字:ecTextOut(x,y,b,c,d)
            // // x: (整数) 水平坐标. (dots)
            // // y: (整数)垂直坐标(dots)
            // //b: (整数)文字高度
            // //c: (字符串)字型名称，如细明体
            // //d: (字符串)数据字符串
            Printer.Command.PrintText(10, 245, 39, "ARIAL black", txtbihao.Text.Trim());
            if (txtbihao.Text.Trim().Length <= 17)
            {
                //画直线命令:La,x,y,x1,y1
                //参数 a = o, 覆盖线条位置下之内容
                //a = e, 将线条位置下之内容， 以反白方式呈现出
                //x = 左上角水平位置(单位： dots)
                //y = 左上角垂直位置(单位： dots)
                //x1 = 右下角水平位置(单位： dots)
                //y1 = 右下角垂直位置(单位： dots)
                //result = _godexPrinter.sendcommand("le,320,245,5,280");
                //Printer.Command.Send("le,320,245,5,280");
                Printer.Command.DrawStraightLinecommand(320, 245, 5, 280);
            }
            else
            { 
                //Printer.Command.Send("le,350,245,5,280");
                Printer.Command.DrawStraightLinecommand(350, 245, 5, 280);
            }
            //Bt,x,y,narrow,wide,height,rotation,readable,data - 条码命令
            Printer.Command.Barcodecommand(0, 297, 3, 5, 90, 0, 1, txttiaoma.Text.Trim());
            var shuoming1 = txtshuoming1.Text.Trim();
            if (string.IsNullOrWhiteSpace(shuoming1))
            {
                //result = _godexPrinter.ecTextOut(0, 415, 32, "ARIAL black", txtshuoming.Text.Trim());
                Printer.Command.PrintText(0, 415, 32, "ARIAL black", txtshuoming.Text.Trim());                  
            }
            else
            {
                Printer.Command.PrintText(0, 410, 30, "ARIAL black", txtshuoming.Text.Trim());
                Printer.Command.PrintText(0, 440, 30, "ARIAL black", shuoming1);   
            }
            if (string.IsNullOrWhiteSpace(txtPO.Text.Trim()) == false)
            {
                Printer.Command.PrintText(270, 440, 30, "ARIAL black", txtPO.Text.Trim());
            }
            Printer.Command.PrintText(0, 470, 30, "ARIAL black", "ALNO,INC.");
            Printer.Command.PrintText(205, 475, 22, "ARIAL black", "Made In China");
        }
        void printNo()
        {
            // //输出True Type字型文字:ecTextOut(x,y,b,c,d)
            // // x: (整数) 水平坐标. (dots)
            // // y: (整数)垂直坐标(dots)
            // //b: (整数)文字高度
            // //c: (字符串)字型名称，如细明体
            // //d: (字符串)数据字符串
            Printer.Command.PrintText(10, 15, 39, "ARIAL black", txtbihao.Text.Trim());
            if (txtbihao.Text.Trim().Length <= 17)
            {
                //画直线命令:La,x,y,x1,y1
                //参数 a = o, 覆盖线条位置下之内容
                //a = e, 将线条位置下之内容， 以反白方式呈现出
                //x = 左上角水平位置(单位： dots)
                //y = 左上角垂直位置(单位： dots)
                //x1 = 右下角水平位置(单位： dots)
                //y1 = 右下角垂直位置(单位： dots)               
                //Printer.Command.Send("le,320,245,5,280");
                Printer.Command.DrawStraightLinecommand( 5, 50, 320, 15); // 画黑色长方形
                //Printer.Command.DrawStraightLinecommand(320, 15, 5, 50); // 画黑色长方形
            }
            else
            {
                Printer.Command.DrawStraightLinecommand(5, 50,350, 15);
                //Printer.Command.DrawStraightLinecommand(350,15, 5, 50);
            }
            //Bt,x,y,narrow,wide,height,rotation,readable,data - 条码命令
            Printer.Command.Barcodecommand(0, 60, 3, 5, 90, 0, 1, txttiaoma.Text.Trim());
            var shuoming1 = txtshuoming1.Text.Trim();
            if (string.IsNullOrWhiteSpace(shuoming1))
            { 
                Printer.Command.PrintText(0, 175, 32, "ARIAL black", txtshuoming.Text.Trim()); 
            }
            else
            {
                Printer.Command.PrintText(0, 170, 30, "ARIAL black", txtshuoming.Text.Trim());
                Printer.Command.PrintText(0, 200, 30, "ARIAL black", shuoming1);    
            }
            
            if (string.IsNullOrWhiteSpace(txtPO.Text.Trim()) == false)
            {
                Printer.Command.PrintText(280, 200, 30, "ARIAL black", txtPO.Text.Trim());
            }


            Printer.Command.PrintText(0, 220 , 30, "ARIAL black", "ALNO,INC.");
            Printer.Command.PrintText(205, 225 , 22, "ARIAL black", "Made In China");
        }


        void register()
        {
            lblLicense.Visible = true;
            LicenseManage.GetLicense();
            var licenseString = string.Format(_license_format,
                    LicenseManage.RoleTypeToString(),
                    Math.Ceiling((LicenseManage.ApplicationLicense.ExpireTime - DateTime.Now).TotalDays));
            if (LicenseManage.VerifyLicense(LicenseManage.ApplicationLicense, true))
            {
                _isExpire = false;
                lblLicense.Text = licenseString;
            }
            else
            {
                lblLicense.Text = $"{licenseString}，已過期！";
                _isExpire = true;
            }

            if(LicenseManage.ApplicationLicense.CustomRole == RoleType.Free)
            {
                lblLicense.Visible = false;
            }
        }
    }
}
