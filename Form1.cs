using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsAppGameOfLife
{
    public partial class Form1 : Form
    {
        Board board = null;
        Button start = null;
        Button stop = null;
        Label lblSteps = null;
        TextBox steps = null;
        int count = 32;
        int squareSize = 15;
        int topMargin = 40;

        Thread runner = null;

        public Form1()
        {
            InitializeComponent();
            GetAndSetDimensions();
        }

        private void GetAndSetDimensions()
        {
            int winWidth = (count + 1) * (squareSize + 1);
            this.Width = winWidth;
            int titleHeight = this.Height - this.ClientRectangle.Height;
            this.Height = count * (squareSize + 1) + topMargin + titleHeight;
            this.BackColor = Color.DarkGray;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            board = new Board(count, squareSize, topMargin);
            for (int i = 0; i < count * count; i++)
            {
                this.Controls.Add(board.Square(i).Panel());
            }

            CreateUI();
        }

        private void CreateUI()
        {
            start = new Button
            {
                Text = "Start",
                Height = topMargin,
                Width = 100
            };
            start.Click += Start_Click;
            this.Controls.Add(start);

            stop = new Button
            {
                Text = "Stop",
                Height = topMargin,
                Location = new Point(100, 0),
                Width = 100
            };
            stop.Click += Stop_Click;
            this.Controls.Add(stop);

            lblSteps = new Label
            {
                Height = topMargin,
                Location = new Point(200, 0),
                Width = 135,
                Text = "Number of generations:"
            };
            this.Controls.Add(lblSteps);

            steps = new TextBox
            {
                Height = topMargin,
                Location = new Point(335, 0),
                Width = 45,
                Text = "1"
            };
            this.Controls.Add(steps);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            StartSimulation();
        }

        private void StartSimulation()
        {
            if (runner == null || !runner.IsAlive)
            {
                runner = new Thread(board.Run);
                if (steps.Text == "")
                {
                    MessageBox.Show("Number of steps is empty");
                }
                else
                {
                    int s;
                    if (Int32.TryParse(steps.Text, out s))
                    {
                        runner.Start(s);
                    }
                    else
                    {
                        MessageBox.Show("Number of steps must only contain an integer!");
                    }
                }
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            EndSimulation();
        }

        private void EndSimulation()
        {
            if (runner != null && runner.IsAlive)
            {
                runner.Abort();
            }
        }
    }
}
