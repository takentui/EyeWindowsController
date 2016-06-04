using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;


namespace EyeWindowsController
{
    public partial class ControlPanelForm : Form
    {
        int HEIGHT = SystemInformation.PrimaryMonitorSize.Height / 10;
        int WIDTH = SystemInformation.PrimaryMonitorSize.Width/ 2;
        public bool IsShowed = false;
        public ControlPanelForm()
        {
            InitializeComponent();
            this.Width = WIDTH;
            this.Height = Height;
            this.Left = SystemInformation.PrimaryMonitorSize.Width / 2 - WIDTH / 2;
            this.Top = 0;
            this.FormBorderStyle = FormBorderStyle.None;
            this.AllowTransparency = true;
            this.BackColor = Color.AliceBlue;//цвет фона  
            this.TransparencyKey = this.BackColor;//он же будет заменен на прозрачный цвет

        }
        MainFormControl main;
        Timer _timer;
        bool ActionIsSelected;

        public void setOwner()
        {
            main = (MainFormControl)this.Owner as MainFormControl;
        }
        private void leftMouseClickBTN_MouseEnter(object sender, EventArgs e)
        {
            SetClick();
        }

        public void refreshTimer()
        {
            if (!ActionIsSelected)
            {
                main.MOUSEEVENTACTION = "";
                main.setActionEnded(null, null);
            }
        }

        private void DblLeftMouseClickBTN_MouseEnter(object sender, EventArgs e)
        {
            SetClick();
        }

        private void DblLeftMouseClickBTN_MouseLeave(object sender, EventArgs e)
        {
            refreshTimer();
        }

        private void leftMouseClickBTN_MouseLeave(object sender, EventArgs e)
        {
            refreshTimer();
        }

        private void RightMouseClickBTN_MouseLeave(object sender, EventArgs e)
        {
            refreshTimer();
        }

        private void keyboard_btn_MouseLeave(object sender, EventArgs e)
        {
            refreshTimer();
        }

        private void move_btn_MouseLeave(object sender, EventArgs e)
        {
            refreshTimer();
        }

        private void SetClick()
        {
            main.MOUSEEVENTACTION = "LeftClick";
            main.startMouseTimer();
            ActionIsSelected = false;
        }

        private void leftMouseClickBTN_Click(object sender, EventArgs e)
        {
            ActionIsSelected = true;
            main.MOUSEEVENTACTION = "LeftClick";
            main.startMouseTimer();
            this.Hide();
        }

        private void DblLeftMouseClickBTN_Click(object sender, EventArgs e)
        {
            ActionIsSelected = true;
            main.MOUSEEVENTACTION = "DBLLeftClick";
            main.startMouseTimer();
            this.Hide();
        }

        private void RightMouseClickBTN_Click(object sender, EventArgs e)
        {
            ActionIsSelected = true;
            main.MOUSEEVENTACTION = "RightClick";
            main.startMouseTimer();
            this.Hide();
        }

        private void keyboard_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Process.GetProcessesByName("osk").Any())
                {
                    var path64 = @"C:\Windows\winsxs\amd64_microsoft-windows-osk_31bf3856ad364e35_6.1.7600.16385_none_06b1c513739fb828\osk.exe";
                    var path32 = @"C:\windows\system32\osk.exe";
                    var path = (Environment.Is64BitOperatingSystem) ? path64 : path32;
                    Process.Start(path);
                }
            }
            catch (Exception exc)
            {
                main.LogWrite("Ошибка запуска Клавиатуры " + exc);
            }
        }

        private void move_btn_Click(object sender, EventArgs e)
        {
            ActionIsSelected = true;
            main.MOUSEEVENTACTION = "Move";
            main.startMouseTimer();
            this.Hide();
        }

        private void ControlPanelForm_Load(object sender, EventArgs e)
        {

        }
    }
}
