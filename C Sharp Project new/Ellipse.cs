using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace C_Sharp_Project_new
{
    [Serializable]
    public class Ellipse : Figure
    {
        float radius;
        public Ellipse() : this(5, 7, 10)
        { }

        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }
        public Ellipse(float x, float y, float radius1)
        {
            radius = radius1;
            X = x;
            Y = y;
        }
        public override void Draw(Graphics g, Color p)
        {
            SolidBrush s1 = new SolidBrush(Color.Cyan);
            Pen p1 = new Pen(p, 2);
            s1.Color = p1.Color;

            g.FillEllipse(s1, X - radius, Y - radius, 2 * radius, 3 * radius);        // width  height
            g.DrawEllipse(p1, X - radius, Y - radius, 2 * radius, 3 * radius);
        }
        public override bool isInside(double otherX, double otherY)
        {
            //                 https://math.stackexchange.com/questions/76457/check-if-a-point-is-within-an-ellipse#:~:text=The%20region%20(disk)%20bounded%20by,it%20is%20outside%20the%20ellipse.


            double x = otherX;
            double h = X - radius;  //center of the ellipse x
            double y = otherY;
            double k = Y - radius; // center of the ellipse y
            double r1 = 2 * radius; // width of the rectangle
            double r2 = 3 * radius; // height of the rectangle
            double n = Math.Pow((x - h), 2) / (r1 * r1);     //website
            double m = Math.Pow((y - k), 2) / (r2 * r2);   //website
            if (n + m <= 1)
            {
                return true;
            }
            return false;
        }
        public override void clear(Graphics g)
        {
            SolidBrush s1 = new SolidBrush(Color.White);
            Pen p1 = new Pen(Color.White, 2);
            g.FillEllipse(s1, X - radius, Y - radius, 2 * radius, 3 * radius);        // width  height
            g.DrawEllipse(p1, X - radius, Y - radius, 2 * radius, 3 * radius);
        }
        public override double getArea()
        {
            return 24;
        }
        ~Ellipse() { }
    }
}
