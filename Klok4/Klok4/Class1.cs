using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klok4
{
    class Class1
    {
        /// <summary>
        /// create the pens, graphics, center point, font and the radius
        /// </summary>
        public static Pen p1 = new Pen(Color.Black, 5);
        public static Pen p2 = new Pen(Color.White, 5);

        public static Graphics g;
        public static Font font = new Font("Arial", 16);
        public static Point Center = new Point(0, 0);
        public static int radius;


        /// <summary>
        /// draw the circular back of the clock.
        /// </summary>
        /// <algo>
        /// first calculate the upper right corner of a box in which the circle will exactly fit, so you can supply that,
        /// with the width and height of the circle to
        /// the draw ellips function so it can draw a cirle.
        /// </algo>
        public static void DrawClockBack(Point center, int radius, Graphics g, Pen p1)
        {
            Point CornerCircle = new Point(center.X - radius, center.Y - radius);
            g.DrawEllipse(p1, CornerCircle.X, CornerCircle.Y, radius * 2, radius * 2);
        }


        /// <summary>
        /// draw the hand of clock,
        /// we calculate the angle by multiplying the time by 2PI,
        /// divided by the amount of positions there are and the 
        /// offset is the percentage to shorten the length of the hand
        /// 
        /// basically a central place to start all processes needed to draw the hand of the circle,
        /// first calculate the angle of the line to delete the previous hand,
        /// then draw that line,
        /// then calculate the angle of the new hand,
        /// then draw that hand
        /// </summary>


        internal static void DrawHand(int time, int AmountPos, int OffSet)
        {
            double DelAngle = (time - 1) * (2 * Math.PI / AmountPos);
            Class1.DeletePrevHand(Class1.Center, Class1.radius, OffSet, Class1.g, DelAngle);
            double Angle = time * (2 * Math.PI / AmountPos);
            Class1.DrawHandLine(Class1.Center, Class1.radius, OffSet, Class1.g, Angle);
        }

        /// <summary>
        /// same as the drawhand function but here a extra process is needed to make the hand for hours,
        /// creep along based on the minutes.
        /// but same as the drawhand function it first calculates the angle of the previous hand,
        /// then it draws that line,
        /// then it calculates the angle of the current hand,
        /// and then draws that line.
        /// </summary>
        /// <algo>
        /// it calculates the angle by multiplying the hours by the rusult of two times PI,
        /// divided by the amount of positions given. then adding that up with the result of
        /// the minutes times the result of PI divided by 30, divided by the amount of positions given.
        /// and for the angle of the previous we take 1 off the value of the hours and minutes because
        /// that is of the previous position.
        /// </algo>
        internal static void DrawHandHour(double Hours, double minutes, int AmountPos, int OffSet)
        {
            double DelAngle = (Hours - 1 * (2 * Math.PI / AmountPos)) + (minutes - 1 * ((Math.PI / 30) / AmountPos));
            Class1.DeletePrevHand(Class1.Center, Class1.radius, OffSet, Class1.g, DelAngle);
            double Angle = (Hours * (2 * Math.PI / AmountPos)) + ( minutes * (Math.PI / 30) / AmountPos);
            Class1.DrawHandLine(Class1.Center, Class1.radius, OffSet, Class1.g, Angle);
        }

        /// <summary>
        /// draw the hands of the clock specified by the time unit provided.
        /// </summary>
        /// <algo>
        /// first calculate the length, this is the radius minus the percentage given when calling to the function,
        /// then calculate the coordinate with cosinus to which the drawfunction will draw the line to,
        /// before drawing the hand, call to the DeletePrevHand function to draw a line over the previous line in the same colour as the background
        /// </algo>
        public static void DrawHandLine(Point center, int radius, int offset, Graphics g, double radial)
        {
            int DrawRadius = radius - (radius * offset / 100);
            double HandX = center.X + (Convert.ToDouble(DrawRadius) * Math.Sin(radial));
            double HandY = center.Y - (Convert.ToDouble(DrawRadius) * Math.Cos(radial));
            Point Hand = new Point((int)HandX, (int)HandY);

            g.DrawLine(p1, center, Hand);
        }
        /// <summary>
        /// delete the previous hand by drawing another line over the previous in the same color as the background,
        /// this is done the same way as the drawhand function
        /// </summary>
        internal static void DeletePrevHand(Point center, int radius, int offset, Graphics g, double radial)
        {
            int DrawRadius2 = radius - (radius * offset / 100);
            double HandX = center.X + (Convert.ToDouble(DrawRadius2) * Math.Sin(radial));
            double HandY = center.Y - (Convert.ToDouble(DrawRadius2) * Math.Cos(radial));
            Point Hand = new Point((int)HandX, (int)HandY);

            g.DrawLine(p2, center, Hand);
        }
        
        /// <summary>
        /// draw the figures of the 12 figures of the hand
        /// </summary>
        /// <algo>
        /// do this by using the same cosinus calculations as the drawhand funtion but this one is repeated 12 times,
        /// and the circumference of the circle is divided by 12 instead of 60,
        /// also the radius is increased to place the numbers outside of the clockback,
        /// and the coordinates for the drawstring function are decreased to place the figures in a better place.
        /// </algo>
        internal static void Drawfigures(Point center, int radius, Graphics g)
        {
            radius += 20;
            SolidBrush b1 = new SolidBrush(Color.Black);
            for (int i = 0; i < 12; i++)
            {
                double radial = (i + 1) * (2 * Math.PI / 12);
                double HandX = center.X + (Convert.ToDouble(radius) * Math.Sin(radial));
                double HandY = center.Y - (Convert.ToDouble(radius) * Math.Cos(radial));
                Point Hand = new Point((int)HandX, (int)HandY);

                SizeF a = g.MeasureString((i + 1).ToString(), font);
                a.Height /= 2;
                a.Width /= 2;

                g.DrawString((i + 1).ToString(), font, b1, Hand.X - a.Width, Hand.Y - a.Height);
            }
        }
    }
}
