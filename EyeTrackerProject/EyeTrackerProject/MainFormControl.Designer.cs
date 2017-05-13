namespace EyeTrackerProject
{
    partial class MainFormControl
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
            this.left_eye = new System.Windows.Forms.PictureBox();
            this.right_eye = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.left_eye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.right_eye)).BeginInit();
            this.SuspendLayout();
            // 
            // logger_tb
            // 
            this.logger_tb.Location = new System.Drawing.Point(7, 7);
            this.logger_tb.Multiline = true;
            this.logger_tb.Name = "logger_tb";
            this.logger_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logger_tb.Size = new System.Drawing.Size(357, 92);
            this.logger_tb.TabIndex = 0;
            // 
            // left_eye
            // 
            this.left_eye.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.left_eye.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.left_eye.Location = new System.Drawing.Point(370, 7);
            this.left_eye.Name = "left_eye";
            this.left_eye.Size = new System.Drawing.Size(141, 92);
            this.left_eye.TabIndex = 5;
            this.left_eye.TabStop = false;
            // 
            // right_eye
            // 
            this.right_eye.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.right_eye.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.right_eye.Location = new System.Drawing.Point(528, 7);
            this.right_eye.Name = "right_eye";
            this.right_eye.Size = new System.Drawing.Size(141, 92);
            this.right_eye.TabIndex = 6;
            this.right_eye.TabStop = false;
            // 
            // MainFormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 105);
            this.Controls.Add(this.right_eye);
            this.Controls.Add(this.left_eye);
            this.Controls.Add(this.logger_tb);
            this.Name = "MainFormControl";
            this.Text = "Главная форма";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormControl_FormClosed);
            this.Load += new System.EventHandler(this.MainFormControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.left_eye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.right_eye)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox logger_tb;
        public System.Windows.Forms.PictureBox left_eye;
        public System.Windows.Forms.PictureBox right_eye;
    }
}

