using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EyeTrackerProject
{
    public partial class CustomCursorForm : Form
    {
        public CustomCursorForm()
        {
            InitializeComponent();
        }

        public void PrintProgress() {
            if (mouseProgress.Value < 100)
                mouseProgress.Increment(mouseProgress.Step);
        }

        public void ClearProgress()
        {
            mouseProgress.Value = 0;
        }

        private void mouseProgress_Click(object sender, EventArgs e)
        {

        }

    }
}
