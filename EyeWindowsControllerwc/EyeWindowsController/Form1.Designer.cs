namespace EyeWindowsController
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
            this.logger_tb = new System.Windows.Forms.TextBox();
            this.videoTranslator = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.videoTranslator)).BeginInit();
            this.SuspendLayout();
            // 
            // logger_tb
            // 
            this.logger_tb.Location = new System.Drawing.Point(7, 7);
            this.logger_tb.Multiline = true;
            this.logger_tb.Name = "logger_tb";
            this.logger_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logger_tb.Size = new System.Drawing.Size(357, 238);
            this.logger_tb.TabIndex = 0;
            // 
            // videoTranslator
            // 
            this.videoTranslator.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.videoTranslator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.videoTranslator.Location = new System.Drawing.Point(370, 7);
            this.videoTranslator.Name = "videoTranslator";
            this.videoTranslator.Size = new System.Drawing.Size(320, 240);
            this.videoTranslator.TabIndex = 5;
            this.videoTranslator.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 252);
            this.Controls.Add(this.videoTranslator);
            this.Controls.Add(this.logger_tb);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.videoTranslator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox logger_tb;
        public System.Windows.Forms.PictureBox videoTranslator;
    }
}

