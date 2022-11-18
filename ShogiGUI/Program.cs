using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShogiGUI
{
    internal static class Program
    {
        private static GameForm gameForm;
        private static MenuForm menuForm;

        public static GameForm GetGameForm()
        {
            if (gameForm == null || gameForm.Destroyed())
            {
                gameForm = new GameForm();
            }
            return gameForm;
        }

        public static MenuForm GetMenuForm()
        {
            if (menuForm == null || menuForm.Destroyed())
            {
                menuForm = new MenuForm();
            }
            return menuForm;
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            gameForm = GetGameForm();
            menuForm = GetMenuForm();
            Application.Run(menuForm);
        }
    }
}
