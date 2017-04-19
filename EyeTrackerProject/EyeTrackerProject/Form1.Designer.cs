namespace EyeTrackerProject
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.imageBoxFrameGrabber = new Emgu.CV.UI.ImageBox();
            this.first_eye = new System.Windows.Forms.PictureBox();
            this.sec_eye = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.eye1 = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.eye2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.first_eye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sec_eye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eye1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eye2)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBoxFrameGrabber
            // 
            this.imageBoxFrameGrabber.Location = new System.Drawing.Point(498, 12);
            this.imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            this.imageBoxFrameGrabber.Size = new System.Drawing.Size(782, 643);
            this.imageBoxFrameGrabber.TabIndex = 0;
            this.imageBoxFrameGrabber.TabStop = false;
            // 
            // first_eye
            // 
            this.first_eye.Location = new System.Drawing.Point(209, 25);
            this.first_eye.Name = "first_eye";
            this.first_eye.Size = new System.Drawing.Size(75, 59);
            this.first_eye.TabIndex = 1;
            this.first_eye.TabStop = false;
            // 
            // sec_eye
            // 
            this.sec_eye.Location = new System.Drawing.Point(300, 25);
            this.sec_eye.Name = "sec_eye";
            this.sec_eye.Size = new System.Drawing.Size(75, 59);
            this.sec_eye.TabIndex = 2;
            this.sec_eye.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 324);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // eye1
            // 
            this.eye1.Location = new System.Drawing.Point(21, 25);
            this.eye1.Name = "eye1";
            this.eye1.Size = new System.Drawing.Size(75, 59);
            this.eye1.TabIndex = 4;
            this.eye1.TabStop = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(21, 469);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(365, 45);
            this.trackBar1.TabIndex = 5;
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(21, 537);
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(365, 45);
            this.trackBar2.TabIndex = 6;
            // 
            // eye2
            // 
            this.eye2.Location = new System.Drawing.Point(111, 25);
            this.eye2.Name = "eye2";
            this.eye2.Size = new System.Drawing.Size(75, 59);
            this.eye2.TabIndex = 7;
            this.eye2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(21, 367);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(365, 57);
            this.textBox1.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 682);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.eye2);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.eye1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sec_eye);
            this.Controls.Add(this.first_eye);
            this.Controls.Add(this.imageBoxFrameGrabber);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imageBoxFrameGrabber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.first_eye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sec_eye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eye1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eye2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private Emgu.CV.UI.ImageBox imageBoxFrameGrabber;
        private System.Windows.Forms.PictureBox first_eye;
        private System.Windows.Forms.PictureBox sec_eye;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox eye1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.PictureBox eye2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
    }
}

