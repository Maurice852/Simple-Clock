using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klok4
{
    /// <summary>
    /// implemntation of an analog clock
    /// </summary>
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        /// <summary>
        /// create graphics to draw, then keep the form square and
        /// then draw the face, hands and ciphers
        /// </summary>
        /// <algo>
        /// first create a bool to check if the clockback and numbers
        /// need to be drawn, this will be done when the size of the
        /// form has been changed,
        /// if the form has been changed the entire form will be cleared.
        /// </algo>

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            bool drawClock = false;
            Class1.g = this.CreateGraphics();
            if (this.Size.Height != this.Size.Width)
            {
                Class1.g.Clear(Color.White);
                this.Size = new Size(this.Size.Width, this.Size.Width);
                drawClock = true;
            }

            Class1.radius = this.Height / 3;
            Class1.Center = new Point(this.Height / 2, this.Height / 2);
            DateTime time = DateTime.Now;

            if (drawClock) Function();
            drawClock = false;


            Class1.DrawHand(time.Second, 60, 10);
            Class1.DrawHand(time.Minute, 60, 30);
            //the DrawHandHour function is only used once but created for continuity
            Class1.DrawHandHour(time.Hour, time.Minute, 12, 50);
        }

        /// <summary>
        /// draw the clockback and the figures
        /// </summary>
        private void Function()
        {
            Class1.Drawfigures(Class1.Center, Class1.radius, Class1.g);
            Class1.DrawClockBack(Class1.Center, Class1.radius, Class1.g, Class1.p1);
        }

    }
}
