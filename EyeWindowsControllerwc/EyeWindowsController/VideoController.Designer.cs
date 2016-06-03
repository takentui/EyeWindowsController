namespace EyeWindowsController
{
    partial class VideoController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.StartSettingButton = new System.Windows.Forms.Button();
            this.PauseSettingButton = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdiobtnRed = new System.Windows.Forms.RadioButton();
            this.rdiobtnBlue = new System.Windows.Forms.RadioButton();
            this.rdiobtnTekCisimTakibi = new System.Windows.Forms.RadioButton();
            this.rdiobtnCokCisimTakibi = new System.Windows.Forms.RadioButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdiobtnGeoSekil = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdiobtnHandSet = new System.Windows.Forms.RadioButton();
            this.rdiobtnGreen = new System.Windows.Forms.RadioButton();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.chkboxMesafeOlcer = new System.Windows.Forms.CheckBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.chkboxMesafeKordinati = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.chkBoxSettingsMode = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 26);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(342, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(262, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // StartSettingButton
            // 
            this.StartSettingButton.Location = new System.Drawing.Point(16, 19);
            this.StartSettingButton.Name = "StartSettingButton";
            this.StartSettingButton.Size = new System.Drawing.Size(233, 26);
            this.StartSettingButton.TabIndex = 2;
            this.StartSettingButton.Text = "Запустить Трекер";
            this.StartSettingButton.UseVisualStyleBackColor = true;
            this.StartSettingButton.Click += new System.EventHandler(this.StartSettingButton_Click);
            // 
            // PauseSettingButton
            // 
            this.PauseSettingButton.Location = new System.Drawing.Point(16, 51);
            this.PauseSettingButton.Name = "PauseSettingButton";
            this.PauseSettingButton.Size = new System.Drawing.Size(233, 26);
            this.PauseSettingButton.TabIndex = 3;
            this.PauseSettingButton.Text = "Пауза";
            this.PauseSettingButton.UseVisualStyleBackColor = true;
            this.PauseSettingButton.Click += new System.EventHandler(this.PauseSettingButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox2.Location = new System.Drawing.Point(12, 312);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(320, 240);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StartSettingButton);
            this.groupBox1.Controls.Add(this.PauseSettingButton);
            this.groupBox1.Location = new System.Drawing.Point(342, 209);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(262, 84);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление камерой";
            // 
            // rdiobtnRed
            // 
            this.rdiobtnRed.AutoSize = true;
            this.rdiobtnRed.Location = new System.Drawing.Point(8, 19);
            this.rdiobtnRed.Name = "rdiobtnRed";
            this.rdiobtnRed.Size = new System.Drawing.Size(84, 17);
            this.rdiobtnRed.TabIndex = 6;
            this.rdiobtnRed.TabStop = true;
            this.rdiobtnRed.Text = "Красный(R)";
            this.rdiobtnRed.UseVisualStyleBackColor = true;
            // 
            // rdiobtnBlue
            // 
            this.rdiobtnBlue.AutoSize = true;
            this.rdiobtnBlue.Location = new System.Drawing.Point(8, 65);
            this.rdiobtnBlue.Name = "rdiobtnBlue";
            this.rdiobtnBlue.Size = new System.Drawing.Size(69, 17);
            this.rdiobtnBlue.TabIndex = 7;
            this.rdiobtnBlue.TabStop = true;
            this.rdiobtnBlue.Text = "Синий(B)";
            this.rdiobtnBlue.UseVisualStyleBackColor = true;
            // 
            // rdiobtnTekCisimTakibi
            // 
            this.rdiobtnTekCisimTakibi.AutoSize = true;
            this.rdiobtnTekCisimTakibi.Location = new System.Drawing.Point(14, 24);
            this.rdiobtnTekCisimTakibi.Name = "rdiobtnTekCisimTakibi";
            this.rdiobtnTekCisimTakibi.Size = new System.Drawing.Size(126, 17);
            this.rdiobtnTekCisimTakibi.TabIndex = 8;
            this.rdiobtnTekCisimTakibi.TabStop = true;
            this.rdiobtnTekCisimTakibi.Text = "За одним объектом";
            this.rdiobtnTekCisimTakibi.UseVisualStyleBackColor = true;
            // 
            // rdiobtnCokCisimTakibi
            // 
            this.rdiobtnCokCisimTakibi.AutoSize = true;
            this.rdiobtnCokCisimTakibi.Location = new System.Drawing.Point(14, 47);
            this.rdiobtnCokCisimTakibi.Name = "rdiobtnCokCisimTakibi";
            this.rdiobtnCokCisimTakibi.Size = new System.Drawing.Size(109, 17);
            this.rdiobtnCokCisimTakibi.TabIndex = 9;
            this.rdiobtnCokCisimTakibi.TabStop = true;
            this.rdiobtnCokCisimTakibi.Text = "За несколькими";
            this.rdiobtnCokCisimTakibi.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(342, 82);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(262, 121);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(339, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Координаты объекта";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdiobtnGeoSekil);
            this.groupBox2.Controls.Add(this.rdiobtnTekCisimTakibi);
            this.groupBox2.Controls.Add(this.rdiobtnCokCisimTakibi);
            this.groupBox2.Location = new System.Drawing.Point(644, 472);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(193, 100);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Режим слежения";
            this.groupBox2.Visible = false;
            // 
            // rdiobtnGeoSekil
            // 
            this.rdiobtnGeoSekil.AutoSize = true;
            this.rdiobtnGeoSekil.Location = new System.Drawing.Point(14, 70);
            this.rdiobtnGeoSekil.Name = "rdiobtnGeoSekil";
            this.rdiobtnGeoSekil.Size = new System.Drawing.Size(150, 17);
            this.rdiobtnGeoSekil.TabIndex = 13;
            this.rdiobtnGeoSekil.TabStop = true;
            this.rdiobtnGeoSekil.Text = "Геометрические фигуры";
            this.rdiobtnGeoSekil.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdiobtnHandSet);
            this.groupBox3.Controls.Add(this.rdiobtnGreen);
            this.groupBox3.Controls.Add(this.rdiobtnRed);
            this.groupBox3.Controls.Add(this.rdiobtnBlue);
            this.groupBox3.Location = new System.Drawing.Point(342, 438);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(129, 114);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Фильтр";
            // 
            // rdiobtnHandSet
            // 
            this.rdiobtnHandSet.AutoSize = true;
            this.rdiobtnHandSet.Location = new System.Drawing.Point(8, 88);
            this.rdiobtnHandSet.Name = "rdiobtnHandSet";
            this.rdiobtnHandSet.Size = new System.Drawing.Size(116, 17);
            this.rdiobtnHandSet.TabIndex = 24;
            this.rdiobtnHandSet.TabStop = true;
            this.rdiobtnHandSet.Text = "Ручная настройка";
            this.rdiobtnHandSet.UseVisualStyleBackColor = true;
            // 
            // rdiobtnGreen
            // 
            this.rdiobtnGreen.AutoSize = true;
            this.rdiobtnGreen.Location = new System.Drawing.Point(8, 42);
            this.rdiobtnGreen.Name = "rdiobtnGreen";
            this.rdiobtnGreen.Size = new System.Drawing.Size(84, 17);
            this.rdiobtnGreen.TabIndex = 8;
            this.rdiobtnGreen.TabStop = true;
            this.rdiobtnGreen.Text = "Зеленый(G)";
            this.rdiobtnGreen.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(17, 38);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(100, 115);
            this.richTextBox2.TabIndex = 14;
            this.richTextBox2.Text = "";
            // 
            // chkboxMesafeOlcer
            // 
            this.chkboxMesafeOlcer.AutoSize = true;
            this.chkboxMesafeOlcer.Location = new System.Drawing.Point(17, 21);
            this.chkboxMesafeOlcer.Name = "chkboxMesafeOlcer";
            this.chkboxMesafeOlcer.Size = new System.Drawing.Size(86, 17);
            this.chkboxMesafeOlcer.TabIndex = 16;
            this.chkboxMesafeOlcer.Text = "Расстояние";
            this.chkboxMesafeOlcer.UseVisualStyleBackColor = true;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(136, 38);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(100, 115);
            this.richTextBox3.TabIndex = 17;
            this.richTextBox3.Text = "";
            // 
            // chkboxMesafeKordinati
            // 
            this.chkboxMesafeKordinati.AutoSize = true;
            this.chkboxMesafeKordinati.Location = new System.Drawing.Point(136, 20);
            this.chkboxMesafeKordinati.Name = "chkboxMesafeKordinati";
            this.chkboxMesafeKordinati.Size = new System.Drawing.Size(115, 17);
            this.chkboxMesafeKordinati.TabIndex = 18;
            this.chkboxMesafeKordinati.Text = "Расст. координат";
            this.chkboxMesafeKordinati.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.richTextBox3);
            this.groupBox4.Controls.Add(this.chkboxMesafeOlcer);
            this.groupBox4.Controls.Add(this.chkboxMesafeKordinati);
            this.groupBox4.Controls.Add(this.richTextBox2);
            this.groupBox4.Location = new System.Drawing.Point(644, 285);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(253, 167);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Режим Расстояние слежения";
            this.groupBox4.Visible = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(479, 387);
            this.trackBar1.Maximum = 255;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(45, 165);
            this.trackBar1.TabIndex = 21;
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(512, 387);
            this.trackBar2.Maximum = 255;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar2.Size = new System.Drawing.Size(45, 165);
            this.trackBar2.TabIndex = 22;
            this.trackBar2.Value = 1;
            this.trackBar2.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(545, 387);
            this.trackBar3.Maximum = 255;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar3.Size = new System.Drawing.Size(45, 165);
            this.trackBar3.TabIndex = 23;
            this.trackBar3.Value = 1;
            this.trackBar3.Scroll += new System.EventHandler(this.trackBar3_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "R";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(512, 371);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "G";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(548, 371);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(476, 385);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "255";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(509, 385);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "255";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(544, 385);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "255";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(479, 486);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(13, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(511, 486);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(546, 486);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Оригинальное изображение";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 296);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(174, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Изображение после фильтрации";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(339, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(110, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "Устройство захвата";
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Location = new System.Drawing.Point(206, 566);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(213, 32);
            this.saveSettingsButton.TabIndex = 37;
            this.saveSettingsButton.Text = "Сохранить";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // chkBoxSettingsMode
            // 
            this.chkBoxSettingsMode.AutoSize = true;
            this.chkBoxSettingsMode.Checked = true;
            this.chkBoxSettingsMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxSettingsMode.Location = new System.Drawing.Point(342, 312);
            this.chkBoxSettingsMode.Name = "chkBoxSettingsMode";
            this.chkBoxSettingsMode.Size = new System.Drawing.Size(117, 17);
            this.chkBoxSettingsMode.TabIndex = 38;
            this.chkBoxSettingsMode.Text = "Режим настройки";
            this.chkBoxSettingsMode.UseVisualStyleBackColor = true;
            // 
            // VideoController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 610);
            this.Controls.Add(this.chkBoxSettingsMode);
            this.Controls.Add(this.saveSettingsButton);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "VideoController";
            this.Text = "VideoController";
            this.Load += new System.EventHandler(this.VideoController_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button StartSettingButton;
        private System.Windows.Forms.Button PauseSettingButton;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdiobtnRed;
        private System.Windows.Forms.RadioButton rdiobtnBlue;
        private System.Windows.Forms.RadioButton rdiobtnTekCisimTakibi;
        private System.Windows.Forms.RadioButton rdiobtnCokCisimTakibi;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdiobtnGeoSekil;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdiobtnGreen;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.CheckBox chkboxMesafeOlcer;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.CheckBox chkboxMesafeKordinati;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.RadioButton rdiobtnHandSet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button saveSettingsButton;
        private System.Windows.Forms.CheckBox chkBoxSettingsMode;
    }
}