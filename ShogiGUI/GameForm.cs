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
        private List<(int x, int y, string prevText)> temp;
        private (int x, int y)? selectedCell;
        private (int x, int y)? targetCell;
        public GameForm()
        { 
            InitializeComponent();
            game = new Game();
            buttonGrid = new Button[game.Board.Size, game.Board.Size];
            temp = new List<(int x, int y, string prevText)>();
            selectedCell = null;
            targetCell = null;
            ShowBoard();
        }
        private void ClearTemp()
        {
            foreach ((int x, int y, string prevText) in temp)
            {
                buttonGrid[x, y].Text = prevText;
            }
            temp.Clear();
        }

        private (int x, int y) NullableToNormal((int x, int y)? cell)
        {
            ValueTuple<int, int> cellTuple = (ValueTuple<int, int>)cell;
            (int x, int y) = cellTuple;
            return (x, y);
        }
        private void ShowBoard()
        {
            int buttonHeight = ScreenBoard.Height / game.Board.Size;
            int buttonWidth = ScreenBoard.Width / game.Board.Size;
            //Console.WriteLine(buttonWidth);
            //Console.WriteLine(buttonHeight);

            for (int i = 0; i < game.Board.Size; i++)
            {
                for (int j = 0; j < game.Board.Size; j++)
                { 
                    buttonGrid[i, j] = new Button();
                    Button currentButton = buttonGrid[i, j];
                    currentButton.Height = buttonHeight;
                    currentButton.Width = buttonWidth;
                    currentButton.Click += GridButton_OnClick;

                    ScreenBoard.Controls.Add(currentButton);

                    Bitmap image = ShogiGUI.Properties.Resources.ResourceManager.GetObject("Pawn") as Bitmap;
                    Bitmap resizedImage = new Bitmap(image, new Size(buttonWidth / 2, buttonHeight / 2));
                    currentButton.Location = new Point(j * buttonWidth, i * buttonHeight);
                    currentButton.BackColor = Color.Green;
                    //currentButton.Image = resizedImage;
                    //currentButton.ImageAlign = ContentAlignment.MiddleCenter;
                    currentButton.Text = game.Board.Name(i, j);
                    currentButton.Tag = (i, j);  
                }
            }
        }

        private void setSelectedCell((int x, int y)? cell)
        {
            if (cell == null)
            {
                if (selectedCell != null)
                {
                    (int x, int y) selectedCellMod = NullableToNormal(selectedCell);
                    buttonGrid[selectedCellMod.x, selectedCellMod.y].BackColor = Color.Green;
                }
                selectedCell = cell;
            }
            else
            {
                (int x, int y) cellMod = NullableToNormal(cell);
                buttonGrid[cellMod.x, cellMod.y].BackColor = Color.Green;
                selectedCell = cell;
            }
        }

        private void setTargetCell((int x, int y)? cell)
        {
            if (cell == null)
            {
                if (targetCell != null)
                {
                    (int x, int y) targetCellMod = NullableToNormal(targetCell);
                    buttonGrid[targetCellMod.x, targetCellMod.y].BackColor = Color.Green;
                }
                targetCell = cell;
            }
            else
            {
                (int x, int y) cellMod = NullableToNormal(cell);
                buttonGrid[cellMod.x, cellMod.y].BackColor = Color.LightGreen;
                targetCell = cell;
            }
        }

        private void SwapCells((int x, int y) first, (int x, int y) second)
        {
            Button firstBtn = buttonGrid[first.x, first.y];
            Button secondBtn = buttonGrid[second.x, second.y];
            (firstBtn, secondBtn) = (secondBtn, firstBtn);
            (firstBtn.Tag, secondBtn.Tag) = (secondBtn.Tag, firstBtn.Tag);
            (firstBtn.Location, secondBtn.Location) = (secondBtn.Location, firstBtn.Location);
            buttonGrid[first.x, first.y] = firstBtn;
            buttonGrid[second.x, second.y] = secondBtn;
        }

        private void MoveCell((int x, int y)? nullableFrom, (int x, int y)? nullableTo)
        {
            (int x, int y) from = NullableToNormal(nullableFrom);
            (int x, int y) to = NullableToNormal(nullableTo);
            SwapCells(from, to);
            buttonGrid[from.x, from.y].Text = "";
        }

        private void GridButton_OnClick(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender;
            ValueTuple<int, int> buttonTag = (ValueTuple<int, int>)pressedButton.Tag;

            (int x, int y) pressedCell = buttonTag;

            ClearTemp();
            if (selectedCell == null)
            {
                ShowMoves(game.Board.AvailableMoves(pressedCell.x, pressedCell.y));
                setSelectedCell(pressedCell);
            }
            else if (game.Board.OneSideStrict(selectedCell ?? (0, 0), pressedCell))
            {
                ShowMoves(game.Board.AvailableMoves(pressedCell.x, pressedCell.y));
                setSelectedCell(pressedCell);
            }
            else
            {
                setTargetCell(pressedCell);
                bool moved = game.Move(selectedCell ?? (0, 0), targetCell ?? (0, 0));
                if (moved)
                {
                    MoveCell(selectedCell, targetCell);
                }
                setSelectedCell(null);
                setTargetCell(null);
            }
        }

        private void ShowMoves(List<(int x, int y)> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].x >= 0 && cells[i].x < buttonGrid.GetLength(0) && 
                    cells[i].y >= 0 && cells[i].y < buttonGrid.GetLength(1))
                {
                    string prevText = (string)buttonGrid[cells[i].x, cells[i].y].Text.Clone();
                    buttonGrid[cells[i].x, cells[i].y].Text = "Legal";
                    temp.Add((cells[i].x, cells[i].y, prevText));
                }
            }
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
