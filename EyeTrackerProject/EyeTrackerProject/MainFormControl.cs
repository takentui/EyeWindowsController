using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gma.System.Windows;
using System.Runtime.InteropServices;


namespace EyeTrackerProject
{
    public partial class MainFormControl : Form
    {
        public int timeInterval { set { } get { return 3000; } }
        UserActivityHook actHook;
        CustomCursorForm mouse_form;
        int timing;
        CustomMouse _CM;
        Timer _timer, _progress_timer;
        public string MOUSEEVENTACTION = "LeftClick";
        ControlPanelForm _controlsForm;
        bool ActionIsEnd; //Действие совершено
        Form1 VCForm;
        public MainFormControl()
        {
            TopMost = true;
            this.Hide();
            _CM = new CustomMouse();
            _controlsForm = new ControlPanelForm();
            _controlsForm.Owner = this;
            _controlsForm.setOwner();
            _timer = new Timer();
            _progress_timer = new Timer();
            _progress_timer.Interval = (int)timeInterval / 12;
            _progress_timer.Tick += PrintProgress;
            _progress_timer.Enabled = true;
            InitializeComponent();
            mouse_form = new CustomCursorForm();
            mouse_form.Owner = this;
            VCForm = new Form1();
            VCForm.Owner = this;
            VCForm.Show();
            // mouse_form.Show();
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetCursorPos(int X, int Y);

        public void MouseMoved(object sender, MouseEventArgs e)
        {

        }

        public void setMouseCoordinates(int x, int y)
        {
            SetCursorPos(x, y);
            Point tempPoint = new Point(x, y + 15);
            mouse_form.DesktopLocation = tempPoint;
            if (_CM.MouseMoved(x, y))
            {
                refreshEventHandlers();
                // LogWrite("Обновили таймеры");
            }
            if (y < _controlsForm.Height && x > (SystemInformation.PrimaryMonitorSize.Width - _controlsForm.Width) / 2 && x < (SystemInformation.PrimaryMonitorSize.Width + _controlsForm.Width) / 2)
            {
                if (!_controlsForm.IsShowed)
                {
                    _controlsForm.Show();
                    mouse_form.Show();
                    _controlsForm.IsShowed = true;
                    LogWrite("Показываем форму ");
                }
            }
            else
            {
                if (_controlsForm.IsShowed)
                {
                    _controlsForm.Hide();
                    _controlsForm.IsShowed = false;
                    LogWrite("Скрываем форму ");
                }
            }
        }

        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            LogWrite("KeyDown   - " + e.KeyData.ToString());
        }
        enum MouseEvent
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10,
        }
        public void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            LogWrite("KeyPress  - " + e.KeyChar);
        }

        public void MyKeyUp(object sender, KeyEventArgs e)
        {
            LogWrite("KeyUp         - " + e.KeyData.ToString());
        }

        public void LogWrite(string txt)
        {
            logger_tb.AppendText("[" + DateTime.Now.ToString() + "] " + txt + Environment.NewLine);
            logger_tb.SelectionStart = logger_tb.Text.Length;
        }

        private void MainFormControl_Load(object sender, EventArgs e)
        {
            actHook = new UserActivityHook(); // crate an instance with global hooks
            // hang on events
            actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
            actHook.KeyDown += new KeyEventHandler(MyKeyDown);
            actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);
            actHook.KeyUp += new KeyEventHandler(MyKeyUp);

            actHook.Start();
        }

        private void DoClick(object sender, EventArgs e)
        {
            _CM.DoClick();
            setActionEnded(sender, e);
        }

        private void DoDBLClick(object sender, EventArgs e)
        {
            _CM.DoDBLClick();
            setActionEnded(sender, e);
        }

        private void DoRightClick(object sender, EventArgs e)
        {
            _CM.DoRightClick();
            _timer.Tick -= DoRightClick;
            _timer.Tick += DoClick;
        }

        private void DoMove(object sender, EventArgs e)
        {
            _CM.DoMoveLeftMouse();
            _timer.Tick -= DoMove;
            _timer.Tick += DoMoveRightMouse;
        }

        private void DoMoveRightMouse(object sender, EventArgs e)
        {

            _CM.DoMoveRightMouse();
            _timer.Tick -= DoMoveRightMouse;
            setActionEnded(sender, e);
        }

        private void PrintProgress(object sender, EventArgs e)
        {
            mouse_form.PrintProgress();
        }

        /// <summary>
        /// Скролл
        /// </summary>
        /// <param name="side">1 - Вверх, 0 - Вниз</param>
        public void DoScroll(int side)
        {

        }

        public void refreshEventHandlers()
        {
            mouse_form.ClearProgress();
            if (ActionIsEnd)
            {
                ActionIsEnd = false;
                LogWrite("Остановили таймеры");
                mouse_form.ClearProgress();
                mouse_form.Hide();
                _progress_timer.Stop();
                MOUSEEVENTACTION = "";
                _timer.Stop();
                _timer.Tick -= DoClick;
                _timer.Tick -= DoDBLClick;
                _timer.Tick -= DoRightClick;
                _timer.Tick -= setActionEnded;
            }
            else
            {
                _timer.Stop();
                _timer.Start();
                // _progress_timer.Stop();
                // _progress_timer.Start();
                mouse_form.ClearProgress();
            }
        }

        public void startMouseTimer()
        {
            //refreshEventHandlers();
            LogWrite("Запуск таймеров в главной форме c событием " + MOUSEEVENTACTION);
            _timer.Dispose();
            _timer = new Timer();
            _timer.Interval = timeInterval;
            switch (MOUSEEVENTACTION)
            {
                case "LeftClick":
                    {
                        _timer.Tick += DoClick;
                    }
                    break;
                case "DBLLeftClick":
                    {
                        _timer.Tick += DoDBLClick;
                    }
                    break;
                case "RightClick":
                    {
                        _timer.Tick += DoRightClick;
                    }
                    break;
                case "Move":
                    {
                        _timer.Tick += DoMove;
                    }
                    break;
            }
            //_timer.Tick += setActionEnded;
            _timer.Enabled = true;

            _timer.Start();
            mouse_form.ClearProgress();
            mouse_form.Show();
            _progress_timer.Start();
        }

        public void setActionEnded(object sender, EventArgs e)
        {
            ActionIsEnd = true;
            refreshEventHandlers();
        }

        public void printControlsForm()
        {

        }

        private void MainFormControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
