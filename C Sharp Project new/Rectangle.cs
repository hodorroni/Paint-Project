using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace C_Sharp_Project_new
{
    [Serializable]
    public class Rectangle : Parallelogram
    {
        public Rectangle() : this(10, 10, 15, 10)
        { }

        public Rectangle(float x, float y, float w1, float h1)
        {
            Width = w1;
            Height = h1;
            X = x;
            Y = y;
        }
        public override void Draw(Graphics g, Color p)
        {
            SolidBrush br = new SolidBrush(Color.Black);
            Pen p1 = new Pen(p, 2);
            br.Color = p;
            g.FillRectangle(br, X - Width / 2, Y - Height / 2, Width, Height);
            g.DrawRectangle(p1, X - Width / 2, Y - Height / 2, Width, Height);
        }

        public override bool isInside(double otherX, double otherY)
        {
            return Math.Abs(otherX - X) <= Width / 2 && Math.Abs(otherY - Y) <= Height / 2;
        }
        public override double getArea()
        {
            return Width * Height;
        }
        public override void clear(Graphics g)
        {
            SolidBrush br = new SolidBrush(Color.White);
            Pen p1 = new Pen(Color.White, 2);
            g.FillRectangle(br, X - Width / 2, Y - Height / 2, Width, Height);
            g.DrawRectangle(p1, X - Width / 2, Y - Height / 2, Width, Height);
        }
        ~Rectangle() { }

    }
}
