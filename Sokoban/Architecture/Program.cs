using System;
using System.Windows.Forms;

namespace Sokoban
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            //Game.CreateMap();
            Application.Run(new Registration());
        }
    }
}