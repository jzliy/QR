using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace IndustrialPC
{
    public partial class _SplashScreen : Form
    {
        public _SplashScreen()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            init();
        }
        private delegate void SetPos(int ipos, string vinfo);

        private void SetTextMesssage(int ipos, string vinfo)   
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                this.Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                this.progressBar1.Value = Convert.ToInt32(ipos);
            }
        }
        private void init()
        {
            Thread fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
        }
        private void SleepT()
        {
            for (int i = 0; i < 500; i++)
            {
                System.Threading.Thread.Sleep(10);
                SetTextMesssage(100 * i / 500, i.ToString() + "\r\n");
            }
        }
    }
}
