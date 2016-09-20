using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnifferDemo
{
    public partial class PotDisplay : UserControl
    {
        string m_Title = "";
        int m_Max = 100, m_Value = 0, m_Min = 0, m_ID = 0;
        public PotDisplay()
        {
            InitializeComponent();
        }
        public int Maximum
        {
            set { m_Max = value; this.Refresh(); }
            get { return m_Max; }
        }
        public int Minimum
        {
            set { m_Min = value; this.Refresh(); }
            get { return m_Min; }
        }
        public int Value
        {
            set { m_Value = value; this.Refresh(); }
            get { return m_Value; }
        }
        public int ID
        {
            set { m_ID = value; this.Refresh(); }
            get { return m_ID; }
        }
        public string Title
        {
            set { m_Title = value; this.Refresh(); }
            get { return m_Title; }
        }

        private void PotDisplay_Paint(object sender, PaintEventArgs e)
        {
            int tmpCircleSize = Math.Min(this.Width - this.Padding.Left - this.Padding.Right, this.Height - this.Padding.Top - this.Padding.Bottom);
            int tmpX = (this.Width - this.Padding.Left - this.Padding.Right - tmpCircleSize) / 2 + this.Padding.Left;
            int tmpY = (this.Height - this.Padding.Top - this.Padding.Bottom - tmpCircleSize) / 2 + this.Padding.Top;

            Graphics graphics = this.CreateGraphics();
            Rectangle rectangle = new Rectangle(tmpX, tmpY, tmpCircleSize, tmpCircleSize);
            Rectangle rectangle2 = new Rectangle(this.Padding.Left, this.Padding.Top, this.Width - this.Padding.Left - this.Padding.Right, this.Height - this.Padding.Top - this.Padding.Bottom);

            graphics.DrawString(m_Title, this.Font, Brushes.Black, (float)this.Padding.Left, 0.0f);

            graphics.DrawEllipse(Pens.Black, rectangle);
            graphics.FillPie(new SolidBrush(Color.Red), rectangle, 90.0f, ((float)m_Value/(float)(m_Max-m_Min) + (float)m_Min)*360.0f);
            graphics.DrawPie(Pens.Black, rectangle, 90.0f, ((float)m_Value / (float)(m_Max - m_Min) + (float)m_Min) * 360.0f);

        }
    }
}
