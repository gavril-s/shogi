using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShogiGUI
{
    internal static class Program
    {
        public static GameForm gameForm;
        public static MenuForm menuForm;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            gameForm = new GameForm();
            menuForm = new MenuForm();
            Application.Run(menuForm);
        }
    }
}
