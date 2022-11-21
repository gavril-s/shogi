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
    public partial class RulesForm : Form
    {
        private bool destroyed = false;

        public bool Destroyed()
        {
            return destroyed;
        }

        public RulesForm()
        {
            InitializeComponent();
            RulesBox.ReadOnly = true;
        }
        private void OnDestroy()
        {
            Program.GetMenuForm().Show();
            destroyed = true;
        }
    }
}
