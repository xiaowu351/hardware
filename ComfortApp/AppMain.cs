using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComfortApp
{
    public partial class AppMain : Form
    {
        public int TabSelectedIndex
        {
            get
            {
                return tabControl1.SelectedIndex;
            }
        }
        public AppMain()
        {
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
