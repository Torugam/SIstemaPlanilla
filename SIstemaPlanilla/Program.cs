using SistemaPlanilla.Vista;
using SIstemaPlanilla;
using System;
using System.Windows.Forms;

namespace SistemaPlanilla
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Login());
        }
    }
}