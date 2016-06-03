namespace EyeWindowsController
{
    partial class ControlPanelForm
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
            this.leftMouseClickBTN = new System.Windows.Forms.Button();
            this.DblLeftMouseClickBTN = new System.Windows.Forms.Button();
            this.RightMouseClickBTN = new System.Windows.Forms.Button();
            this.keyboard_btn = new System.Windows.Forms.Button();
            this.move_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // leftMouseClickBTN
            // 
            this.leftMouseClickBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leftMouseClickBTN.Location = new System.Drawing.Point(12, 9);
            this.leftMouseClickBTN.Name = "leftMouseClickBTN";
            this.leftMouseClickBTN.Size = new System.Drawing.Size(140, 100);
            this.leftMouseClickBTN.TabIndex = 0;
            this.leftMouseClickBTN.Text = "Левый клик";
            this.leftMouseClickBTN.UseVisualStyleBackColor = true;
            this.leftMouseClickBTN.Click += new System.EventHandler(this.leftMouseClickBTN_Click);
            this.leftMouseClickBTN.MouseEnter += new System.EventHandler(this.leftMouseClickBTN_MouseEnter);
            this.leftMouseClickBTN.MouseLeave += new System.EventHandler(this.leftMouseClickBTN_MouseLeave);
            // 
            // DblLeftMouseClickBTN
            // 
            this.DblLeftMouseClickBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DblLeftMouseClickBTN.Location = new System.Drawing.Point(169, 9);
            this.DblLeftMouseClickBTN.Name = "DblLeftMouseClickBTN";
            this.DblLeftMouseClickBTN.Size = new System.Drawing.Size(140, 100);
            this.DblLeftMouseClickBTN.TabIndex = 1;
            this.DblLeftMouseClickBTN.Text = "Двойной Левый клик";
            this.DblLeftMouseClickBTN.UseVisualStyleBackColor = true;
            this.DblLeftMouseClickBTN.Click += new System.EventHandler(this.DblLeftMouseClickBTN_Click);
            this.DblLeftMouseClickBTN.MouseEnter += new System.EventHandler(this.DblLeftMouseClickBTN_MouseEnter);
            this.DblLeftMouseClickBTN.MouseLeave += new System.EventHandler(this.DblLeftMouseClickBTN_MouseLeave);
            // 
            // RightMouseClickBTN
            // 
            this.RightMouseClickBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RightMouseClickBTN.Location = new System.Drawing.Point(328, 9);
            this.RightMouseClickBTN.Name = "RightMouseClickBTN";
            this.RightMouseClickBTN.Size = new System.Drawing.Size(140, 100);
            this.RightMouseClickBTN.TabIndex = 1;
            this.RightMouseClickBTN.Text = "Правый клик";
            this.RightMouseClickBTN.UseVisualStyleBackColor = true;
            this.RightMouseClickBTN.Click += new System.EventHandler(this.RightMouseClickBTN_Click);
            this.RightMouseClickBTN.MouseEnter += new System.EventHandler(this.DblLeftMouseClickBTN_MouseEnter);
            this.RightMouseClickBTN.MouseLeave += new System.EventHandler(this.DblLeftMouseClickBTN_MouseLeave);
            // 
            // keyboard_btn
            // 
            this.keyboard_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.keyboard_btn.Location = new System.Drawing.Point(484, 9);
            this.keyboard_btn.Name = "keyboard_btn";
            this.keyboard_btn.Size = new System.Drawing.Size(140, 100);
            this.keyboard_btn.TabIndex = 1;
            this.keyboard_btn.Text = "Клавиатура";
            this.keyboard_btn.UseVisualStyleBackColor = true;
            this.keyboard_btn.Click += new System.EventHandler(this.keyboard_btn_Click);
            this.keyboard_btn.MouseEnter += new System.EventHandler(this.DblLeftMouseClickBTN_MouseEnter);
            this.keyboard_btn.MouseLeave += new System.EventHandler(this.DblLeftMouseClickBTN_MouseLeave);
            // 
            // move_btn
            // 
            this.move_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.move_btn.Location = new System.Drawing.Point(639, 9);
            this.move_btn.Name = "move_btn";
            this.move_btn.Size = new System.Drawing.Size(140, 100);
            this.move_btn.TabIndex = 2;
            this.move_btn.Text = "Переместить";
            this.move_btn.UseVisualStyleBackColor = true;
            this.move_btn.Click += new System.EventHandler(this.move_btn_Click);
            this.move_btn.MouseEnter += new System.EventHandler(this.DblLeftMouseClickBTN_MouseEnter);
            this.move_btn.MouseLeave += new System.EventHandler(this.move_btn_MouseLeave);
            // 
            // ControlPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::EyeWindowsController.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(795, 111);
            this.Controls.Add(this.move_btn);
            this.Controls.Add(this.keyboard_btn);
            this.Controls.Add(this.RightMouseClickBTN);
            this.Controls.Add(this.DblLeftMouseClickBTN);
            this.Controls.Add(this.leftMouseClickBTN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(200, 880);
            this.Name = "ControlPanelForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ControlPanelForm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ControlPanelForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button leftMouseClickBTN;
        private System.Windows.Forms.Button DblLeftMouseClickBTN;
        private System.Windows.Forms.Button RightMouseClickBTN;
        private System.Windows.Forms.Button keyboard_btn;
        private System.Windows.Forms.Button move_btn;
    }
}