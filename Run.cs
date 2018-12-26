using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CarRaceProject
{
    static class Run
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CarRaceUI());
        }
    }
}
