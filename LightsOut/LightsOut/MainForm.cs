using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class MainForm : Form
    {
        private LightsOutGame game; 
        private const int GridOffset = 25;
        private const int GridLength = 200;
        
        public MainForm()
        {
            InitializeComponent();

            game = new LightsOutGame();
        }

        private bool PlayerWon()
        {
            return game.IsGameOver();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int CellLength = GridLength / game.GridSize;

            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Brush brush;
                    Pen pen;

                    if (game.GetGridValue(r, c))
                    {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;

                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            int CellLength = GridLength / game.GridSize;
            if (e.X < GridOffset || e.X > CellLength * game.GridSize + GridOffset ||
                e.Y < GridOffset || e.Y > CellLength * game.GridSize + GridOffset)
                return;

            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;

            game.Move(r, c);

            this.Invalidate();

            if (PlayerWon())
            {
                MessageBox.Show(this, "Congratualations! You've won!", "Lights Out!",
                                   MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game.NewGame();

            this.Invalidate();
        }

        private void GameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameButton_Click(sender, e);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitButton_Click(sender, e);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }

        private void X4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.GridSize.Equals(4);
            game.NewGame();
        }

        private void X3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.GridSize.Equals(3);
            game.NewGame();
        }

        private void X5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.GridSize.Equals(5);
            game.NewGame();
        }
    }
}
