using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace C_Sharp_Project_new
{
    [Serializable]
    public class Parallelogram : Figure
    {
        float width;
        float height;


        public Parallelogram() : this(5, 10, 24, 12) { }

        public Parallelogram(float x, float y, float w1, float h1)
        {
            width = w1;
            height = h1;
            X = x;
            Y = y;
        }
        //public Parallelogram()
        public Parallelogram(Point[] arr)
        {
            X = arr[0].X;
            Y = arr[0].Y;
            width = arr[1].X - X;
            height = (Y - arr[2].Y);
        }

        public float Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
            }
        }
        public float Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
            }
        }

        //https://stackoverflow.com/questions/4243042/c-sharp-point-in-polygon
        public override bool isInside(double otherX, double otherY)         //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@                          //@@@@ need to fix
        {
            PointF[] polygon = new PointF[4];
            Point s1 = new Point((int)X - 12, (int)Y + 6);
            Point s2 = new Point(s1.X + 30, s1.Y);
            Point s3 = new Point(s1.X + 24, s1.Y - 12);
            Point s4 = new Point(s1.X - 6, s1.Y - 12);
            Point[] p3 = new Point[4];
            polygon[0] = s1;
            polygon[1] = s2;
            polygon[2] = s3;
            polygon[3] = s4;
            int i, j;
            int nvert = polygon.Length;
            bool c = false;
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                if (((polygon[i].Y > otherY) != (polygon[j].Y > otherY)) &&
                 (otherX < (polygon[j].X - polygon[i].X) * (otherY - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                    c = !c;
            }
            return c;
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

        public override void Draw(Graphics g, Color p)
        {
            Point s1 = new Point((int)X - 12, (int)Y + 6);
            Point s2 = new Point(s1.X + 30, s1.Y);
            Point s3 = new Point(s1.X + 24, s1.Y - 12);
            Point s4 = new Point(s1.X - 6, s1.Y - 12);
            Point[] p3 = new Point[4];
            p3[0] = s1;
            p3[1] = s2;
            p3[2] = s3;
            p3[3] = s4;
            SolidBrush br = new SolidBrush(Color.Black);
            Pen p2 = new Pen(p, 2);
            br.Color = p;
            g.FillPolygon(br, p3);
            g.DrawPolygon(p2, p3);
        }


        ~Parallelogram() { }




    }
}
