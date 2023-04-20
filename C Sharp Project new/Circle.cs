using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;
namespace C_Sharp_Project_new
{
    [Serializable]
    public class Circle : Ellipse
    {
        public Circle(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public Circle(Ellipse e1)
        {
            X = e1.X;
            Y = e1.Y;
            Radius = e1.Radius;
        }
        public override void clear(Graphics g)
        {
            SolidBrush s1 = new SolidBrush(Color.White);
            Pen p1 = new Pen(Color.White, 2);
            g.FillEllipse(s1, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
            g.DrawEllipse(p1, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
        }
        public override void Draw(Graphics g, Color p)
        {
            SolidBrush s1 = new SolidBrush(Color.Cyan);
            Pen p1 = new Pen(p, 2);
            s1.Color = p1.Color;

            g.FillEllipse(s1, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
            g.DrawEllipse(p1, X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
        }

        public override bool isInside(double otherX, double otherY)
        {
            return Math.Sqrt((otherX - X) * (otherX - X) + (otherY - Y) * (otherY - Y)) < Radius;
        }


        ~Circle() { }
    }
}
