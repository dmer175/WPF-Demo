namespace GreenRain
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.wordRain1 = new myControl.WordRain();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_charlist = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_fontsizemin = new System.Windows.Forms.TextBox();
            this.textBox_fontsizemax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_countmin = new System.Windows.Forms.TextBox();
            this.textBox_countmax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_speedmin = new System.Windows.Forms.TextBox();
            this.textBox_speedmax = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_colorRmin = new System.Windows.Forms.TextBox();
            this.textBox_colorRmax = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_colorGmin = new System.Windows.Forms.TextBox();
            this.textBox_colorGmax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_colorBmin = new System.Windows.Forms.TextBox();
            this.textBox_colorBmax = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button_fontsize = new System.Windows.Forms.Button();
            this.button_count = new System.Windows.Forms.Button();
            this.button_speed = new System.Windows.Forms.Button();
            this.button_color = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wordRain1
            // 
            this.wordRain1.BackColor = System.Drawing.Color.Black;
            this.wordRain1.Location = new System.Drawing.Point(12, 12);
            this.wordRain1.Name = "wordRain1";
            this.wordRain1.Size = new System.Drawing.Size(667, 498);
            this.wordRain1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(685, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "设置字符(以英文逗号隔开)";
            // 
            // textBox_charlist
            // 
            this.textBox_charlist.Location = new System.Drawing.Point(686, 28);
            this.textBox_charlist.Multiline = true;
            this.textBox_charlist.Name = "textBox_charlist";
            this.textBox_charlist.Size = new System.Drawing.Size(142, 64);
            this.textBox_charlist.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(685, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "设置字体大小范围([1,50])";
            // 
            // textBox_fontsizemin
            // 
            this.textBox_fontsizemin.Location = new System.Drawing.Point(708, 149);
            this.textBox_fontsizemin.Name = "textBox_fontsizemin";
            this.textBox_fontsizemin.Size = new System.Drawing.Size(40, 21);
            this.textBox_fontsizemin.TabIndex = 3;
            // 
            // textBox_fontsizemax
            // 
            this.textBox_fontsizemax.Location = new System.Drawing.Point(789, 149);
            this.textBox_fontsizemax.Name = "textBox_fontsizemax";
            this.textBox_fontsizemax.Size = new System.Drawing.Size(40, 21);
            this.textBox_fontsizemax.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(763, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(685, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "设置字体个数范围([1,100])";
            // 
            // textBox_countmin
            // 
            this.textBox_countmin.Location = new System.Drawing.Point(708, 232);
            this.textBox_countmin.Name = "textBox_countmin";
            this.textBox_countmin.Size = new System.Drawing.Size(40, 21);
            this.textBox_countmin.TabIndex = 3;
            // 
            // textBox_countmax
            // 
            this.textBox_countmax.Location = new System.Drawing.Point(789, 232);
            this.textBox_countmax.Name = "textBox_countmax";
            this.textBox_countmax.Size = new System.Drawing.Size(40, 21);
            this.textBox_countmax.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(763, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "-";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(685, 295);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "设置速度范围([0.1,100.0])";
            // 
            // textBox_speedmin
            // 
            this.textBox_speedmin.Location = new System.Drawing.Point(708, 310);
            this.textBox_speedmin.Name = "textBox_speedmin";
            this.textBox_speedmin.Size = new System.Drawing.Size(40, 21);
            this.textBox_speedmin.TabIndex = 3;
            // 
            // textBox_speedmax
            // 
            this.textBox_speedmax.Location = new System.Drawing.Point(789, 310);
            this.textBox_speedmax.Name = "textBox_speedmax";
            this.textBox_speedmax.Size = new System.Drawing.Size(40, 21);
            this.textBox_speedmax.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(763, 313);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(685, 376);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "设置颜色范围";
            // 
            // textBox_colorRmin
            // 
            this.textBox_colorRmin.Location = new System.Drawing.Point(708, 391);
            this.textBox_colorRmin.Name = "textBox_colorRmin";
            this.textBox_colorRmin.Size = new System.Drawing.Size(40, 21);
            this.textBox_colorRmin.TabIndex = 3;
            // 
            // textBox_colorRmax
            // 
            this.textBox_colorRmax.Location = new System.Drawing.Point(789, 391);
            this.textBox_colorRmax.Name = "textBox_colorRmax";
            this.textBox_colorRmax.Size = new System.Drawing.Size(40, 21);
            this.textBox_colorRmax.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(763, 394);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 5;
            this.label9.Text = "-";
            // 
            // textBox_colorGmin
            // 
            this.textBox_colorGmin.Location = new System.Drawing.Point(708, 418);
            this.textBox_colorGmin.Name = "textBox_colorGmin";
            this.textBox_colorGmin.Size = new System.Drawing.Size(40, 21);
            this.textBox_colorGmin.TabIndex = 3;
            // 
            // textBox_colorGmax
            // 
            this.textBox_colorGmax.Location = new System.Drawing.Point(789, 418);
            this.textBox_colorGmax.Name = "textBox_colorGmax";
            this.textBox_colorGmax.Size = new System.Drawing.Size(40, 21);
            this.textBox_colorGmax.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(763, 421);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "-";
            // 
            // textBox_colorBmin
            // 
            this.textBox_colorBmin.Location = new System.Drawing.Point(708, 445);
            this.textBox_colorBmin.Name = "textBox_colorBmin";
            this.textBox_colorBmin.Size = new System.Drawing.Size(40, 21);
            this.textBox_colorBmin.TabIndex = 3;
            // 
            // textBox_colorBmax
            // 
            this.textBox_colorBmax.Location = new System.Drawing.Point(789, 445);
            this.textBox_colorBmax.Name = "textBox_colorBmax";
            this.textBox_colorBmax.Size = new System.Drawing.Size(40, 21);
            this.textBox_colorBmax.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(763, 448);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 12);
            this.label11.TabIndex = 5;
            this.label11.Text = "-";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(691, 394);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 12);
            this.label13.TabIndex = 5;
            this.label13.Text = "R";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(691, 418);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 12);
            this.label14.TabIndex = 5;
            this.label14.Text = "-";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(691, 454);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(11, 12);
            this.label15.TabIndex = 5;
            this.label15.Text = "-";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(691, 454);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(11, 12);
            this.label16.TabIndex = 5;
            this.label16.Text = "B";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(691, 421);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(11, 12);
            this.label17.TabIndex = 5;
            this.label17.Text = "G";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(753, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_fontsize
            // 
            this.button_fontsize.Location = new System.Drawing.Point(753, 176);
            this.button_fontsize.Name = "button_fontsize";
            this.button_fontsize.Size = new System.Drawing.Size(75, 23);
            this.button_fontsize.TabIndex = 7;
            this.button_fontsize.Text = "设置";
            this.button_fontsize.UseVisualStyleBackColor = true;
            this.button_fontsize.Click += new System.EventHandler(this.button_fontsize_Click);
            // 
            // button_count
            // 
            this.button_count.Location = new System.Drawing.Point(754, 261);
            this.button_count.Name = "button_count";
            this.button_count.Size = new System.Drawing.Size(75, 23);
            this.button_count.TabIndex = 7;
            this.button_count.Text = "设置";
            this.button_count.UseVisualStyleBackColor = true;
            this.button_count.Click += new System.EventHandler(this.button_count_Click);
            // 
            // button_speed
            // 
            this.button_speed.Location = new System.Drawing.Point(754, 337);
            this.button_speed.Name = "button_speed";
            this.button_speed.Size = new System.Drawing.Size(75, 23);
            this.button_speed.TabIndex = 7;
            this.button_speed.Text = "设置";
            this.button_speed.UseVisualStyleBackColor = true;
            this.button_speed.Click += new System.EventHandler(this.button_speed_Click);
            // 
            // button_color
            // 
            this.button_color.Location = new System.Drawing.Point(754, 472);
            this.button_color.Name = "button_color";
            this.button_color.Size = new System.Drawing.Size(75, 23);
            this.button_color.TabIndex = 7;
            this.button_color.Text = "设置";
            this.button_color.UseVisualStyleBackColor = true;
            this.button_color.Click += new System.EventHandler(this.button_color_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 522);
            this.Controls.Add(this.button_color);
            this.Controls.Add(this.button_speed);
            this.Controls.Add(this.button_count);
            this.Controls.Add(this.button_fontsize);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_colorBmax);
            this.Controls.Add(this.textBox_colorGmax);
            this.Controls.Add(this.textBox_colorRmax);
            this.Controls.Add(this.textBox_speedmax);
            this.Controls.Add(this.textBox_countmax);
            this.Controls.Add(this.textBox_fontsizemax);
            this.Controls.Add(this.textBox_colorBmin);
            this.Controls.Add(this.textBox_colorGmin);
            this.Controls.Add(this.textBox_colorRmin);
            this.Controls.Add(this.textBox_speedmin);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_countmin);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_fontsizemin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_charlist);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wordRain1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private myControl.WordRain wordRain1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_charlist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_fontsizemin;
        private System.Windows.Forms.TextBox textBox_fontsizemax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_countmin;
        private System.Windows.Forms.TextBox textBox_countmax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_speedmin;
        private System.Windows.Forms.TextBox textBox_speedmax;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_colorRmin;
        private System.Windows.Forms.TextBox textBox_colorRmax;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_colorGmin;
        private System.Windows.Forms.TextBox textBox_colorGmax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_colorBmin;
        private System.Windows.Forms.TextBox textBox_colorBmax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_fontsize;
        private System.Windows.Forms.Button button_count;
        private System.Windows.Forms.Button button_speed;
        private System.Windows.Forms.Button button_color;

    }
}

