using ComfortApp.Common;
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
    public partial class AppMain : ABathForm
    {
        public int TabSelectedIndex
        {
            get
            {
                return tabControl1.SelectedIndex;
            }
        }

        public ImageMode ImageMode
        {
            get
            {
                if (TabSelectedIndex == 0)
                {
                    return ImageMode.No;
                }
                else
                {
                    return ImageMode.Yes;
                }
            }
        }
        public AppMain()
        {
            InitializeComponent();
            this.Activated += AppMain_Activated;
        }

        private void AppMain_Activated(object sender, EventArgs e)
        {
            FirstFocus();
        }

        private void AppMain_Load(object sender, EventArgs e)
        {
             
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FirstFocus();
        }

       private void FirstFocus()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                printFormControl1.FirstFocus();
            }
            else
            {
                printFormControl2.FirstFocus();
            }
        }
    }
}
