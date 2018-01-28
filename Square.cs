using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAppGameOfLife
{
    class Square
    {
        private Panel pPanel;
        private bool bAlive;
        private bool bNextState;

        public Square(Point location, int size)
        {
            // Default state. Not alive
            bAlive = false;
            bNextState = false;

            // Setting up graphical representation
            GeneratePanel(location, size);
            pPanel.Click += new EventHandler(Square_Click);
        }

        private void GeneratePanel(Point location, int size)
        {
            pPanel = new Panel
            {
                Height = size,
                Width = size,
                Location = location,
                BackColor = Color.White
            };
        }

        private void Toggle()
        {
            if (bAlive)
            {
                this.pPanel.BackColor = Color.White;
            }
            else
            {
                this.pPanel.BackColor = Color.Black;
            }

            bAlive = !bAlive;
        }

        private void Update()
        {
            if (bAlive)
            {
                this.pPanel.BackColor = Color.Black;
            }
            else
            {
                this.pPanel.BackColor = Color.White;
            }
        }

        private void Square_Click(object sender, EventArgs e)
        {
            Toggle();
        }

        public void UpdateDisplay()
        {
            bAlive = bNextState;
            Update();
        }

        public Panel Panel()
        {
            return this.pPanel;
        }

        public bool IsAlive()
        {
            return bAlive;
        }

        public void SetState(bool state)
        {
            bNextState = state;
        }

        public void SetState()
        {
            bNextState = bAlive;
        }
    }
}