using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAppGameOfLife
{
    class Square
    {
        private Panel m_Panel;
        private bool m_alive;
        private bool m_NextState;

        public Square(Point location, int size)
        {
            // Default state. Not alive
            m_alive = false;
            m_NextState = false;

            // Setting up graphical representation
            m_Panel = new Panel();
            m_Panel.Height = size;
            m_Panel.Width = size;
            m_Panel.Location = location;
            m_Panel.BackColor = Color.White;
            m_Panel.Click += new System.EventHandler(Square_Click);
        }

        private void toggle()
        {
            if (m_alive)
            {
                this.m_Panel.BackColor = Color.White;
            }
            else
            {
                this.m_Panel.BackColor = Color.Black;
            }

            m_alive = !m_alive;
        }

        private void update()
        {
            if (m_alive)
            {
                this.m_Panel.BackColor = Color.Black;
            }
            else
            {
                this.m_Panel.BackColor = Color.White;
            }
        }

        private void Square_Click(object sender, EventArgs e)
        {
            toggle();
        }

        public void updateDisplay()
        {
            m_alive = m_NextState;
            update();
        }

        public Panel panel()
        {
            return this.m_Panel;
        }

        public bool isAlive()
        {
            return m_alive;
        }

        public void setState(bool state)
        {
            m_NextState = state;
        }

        public void setState()
        {
            m_NextState = m_alive;
        }
    }
}