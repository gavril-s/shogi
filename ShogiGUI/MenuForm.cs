using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShogiGUI
{
    public partial class MenuForm : Form
    {
        private bool destroyed = false;

        public bool Destroyed()
        {
            return destroyed;
        }

        public MenuForm()
        {
            InitializeComponent();
            ToTheGameButton.Click += ToTheGameButton_OnClick;
            RulesButton.Click += RulesButton_OnClick;
        }

        private void ToTheGameButton_OnClick(object sender, EventArgs e)
        {
            Program.GetGameForm().Show();
            this.Hide();
        }

        private void RulesButton_OnClick(object sender, EventArgs e)
        {
            Program.GetRulesForm().Show();
            this.Hide();
        }

        private void OnDestroy()
        {
            destroyed = true;
        }
    }
}
