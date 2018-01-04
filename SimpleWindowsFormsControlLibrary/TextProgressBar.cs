using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleWindowsFormsControlLibrary
{
    [ToolboxItem(true)]
    public partial class TextProgressBar : System.Windows.Forms.ProgressBar
    {
        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        // 添加文字相关的私有字段
        private string text = "正在启动程序...";
        private Color textColor = Color.Black;
        private Font font = new System.Drawing.Font("SimSun ", 9);

        // 重写Text属性
        [BrowsableAttribute(true)]
        [BindableAttribute(true)]
        public override string Text
        {
            get { return text; }
            set { text = value; this.Invalidate(); }
        }

        // 文字颜色
        public System.Drawing.Color TextColor
        {
            get { return textColor; }
            set { textColor = value; this.Invalidate(); }
        }

        // 重写Font属性
        [BrowsableAttribute(false)]
        public override Font Font
        {
            get { return font; }
            set { font = value; this.Invalidate(); }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0xf || m.Msg == 0x133)
            {
                //拦截系统消息，获得当前控件进程以便重绘。  
                //一些控件（如TextBox、Button等）是由系统进程绘制，重载OnPaint方法将不起作用.  
                //所有这里并没有使用重载OnPaint方法绘制TextBox边框。  
                //MSDN:重写   OnPaint   将禁止修改所有控件的外观。  
                //那些由   Windows   完成其所有绘图的控件（例如   Textbox）从不调用它们的   OnPaint   方法，  
                //因此将永远不会使用自定义代码。请参见您要修改的特定控件的文档，  
                //查看   OnPaint   方法是否可用。如果某个控件未将   OnPaint   作为成员方法列出，  
                //则您无法通过重写此方法改变其外观。  
                //MSDN:要了解可用的   Message.Msg、Message.LParam   和   Message.WParam   值，  
                //请参考位于   MSDN   Library   中的   Platform   SDK   文档参考。可在   Platform   SDK（“Core   SDK”一节）  
                //下载中包含的   windows.h   头文件中找到实际常数值，该文件也可在   MSDN   上找到。  

                IntPtr hDC = GetWindowDC(m.HWnd);
                if (hDC.ToInt32() == 0)
                {
                    return;
                }

                //base.OnPaint(e);

                System.Drawing.Graphics g = Graphics.FromHdc(hDC);
                SolidBrush brush = new SolidBrush(textColor);

                SizeF size = g.MeasureString(text, font);
                float x = (this.Width - size.Width) / 2;
                float y = (this.Height - size.Height) / 2;
                g.DrawString(Text, font, brush, x, y);
                // 下面的显示方法与上面的方法有相同的效果
                //StringFormat format = new StringFormat();
                //format.Alignment = StringAlignment.Center;
                //format.LineAlignment = StringAlignment.Center;
                //g.DrawString(text, font, brush, this.ClientRectangle, format);

                //返回结果
                m.Result = IntPtr.Zero;
                //释放
                ReleaseDC(m.HWnd, hDC);
            }
        }
    }
}
