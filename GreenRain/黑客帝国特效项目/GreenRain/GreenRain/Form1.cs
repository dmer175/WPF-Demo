using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GreenRain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Text = "黑客帝国";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string temp = textBox_charlist.Text.Trim();
            if (temp != string.Empty)
            {
                string[] words = temp.Split(',');
                wordRain1.SetArrayValue(new System.Collections.ArrayList(words));
            }
        }

        private void button_fontsize_Click(object sender, EventArgs e)
        {
            if (textBox_fontsizemin.Text.Trim() == string.Empty || textBox_fontsizemax.Text.Trim() == string.Empty)
            {
                return;
            }
            int min = int.Parse(textBox_fontsizemin.Text.Trim());
            int max = int.Parse(textBox_fontsizemax.Text.Trim());
            if (min > 50 || min < 1 || max > 50 || min < 1 || min > max)
            {
                MessageBox.Show("参数错误");
                return;
            }
            wordRain1.SetFontSizeRange(min, max);
        }

        private void button_count_Click(object sender, EventArgs e)
        {
            if (textBox_countmin.Text.Trim() == string.Empty || textBox_countmin.Text.Trim() == string.Empty)
            {
                return;
            }
            int min = int.Parse(textBox_countmin.Text.Trim());
            int max = int.Parse(textBox_countmax.Text.Trim());
            if (min > 100 || min < 1 || max > 100 || max < 1 || min > max)
            {
                MessageBox.Show("参数错误");
                return;
            }
            wordRain1.SetCountRange(min, max);
        }

        private void button_speed_Click(object sender, EventArgs e)
        {
            if (textBox_speedmin.Text.Trim() == string.Empty || textBox_speedmin.Text.Trim() == string.Empty)
            {
                return;
            }
            float min = float.Parse(textBox_speedmin.Text.Trim());
            float max = float.Parse(textBox_speedmax.Text.Trim());
            if (min > 100.0 || min < 0.1 || max > 100.0 || max < 0.1 || min > max)
            {
                MessageBox.Show("参数错误");
                return;
            }
            wordRain1.SetSpeedRange(min, max);
        }

        private void button_color_Click(object sender, EventArgs e)
        {
            if (textBox_colorRmin.Text.Trim() == string.Empty || textBox_colorRmax.Text.Trim() == string.Empty
                || textBox_colorGmin.Text.Trim() == string.Empty || textBox_colorGmax.Text.Trim() == string.Empty
                || textBox_colorBmin.Text.Trim() == string.Empty || textBox_colorBmax.Text.Trim() == string.Empty)
            {
                return;
            }
            int rmin = int.Parse(textBox_colorRmin.Text.Trim());
            int rmax = int.Parse(textBox_colorRmax.Text.Trim());
            int gmin = int.Parse(textBox_colorGmin.Text.Trim());
            int gmax = int.Parse(textBox_colorGmax.Text.Trim());
            int bmin = int.Parse(textBox_colorBmin.Text.Trim());
            int bmax = int.Parse(textBox_colorBmax.Text.Trim());
            wordRain1.SetFontColorRange(Color.FromArgb(rmin, gmin, bmin), Color.FromArgb(rmax, gmax, bmax));
        }
    }
}
