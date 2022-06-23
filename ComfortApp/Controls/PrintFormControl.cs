﻿using ComfortApp.Common;
using ComfortApp.Models;
using EzioDll;
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
        
         
        private string _tupian;
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
        }

        //private void btnPrint_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(AppMain?.TabSelectedIndex.ToString());
        //}

        private void RegisterEvent()
        {
            txtbihao.LostFocus += Txtbihao_LostFocus;
        }

        private void Txtbihao_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbihao.Text.Trim()))
            {
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
                    pbImage.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"./images/{_tupian}.bmp");
                }
                else
                {
                    txttiaoma.Text = string.Empty;
                    txtshuoming.Text = string.Empty;
                    txtnuber.Text = string.Empty;
                    txtshuoming1.Text = string.Empty;
                    if (cblock.Checked == false)
                    {
                        txtdingdan.Text = string.Empty;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(txtdingdan.Text.Trim()))
            {
                txtdingdan.Focus();
            }
            else
            {
                if (cblock.Checked)
                {
                    txtNumber.Focus();
                }
            }
            //ReadXTSZ();
            _tmsz_info = LibHelper.ReadXTSZ(AppMain.TabSelectedIndex);
            if (_tmsz_info == null)
            {
                MessageBox.Show("未找到系統設置信息，請先設置系統信息再試!");
                return;
            }
            ConnectPrinter();
            //if (_godexPrinter.openport("Godex EZ-1100 Plus") == false)
            //{
            //    MessageBox.Show("打开 Godex EZ-1100 Plus 失败，请检查名称是否正确!");
            //    _godexPrinter.closeport();
            //    return;
            //}
            Printer.Config.Setup(66, 10, 3, 0, 3, 0);
            //if (_godexPrinter.beginjob(66, 10, 3, 0, 3, 0) == false) //'(紙張高度, 列印明暗度, 列印速度, 0:標籤紙 1:連續紙 2:黑線紙, 標籤間距, 黑線寬度)
            //{
            //    MessageBox.Show("打印机Godex EZ-1100 Plus 开始打印工作失败!");
            //    return;
            //}

            //_godexPrinter.sendcommand("~AI"); // 文字命令，AI命令表示打印圖片            
            //_godexPrinter.sendcommand("~MDEL"); // 清除現在正在使用記憶體的所有資料
            Printer.Command.UploadImage_Int(pbImage.ImageLocation, _tupian, Image_Type.BMP);
            //_godexPrinter.intloadimage(pbImage.ImageLocation, _tupian, "bmp"); // 加载图形文件到条形码机内部存储器
            //_godexPrinter.endjob();
            //_godexPrinter.closeport();
            DisconnectPrinter();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new TMSZ(AppMain.TabSelectedIndex).ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            new CCFD(AppMain.TabSelectedIndex).ShowDialog();
        }
        private void PrintFormControl_Load(object sender, EventArgs e)
        {
            Printer = new GodexPrinter();
            _tupian = string.Empty;
            ReadXTSZ();
        }
        

        private void ReadXTSZ()
        {
            
            _tmsz_info = LibHelper.ReadXTSZ(AppMain.TabSelectedIndex);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //saveTosa();
            //print();
            printTest();
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


       void printTest()
        {
            #region MyRegion
            //_godexPrinter.openport("Godex EZ-1100 Plus");
            //_godexPrinter.beginjob(66, 10, 3, 0, 3, 0);
            //bool result = false;
            //result = _godexPrinter.sendcommand("^W50"); // 標籤寬度設定
            //result = _godexPrinter.sendcommand("^E15");//停歇点设定
            ////result = _godexPrinter.sendcommand("^h" + _tmsz_info.wd); //设定黑度(列印深浅)。 值愈大，印表头温度愈高
            ////result = _godexPrinter.sendcommand("^s" + _tmsz_info.sd); //列印速度设定,实际列印速度请参考各机种规格书
            ////result = _godexPrinter.sendcommand("^R" + _tmsz_info.le); //设定标签左边界起印点
            //result = _godexPrinter.sendcommand("^P1"); //列印张数设定,设定一次要列印的张数x = 1 ~ 32767
            //result = _godexPrinter.sendcommand("^L"); //标签起始符号设定
            //result = _godexPrinter.sendcommand("AC,20,60,1,1,1,0,TEST"); //标签起始符号设定
            //result = _godexPrinter.ecTextOut(20, 10, 30, "ARIAL black", "ALNO,INC.");
            ////result = _godexPrinter.ecTextOut(205, 475, 22, "ARIAL black", "Made In China");
            //result = _godexPrinter.sendcommand("E"); //标签结束命令。条码机接收此命令后，即开始列印。
            ////Thread.Sleep(2000);
            //result = _godexPrinter.endjob();
            //_godexPrinter.closeport(); 
            #endregion

            ConnectPrinter();

            LabelSetup();
            // Print Text
            Printer.Command.Start();

            
            //result = _godexPrinter.sendcommand("^L"); //标签起始符号设定
             
            //result = _godexPrinter.ecTextOut(20, 10, 30, "ARIAL black", "ALNO,INC.");
            ////result = _godexPrinter.ecTextOut(205, 475, 22, "ARIAL black", "Made In China");
            Printer.Command.PrintText(20, 10, 30, "ARIAL black", "ALNO,INC.");
            Printer.Command.PrintText(205, 475, 22, "ARIAL black", "Made In China");
            //Printer.Command.PrintText_Unicode(PosX, PosY += 40, FontHeight, "Arial", "這是中文測試");
            //Printer.Command.PrintText_Unicode(PosX, PosY += 40, FontHeight, "MS Gothic", "これは日本のテストです", 0, FontWeight.FW_400_NORMAL, RotateMode.Angle_180);
            //Printer.Command.PrintText_Unicode(PosX, PosY += 40, FontHeight, "GulimChe", "이것은 한국의 테스트입니다", 0, FontWeight.FW_900_HEAVY, RotateMode.Angle_0);
            //Printer.Command.PrintText(PosX, PosY += 40, FontHeight, "Arial", "GoDEX EZio DLL Test");
            //Printer.Command.PrintText(PosX, PosY += 40, FontHeight, "Arial", "GoDEX EZio DLL Test", 0, FontWeight.FW_900_HEAVY, RotateMode.Angle_180);
            //Printer.Command.PrintText(PosX, PosY += 40, FontHeight, "Arial", "GoDEX EZio DLL Test", 0, FontWeight.FW_700_BOLD, RotateMode.Angle_0, Italic_State.OFF, Underline_State.OFF, Strikeout_State.OFF, Inverse_State.ON);
            Printer.Command.End();

            DisconnectPrinter();
        }
        //------------------------------------------------------------------------
        // Connect Printer
        //------------------------------------------------------------------------
        private void ConnectPrinter()
        {                        
                Printer.Open("Godex EZ-1100 Plus");
        }
        private void LabelSetup()
        {
            Printer.Config.Setup(66, 10, 3, 0, 3, 0);
            Printer.Config.LabelWidth(50);// 標籤寬度設定
            Printer.Config.EndSetting(15);//停歇点设定
            Printer.Config.Dark(_tmsz_info.wd);//设定黑度(列印深浅)。 值愈大，印表头温度愈高
            Printer.Config.Speed(_tmsz_info.sd); //列印速度设定,实际列印速度请参考各机种规格书
            Printer.Config.LeftBorder(_tmsz_info.le);//设定标签左边界起印点
            Printer.Config.PageNo(1);//列印张数设定,设定一次要列印的张数x = 1 ~ 32767
            //Printer.Config.CopyNo((int)Num_Copy.Value);


            //_godexPrinter.beginjob(66, 10, 3, 0, 3, 0);
            //bool result = false;
            //result = _godexPrinter.sendcommand("^W50"); // 標籤寬度設定
            //result = _godexPrinter.sendcommand("^E15");//停歇点设定
            ////result = _godexPrinter.sendcommand("^h" + _tmsz_info.wd); //设定黑度(列印深浅)。 值愈大，印表头温度愈高
            ////result = _godexPrinter.sendcommand("^s" + _tmsz_info.sd); //列印速度设定,实际列印速度请参考各机种规格书
            ////result = _godexPrinter.sendcommand("^R" + _tmsz_info.le); //设定标签左边界起印点
            //result = _godexPrinter.sendcommand("^P1"); //列印张数设定,设定一次要列印的张数x = 1 ~ 32767
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

            LabelSetup();


            // bool result = false;
            // result = _godexPrinter.sendcommand("^W50"); // 標籤寬度設定
            //result =_godexPrinter.sendcommand("^E15");//停歇点设定
            //result =_godexPrinter.sendcommand("^h" + _tmsz_info.wd); //设定黑度(列印深浅)。 值愈大，印表头温度愈高
            //result =_godexPrinter.sendcommand("^s" + _tmsz_info.sd); //列印速度设定,实际列印速度请参考各机种规格书
            //result =_godexPrinter.sendcommand("^R" + _tmsz_info.le); //设定标签左边界起印点
            //result =_godexPrinter.sendcommand("^P" + txtNumber.Text.Trim()); //列印张数设定,设定一次要列印的张数x = 1 ~ 32767
            // result = _godexPrinter.sendcommand("^L"); //标签起始符号设定

            // Print Text
            Printer.Command.Start();

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
            // result = _godexPrinter.ecTextOut(10, 245, 39, "ARIAL black", txtbihao.Text.Trim());
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
                Printer.Command.Send("le,320,245,5,280");
            }
            else
            {
                //result = _godexPrinter.sendcommand("le,350,245,5,280");
                Printer.Command.Send("le,350,245,5,280");
            }
            var shuoming1 = txtshuoming1.Text.Trim();
            if (string.IsNullOrWhiteSpace(shuoming1))
            {
                //result = _godexPrinter.ecTextOut(0, 415, 32, "ARIAL black", txtshuoming.Text.Trim());
                Printer.Command.PrintText(0, 415, 32, "ARIAL black", txtshuoming.Text.Trim());

                //Bt,x,y,narrow,wide,height,rotation,readable,data - 条码命令
                //result = _godexPrinter.sendcommand("BH,0,297,3,5,90,0,1," + txttiaoma.Text.Trim());
                Printer.Command.Send("BH,0,297,3,5,90,0,1," + txttiaoma.Text.Trim());
            }
            else
            {
                //result = _godexPrinter.ecTextOut(0, 410, 30, "ARIAL black", txtshuoming.Text.Trim());
                Printer.Command.PrintText(0, 410, 30, "ARIAL black", txtshuoming.Text.Trim());
            }

            //if(string.IsNullOrWhiteSpace(shuoming1) == false && string.IsNullOrWhiteSpace( txtjuli.Text.Trim()) == false)
            //{
            //    _godexPrinter.ecTextOut(juli, 440, 30, "ARIAL black", shuoming1);
            //    _godexPrinter.sendcommand("BH,0,297,3,5,90,0,1,"+ txttiaoma.Text.Trim());
            //    if(string.IsNullOrWhiteSpace(txtdingdan.Text.Trim()) == false)
            //    {
            //        _godexPrinter.ecTextOut(280, 440, 30, "ARIAL black", txtdingdan.Text.Trim());
            //    }
            //}

            if (string.IsNullOrWhiteSpace(shuoming1) == false)//&& string.IsNullOrWhiteSpace(txtjuli.Text.Trim()))
            {
                //result = _godexPrinter.ecTextOut(0, 440, 30, "ARIAL black", shuoming1);
                Printer.Command.PrintText(0, 440, 30, "ARIAL black", shuoming1);

                //result = _godexPrinter.sendcommand("BH,0,297,3,5,90,0,1," + txttiaoma.Text.Trim());
                Printer.Command.Send("BH,0,297,3,5,90,0,1," + txttiaoma.Text.Trim());
                if (string.IsNullOrWhiteSpace(txtdingdan.Text.Trim()) == false)
                {
                    //result = _godexPrinter.ecTextOut(280, 440, 30, "ARIAL black", txtdingdan.Text.Trim());
                    Printer.Command.PrintText(280, 440, 30, "ARIAL black", txtdingdan.Text.Trim());
                }
            }

            // result = _godexPrinter.ecTextOut(0, 470, 30, "ARIAL black", "ALNO,INC.");
            // result = _godexPrinter.ecTextOut(205, 475, 22, "ARIAL black", "Made In China");
            Printer.Command.PrintText(20, 10, 30, "ARIAL black", "ALNO,INC.");
            Printer.Command.PrintText(205, 475, 22, "ARIAL black", "Made In China");
            // result = _godexPrinter.sendcommand("E"); //标签结束命令。条码机接收此命令后，即开始列印。
            Printer.Command.End();
            // Thread.Sleep(2000);
            // result = _godexPrinter.endjob();
            // _godexPrinter.closeport();
            DisconnectPrinter();

        }


    }
}
