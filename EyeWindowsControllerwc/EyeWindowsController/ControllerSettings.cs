using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EyeWindowsController
{
    [Serializable]
    public class ControllerSettings
    {
        public string CheckSettings { get; set; }

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}
