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
        private static RulesForm rulesForm;

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

        public static RulesForm GetRulesForm()
        {
            if (rulesForm == null || rulesForm.Destroyed())
            {
                rulesForm = new RulesForm();
            }
            return rulesForm;
        }

        public static GameEndForm GetGameEndForm(string text)
        {
            return new GameEndForm(text);
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            menuForm = GetMenuForm();
            Application.Run(menuForm);
        }
    }
}
