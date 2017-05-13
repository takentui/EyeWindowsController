﻿namespace EyeTrackerProject
{
    partial class CustomCursorForm
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
            this.mouseProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // mouseProgress
            // 
            this.mouseProgress.Location = new System.Drawing.Point(0, 0);
            this.mouseProgress.MarqueeAnimationSpeed = 10;
            this.mouseProgress.Name = "mouseProgress";
            this.mouseProgress.Size = new System.Drawing.Size(30, 22);
            this.mouseProgress.TabIndex = 9999;
            this.mouseProgress.Click += new System.EventHandler(this.mouseProgress_Click);
            // 
            // CustomCursorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::EyeTrackerProject.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(30, 22);
            this.Controls.Add(this.mouseProgress);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(30, 22);
            this.MinimumSize = new System.Drawing.Size(30, 22);
            this.Name = "CustomCursorForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CustomCursorForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar mouseProgress;
    }
}