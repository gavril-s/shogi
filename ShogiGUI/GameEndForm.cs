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
    public partial class GameEndForm : Form
    {
        private bool destroyed = false;

        public bool Destroyed()
        {
            return destroyed;
        }

        public GameEndForm()
        {
            InitializeComponent();
        }

        public GameEndForm(string text)
        {
            InitializeComponent();
            MessageLabel.Text = text;
        }

        private void OnDestroy()
        {
            Program.GetGameForm().Close();
            destroyed = true;
        }
    }
}
