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
        public MenuForm()
        {
            InitializeComponent();
            ToTheGameButton.Click += ToTheGameButton_OnClick;
        }

        private void ToTheGameButton_OnClick(object sender, EventArgs e)
        {
            Program.gameForm.Show();
            this.Hide();
        }
    }
}
