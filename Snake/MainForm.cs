using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public partial class MainForm : Form
    {
        Graphics snakeGrap;
        Point beginPos;
        SnakeBody snake;
        public MainForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            snakeGrap = this.CreateGraphics();

            beginPos = new Point(this.Width / 2, this.Height / 2);

            snake = new SnakeBody(beginPos, snakeGrap);
            snake.SnakeGrowSpeed = 0.1f;
            snake.SnakeMoveSpeed = 100;

            this.timerMove.Interval = 200;
            this.timerGrow.Interval = 300;

            timerGrow.Start();
            timerMove.Start();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            snake.Draw();
        }

        private void TimerMoveSpeed_Tick(object sender, EventArgs e)
        {
            snake.Move();
            this.Refresh();
        }

        private void TimerGrow_Tick(object sender, EventArgs e)
        {
            snake.Grow();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.A:
                    snake.SnakeDirec = SnakeBody.Direction.WEST;
                    break;
                case Keys.Right:
                case Keys.D:
                    snake.SnakeDirec = SnakeBody.Direction.EAST;
                    break;
                case Keys.Up:
                case Keys.W:
                    snake.SnakeDirec = SnakeBody.Direction.NORTH;
                    break;
                case Keys.Down:
                case Keys.S:
                    snake.SnakeDirec = SnakeBody.Direction.SOUTH;
                    break;
            }
        }       
    }
}
