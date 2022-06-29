using ComfortApp.Common;
using ComfortApp.Models;
using System;

using System.Windows.Forms;

namespace ComfortApp
{
    public partial class TMSZ : ABathForm
    {
        private int _dbIndex;
        private TMSZ_Info _tmsz_Info;
        public TMSZ(int dbIndex)
        {
            InitializeComponent();
            _dbIndex = dbIndex;
            cmbGap.SelectedIndex = 0;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            sql = $"insert into tmsz(dk,wd,sd,le,gap) values(" +
                   $"{txtdk.Text.Trim()}," +
                   $"{txtwd.Text.Trim()}," +
                   $"{txtsd.Text.Trim()}," +
                   $"{txtle.Text.Trim()}," +
                   $"{cmbGap.Text.Trim()})";
            if (_tmsz_Info != null)
            {
                AccessDbHelper.ExecuteNonQuery(_dbIndex, "delete from tmsz");
            }

            AccessDbHelper.ExecuteNonQuery(_dbIndex, sql);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TMSZ_Load(object sender, EventArgs e)
        {
            txtdk.Focus();
            LibHelper.FindTextBoxControl(this);
            _tmsz_Info = LibHelper.ReadXTSZ(_dbIndex);
            if (_tmsz_Info != null)
            {
                txtdk.Text = $"{_tmsz_Info.dk}";
                txtwd.Text = $"{_tmsz_Info.wd}";
                txtsd.Text = $"{_tmsz_Info.sd}";
                txtle.Text = $"{_tmsz_Info.le}";
                cmbGap.SelectedItem = _tmsz_Info.gap.ToString();
            }

        }


    }
}
