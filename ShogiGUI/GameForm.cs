﻿using ShogiModel;
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
        private Color defaultColor;
        private Color selectedColor;
        private Color backColor;
        private Bitmap dotImage;
        private float dotSize;
        private float pieceSize;
        private Game game;
        private Button[,] boardGrid;
        private Dictionary<Button, ToolTip> toolTips;
        private List<Button> whiteHandGrid;
        private List<Button> blackHandGrid;
        private (int x, int y)? selectedBoardCell;
        private (GameSide side, int x)? selectedHandCell;
        private (int x, int y)? targetBoardCell;
        private int buttonWidth;
        private int buttonHeight;
        private List<(int x, int y, Bitmap prevImage)> temp;
        private bool destroyed;

        public GameForm()
        { 
            InitializeComponent();
            defaultColor = Color.White;
            selectedColor = Color.Gray;
            backColor = Color.Brown;
            dotImage = ShogiGUI.Properties.Resources.ResourceManager.GetObject("Dot") as Bitmap;
            dotSize = 0.25f;
            pieceSize = 1f;
            game = new Game();
            boardGrid = new Button[game.Board.Size, game.Board.Size];
            toolTips = new Dictionary<Button, ToolTip>();
            whiteHandGrid = new List<Button>();
            blackHandGrid = new List<Button>();
            selectedBoardCell = null;
            targetBoardCell = null;
            buttonWidth = 0;
            buttonHeight = 0;
            temp = new List<(int x, int y, Bitmap prevImage)>();
            destroyed = false;

            this.BackColor = backColor;
            ShowBoard();
            ShowWhiteHand();
            ShowBlackHand();
            UpdateGameStateLabel();
        }

        public bool Destroyed()
        {
            return destroyed;
        }

        private void ClearTemp()
        {
            foreach ((int x, int y, Bitmap prevImage) in temp)
            {
                boardGrid[x, y].Image = prevImage;
            }
            temp.Clear();
        }

        private (int x, int y) NullableToNormal((int x, int y)? cell)
        {
            ValueTuple<int, int> cellTuple = (ValueTuple<int, int>)cell;
            (int x, int y) = cellTuple;
            return (x, y);
        }
        private (GameSide side, int x) NullableToNormal((GameSide side, int x)? cell)
        {
            ValueTuple<GameSide, int> cellTuple = (ValueTuple<GameSide, int>)cell;
            (GameSide side, int x) = cellTuple;
            return (side, x);
        }

        private void UpdateToolTip(Button button, string tip)
        {
            if (!toolTips.ContainsKey(button))
            {
                toolTips.Add(button, new ToolTip());
            }
            toolTips[button].SetToolTip(button, tip);
            
        }

        private void UpdateGameStateLabel(object sender=null, EventArgs e=null)
        {
            GameStateLabel.Text = game.CurrentMessage();
        }

        private Bitmap GetImageForButton(string name, int? width=null, int? height=null)
        {
            if (width == null || height == null)
            {
                width = buttonWidth;
                height = buttonHeight;
            }
            Bitmap image = ShogiGUI.Properties.Resources.ResourceManager.GetObject(name) as Bitmap;
            if (image != null)
            {
                Bitmap resizedImage = new Bitmap(
                    image,
                    new Size((int)width, (int)height));
                return resizedImage;
            }
            return null;
        }

        private void ShowBoard()
        {
            buttonWidth = ScreenBoard.Width / game.Board.Size;
            buttonHeight = ScreenBoard.Height / game.Board.Size;

            for (int i = 0; i < game.Board.Size; i++)
            {
                for (int j = 0; j < game.Board.Size; j++)
                {  
                    boardGrid[i, j] = new Button();
                    Button currentButton = boardGrid[i, j];
                    currentButton.Height = buttonHeight;
                    currentButton.Width = buttonWidth;
                    currentButton.Click += BoardGridButton_OnClick;
                    currentButton.Click += UpdateGameStateLabel;
                    currentButton.Location = new Point(j * buttonWidth, i * buttonHeight);
                    currentButton.BackColor = defaultColor;
                    currentButton.Tag = (i, j);
                    currentButton.TextImageRelation = TextImageRelation.Overlay;
                    currentButton.ImageAlign = ContentAlignment.MiddleCenter;

                    currentButton.Image = GetImageForButton(game.Board.Name(i, j),
                        (int)(buttonWidth * pieceSize), (int)(buttonHeight * pieceSize));
                    
                    ScreenBoard.Controls.Add(currentButton);
                    UpdateToolTip(currentButton, game.Board.NameRUS(i, j));
                }
            }
        }

        private void ShowWhiteHand()
        {
            int handSize = (WhiteHand.Width / buttonWidth) * (WhiteHand.Height / buttonHeight);
            for (int i = 0; i < handSize; i++)
            {
                Button currentButton = new Button();
                currentButton.Height = buttonHeight;
                currentButton.Width = buttonWidth;
                currentButton.Click += HandButton_OnClick;

                WhiteHand.Controls.Add(currentButton);

                currentButton.Location = new Point(
                    (i % (WhiteHand.Width / buttonWidth)) * buttonWidth,
                    (i / (WhiteHand.Width / buttonWidth)) * buttonHeight);
                currentButton.BackColor = defaultColor;
                currentButton.Text = "";
                currentButton.Tag = (GameSide.White, i);
                whiteHandGrid.Add(currentButton);
            }
        }

        private void ShowBlackHand()
        {
            int handSize = (BlackHand.Width / buttonWidth) * (BlackHand.Height / buttonHeight);
            for (int i = 0; i < handSize; i++)
            {
                Button currentButton = new Button();
                currentButton.Height = buttonHeight;
                currentButton.Width = buttonWidth;
                currentButton.Click += HandButton_OnClick;

                BlackHand.Controls.Add(currentButton);

                currentButton.Location = new Point(
                    (i % (BlackHand.Width / buttonWidth)) * buttonWidth,
                    (i / (BlackHand.Width / buttonWidth)) * buttonHeight);
                currentButton.BackColor = defaultColor;
                currentButton.Text = "";
                currentButton.Tag = (GameSide.Black, i);
                blackHandGrid.Add(currentButton);
            }
        }

        private Button GetHandCellButton((GameSide side, int x)? cell)
        {
            (GameSide side, int x) = NullableToNormal(cell);
            if (side == GameSide.White)
            {
                return whiteHandGrid[x];
            }
            else if (side == GameSide.Black)
            {
                return blackHandGrid[x];
            }

            return null;
        }

        private void SetSelectedBoardCell((int x, int y)? cell)
        {
            if (cell == null)
            {
                if (selectedBoardCell != null)
                {
                    (int x, int y) selectedBoardCellMod = NullableToNormal(selectedBoardCell);
                    boardGrid[selectedBoardCellMod.x, selectedBoardCellMod.y].BackColor = defaultColor;
                }
                selectedBoardCell = cell;
            }
            else
            {
                SetSelectedBoardCell(null);
                (int x, int y) cellMod = NullableToNormal(cell);
                if (!game.Board.IsFree(cellMod.x, cellMod.y))
                {
                    boardGrid[cellMod.x, cellMod.y].BackColor = selectedColor;
                    selectedBoardCell = cell;
                }
            }
        }

        private void SetSelectedHandCell((GameSide side, int x)? cell)
        { 
            if (cell == null)
            {
                if (selectedHandCell != null)
                {
                    Button selectedHandCellButton = GetHandCellButton(selectedHandCell);

                    if (selectedHandCellButton != null)
                    {
                        selectedHandCellButton.BackColor = defaultColor;
                    }
                }
                selectedHandCell = cell;
            }
            else
            {
                SetSelectedHandCell(null);
                Button cellButton = GetHandCellButton(cell);
                if (cellButton != null)
                {
                    cellButton.BackColor = selectedColor;
                }
                selectedHandCell = cell;
            }
        }
        private void SetTargetBoardCell((int x, int y)? cell)
        {
            if (cell == null)
            {
                if (targetBoardCell != null)
                {
                    (int x, int y) targetBoardCellMod = NullableToNormal(targetBoardCell);
                    boardGrid[targetBoardCellMod.x, targetBoardCellMod.y].BackColor = defaultColor;
                }
                targetBoardCell = cell;
            }
            else
            {
                SetTargetBoardCell(null);
                (int x, int y) cellMod = NullableToNormal(cell);
                boardGrid[cellMod.x, cellMod.y].BackColor = defaultColor;
                targetBoardCell = cell;
            }
        }

        private void SwapBoardCells((int x, int y) first, (int x, int y) second)
        {
            Button firstBtn = boardGrid[first.x, first.y];
            Button secondBtn = boardGrid[second.x, second.y];
            (firstBtn, secondBtn) = (secondBtn, firstBtn);
            (firstBtn.Tag, secondBtn.Tag) = (secondBtn.Tag, firstBtn.Tag);
            (firstBtn.Location, secondBtn.Location) = (secondBtn.Location, firstBtn.Location);
            boardGrid[first.x, first.y] = firstBtn;
            boardGrid[second.x, second.y] = secondBtn;
        }

        private void SwapBoardAndHandCells((int x, int y) board, (GameSide side, int x) hand)
        {
            Button firstBtn = GetHandCellButton(hand);
            Button secondBtn = boardGrid[board.x, board.y];
            (firstBtn, secondBtn) = (secondBtn, firstBtn);
            (firstBtn.Tag, secondBtn.Tag) = (secondBtn.Tag, firstBtn.Tag);
            (firstBtn.Location, secondBtn.Location) = (secondBtn.Location, firstBtn.Location);
            firstBtn.Click -= BoardGridButton_OnClick;
            firstBtn.Click += HandButton_OnClick;
            secondBtn.Click -= HandButton_OnClick;
            secondBtn.Click += BoardGridButton_OnClick;

            boardGrid[board.x, board.y] = secondBtn;
            ScreenBoard.Controls.Add(secondBtn);
            ScreenBoard.Controls.Remove(firstBtn);
            if (hand.side == GameSide.White)
            {
                whiteHandGrid[hand.x] = firstBtn;
                WhiteHand.Controls.Add(firstBtn);
                WhiteHand.Controls.Remove(secondBtn);
            }
            else if (hand.side == GameSide.Black)
            {
                blackHandGrid[hand.x] = firstBtn;
                BlackHand.Controls.Add(firstBtn);
                BlackHand.Controls.Remove(secondBtn);
            }
        }

        private (GameSide side, int x)? GetFreeHandCell(GameSide? side)
        {
            if (side == null)
            {
                return null;
            }

            List<Button> hand = (side == GameSide.White) ? whiteHandGrid : blackHandGrid;
            int i = 0;
            while (i < hand.Count)
            {
                if (hand[i].Image == null)
                {
                    return ((GameSide)side, i);
                }
                i++;
            }
            return null;
        }

        private void MoveCell((int x, int y)? nullableFrom, (int x, int y)? nullableTo)
        {
            (int x, int y) from = NullableToNormal(nullableFrom);
            (int x, int y) to = NullableToNormal(nullableTo);
            SwapBoardCells(from, to);
            (GameSide side, int x)? freeHandCell = GetFreeHandCell(game.Board.Side(to.x, to.y));
            if (freeHandCell != null)
            {
                (GameSide side, int x) freeHandCellMod = NullableToNormal(freeHandCell);
                SwapBoardAndHandCells(from, freeHandCellMod);
                string newImageName = game.Hand(freeHandCellMod.side).Name(freeHandCellMod.x);
                GetHandCellButton(freeHandCell).Image = GetImageForButton(newImageName, 
                    (int)(buttonWidth * pieceSize), (int)(buttonHeight * pieceSize));
                UpdateToolTip(GetHandCellButton(freeHandCell), game.Hand(freeHandCellMod.side).NameRUS(freeHandCellMod.x));
            }
        }

        private void DropCell((GameSide side, int x)? nullableFrom, (int x, int y)? nullableTo)
        {
            (GameSide side, int x) from = NullableToNormal(nullableFrom);
            (int x, int y) to = NullableToNormal(nullableTo);

            SwapBoardAndHandCells(to, from);
        }

        private void PromoteCell((int x, int y) cell)
        {
            string newImageName = game.Board.Name(cell.x, cell.y);
            boardGrid[cell.x, cell.y].Image = GetImageForButton(newImageName,
                (int)(buttonWidth * pieceSize), (int)(buttonHeight * pieceSize));
            UpdateToolTip(boardGrid[cell.x, cell.y], game.Board.NameRUS(cell.x, cell.y));
        }

        private void BoardGridButton_OnClick(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender;
            ValueTuple<int, int> buttonTag = (ValueTuple<int, int>)pressedButton.Tag;

            (int x, int y) pressedCell = buttonTag;

            ClearTemp();
            if (selectedHandCell != null)
            {
                SetTargetBoardCell(pressedCell);
                (GameSide side, int x) selectedHand = NullableToNormal(selectedHandCell);
                bool droped = game.Drop(
                    selectedHand.side,
                    selectedHand.x,
                    targetBoardCell ?? (0, 0));
                if (droped)
                {
                    DropCell(selectedHandCell, targetBoardCell);
                }
                SetSelectedBoardCell(null);
            }
            else if (selectedBoardCell == null)
            {
                ShowMoves(game.AvailableMoves(pressedCell.x, pressedCell.y));
                SetSelectedBoardCell(pressedCell);
            }
            else if (pressedCell == selectedBoardCell)
            {
                bool promoted = game.Promote(pressedCell.x, pressedCell.y);
                if (promoted)
                {
                    PromoteCell(pressedCell);
                }
                SetSelectedBoardCell(null);
            }
            else if (game.Board.OneSideStrict(selectedBoardCell ?? (0, 0), pressedCell))
            {
                ShowMoves(game.AvailableMoves(pressedCell.x, pressedCell.y));
                SetSelectedBoardCell(pressedCell);
            }
            else
            {
                SetTargetBoardCell(pressedCell);
                bool moved = game.Move(selectedBoardCell ?? (0, 0), targetBoardCell ?? (0, 0));
                if (moved)
                {
                    MoveCell(selectedBoardCell, targetBoardCell);
                }
                SetSelectedBoardCell(null);
            }
            SetSelectedHandCell(null);
            SetTargetBoardCell(null);
        }

        private void HandButton_OnClick(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender;
            ValueTuple<GameSide, int> buttonTag = (ValueTuple<GameSide, int>)pressedButton.Tag;

            (GameSide side, int x) pressedCell = buttonTag;
            ClearTemp();
            SetSelectedHandCell(buttonTag);
        }

        private void ShowMoves(List<(int x, int y)> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].x >= 0 && cells[i].x < boardGrid.GetLength(0) && 
                    cells[i].y >= 0 && cells[i].y < boardGrid.GetLength(1))
                {
                    Bitmap prevImage = null;
                    if (boardGrid[cells[i].x, cells[i].y].Image != null)
                    {
                        prevImage = boardGrid[cells[i].x, cells[i].y].Image.Clone() as Bitmap;
                    }

                    boardGrid[cells[i].x, cells[i].y].Image = new Bitmap(
                        dotImage,
                        new Size(
                            (int)(buttonWidth * dotSize),
                            (int)(buttonHeight * dotSize)));
                    temp.Add((cells[i].x, cells[i].y, prevImage));
                }
            }
        }
    }
}
