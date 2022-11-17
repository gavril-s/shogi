using ShogiModel;
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
    public partial class GameForm : Form
    {
        private Game game;
        private Button[,] buttonGrid;
        public GameForm()
        {
            game = new Game();
            buttonGrid = new Button[game.Board.Size, game.Board.Size];
            InitializeComponent();
            ShowBoard();
        }

        private void ShowBoard()
        {
            int btn_height = ScreenBoard.Height / game.Board.Size;
            int btn_width = ScreenBoard.Width / game.Board.Size;

            for (int i = 0; i < game.Board.Size; i++)
            {
                for (int j = 0; j < game.Board.Size; j++)
                {
                    buttonGrid[i, j] = new Button();
                    buttonGrid[i, j].Height = btn_height;
                    buttonGrid[i, j].Width = btn_width;
                    buttonGrid[i, j].Click += ButtonGrid_OnClick;

                    ScreenBoard.Controls.Add(buttonGrid[i, j]);

                    buttonGrid[i, j].Location = new Point(i * btn_width, j * btn_height);
                    buttonGrid[i, j].Text = game.Board[i, j].Name;
                }
            }
        }

        private void ButtonGrid_OnClick(object sender, EventArgs e)
        {
           
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
