using ComfortApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComfortApp
{
    public partial class CCFD : ABathForm
    {
        private ImageMode _imageMode;
        private int _dbIndex;
        public CCFD(ImageMode imageMode)
        {
            InitializeComponent();
            _dbIndex = (int)imageMode;
            _imageMode = imageMode;
            RegisterEvent();
            if (_imageMode == ImageMode.No)
            {
                txttupian.Visible = false;
                pbImage.Visible = false;
                lbltupian.Visible = false;
            }

           

        }

        private void RegisterEvent()
        {
            txtbihao.LostFocus += Txtbihao_LostFocus;
            txttiaoma.LostFocus += Txttiaoma_LostFocus;
             
        }

        

        private void Txttiaoma_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txttiaoma.Text.Trim())
                && txttiaoma.Text.Trim().Length != 11)
            {
                MessageBox.Show("不能為空或者條碼編號不是11位!");
                txttiaoma.Focus();
                return;
            }
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
                //MessageBox.Show("編號格式不正確!");
                //return;
                tupianIndex = bihao.Length;
            }
            if(_imageMode == ImageMode.Yes)
            {
                txttupian.Text = bihao.Substring(0, tupianIndex).Trim();
            }
            
            using (var reader = AccessDbHelper.GetOleDbDataReader(_dbIndex, $"select * from tiaom  where bihao ='{bihao}'"))
            {
                if (reader.Read())
                {
                    txttiaoma.Text = $"{ reader["tiaoma"]}".Trim();
                    txtshuoming.Text = $"{ reader["shuming"]}".Trim();
                    txtshuoming1.Text = $"{ reader["shuming1"]}".Trim();
                    txtpo.Text = $"{ reader["nuber"]}".Trim();
                    txtjuli.Text = $"{ reader["juli"]}".Trim();
                    //txtdingdan.Text = $"{ reader["dingdan"]}";
                    //pbImage.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"./images/{_tupian}.bmp");
                }
                else
                {
                    txttiaoma.Text = string.Empty;
                    txtshuoming.Text = string.Empty;
                    txtshuoming1.Text = string.Empty;
                    txtpo.Text = string.Empty;
                        txtjuli.Text = string.Empty;
                }
            } 
        }

        private void CCFD_Load(object sender, EventArgs e)
        {
            LoadData();
            txtbihao.Focus();
            LibHelper.FindTextBoxControl(splitContainer1.Panel1);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtbihao.Text.Trim()))
            {
                MessageBox.Show("編號不能為空!");
                return;
            }
            var existObj = AccessDbHelper.ExecuteScalar(_dbIndex, $"select count(*) from tiaom where  tiaoma='{txttiaoma.Text.Trim()}'");
            if (existObj is int && Convert.ToInt32(existObj) > 0)
            {
                MessageBox.Show($"此條碼已存在!{txtbihao.Text.Trim()}");
                txttiaoma.Text = string.Empty;
                return;

            }

            if(string.IsNullOrWhiteSpace(txtbihao.Text.Trim()) ==false &&
                string.IsNullOrWhiteSpace(txtshuoming.Text.Trim())==false)
            {
                var exist_bihao = AccessDbHelper.ExecuteScalar(_dbIndex, $"select count(*) from tiaom where  bihao='{txtbihao.Text.Trim()}'");
                if (exist_bihao is int && Convert.ToInt32(exist_bihao) > 0)
                {
                    //修改
                    if(MessageBox.Show("編號已存在,是否要修改!!!!","提示", MessageBoxButtons.YesNo) == DialogResult.OK)
                    {
                        var updateSql = $"update tiaom set tiaoma='{txttiaoma.Text.Trim()}'," +
                            $"shuming='{txtshuoming.Text.Trim()}',shuming1='{txtshuoming1.Text.Trim()}'," +
                            $"nuber='{txtpo.Text.Trim()}',juli='{txtjuli.Text.Trim()}',tupian='{txttupian.Text.Trim()}'" +
                            $" where bihao='{txtbihao.Text.Trim()}'";
                        AccessDbHelper.ExecuteNonQuery(_dbIndex, updateSql);
                    }
                }
                else
                {
                    //新增
                    var insertSql = "insert into tiaom(bihao,tiaoma,shuming,shuming1,nuber,juli,tupian) values(" +
                        $"'{txtbihao.Text.Trim()}','{txttiaoma.Text.Trim()}','{txtshuoming.Text.Trim()}','{txtshuoming1.Text.Trim()}','{txtpo.Text.Trim()}','{txtjuli.Text.Trim()}','{txttupian.Text.Trim()}')";
                    AccessDbHelper.ExecuteNonQuery(_dbIndex, insertSql);
                }
                MessageBox.Show("保存成功!!");
            }
            txtbihao.Text = string.Empty;
            txttiaoma.Text = string.Empty;
            txtshuoming.Text = string.Empty;
            txtshuoming1.Text = string.Empty;
            txtpo.Text = string.Empty;
            txtjuli.Text = string.Empty;
            LoadData();
            txtbihao.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDeletebihao.Text.Trim()))
            {
                MessageBox.Show("刪除編號不能為空!!");
                return;
            }
            var result = AccessDbHelper.ExecuteNonQuery(_dbIndex, $"delete from tiaom where bihao ='{txtDeletebihao.Text.Trim()}'");

            if (result > 0)
            {
                MessageBox.Show($"[{txtDeletebihao.Text.Trim()}]刪除成功");
                txtDeletebihao.Text = string.Empty;
            }
            else
            {
                MessageBox.Show($"[{txtDeletebihao.Text.Trim()}]刪除失敗,記錄不存在!");
            }
            LoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();//添加行号
        }

        private void txttupian_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txttupian.Text.Trim()) == false)
            {
                pbImage.ImageLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"./images/{txttupian.Text.Trim()}.bmp");

            }
        }

        private void txttiaoma_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和十进制数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtbihao_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Convert.ToChar(e.KeyChar.ToString().ToUpper());
        }

        private void LoadData()
        {
            var data = AccessDbHelper.GetDataTable(_dbIndex, "select RTrim(nuber)  as 序號,RTrim(bihao) as  編號,RTrim(tiaoma)  as 條碼,RTrim(shuming) as 說明,RTrim(shuming1) as 說明1 ,RTrim(tupian)  as 圖片 from tiaom   order by bihao asc");
            dgView.DataSource = data;

        }
    }
}
