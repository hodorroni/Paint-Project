using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace C_Sharp_Project_new
{
    [Serializable]
    public abstract class Figure
    {
        float x;
        float y;
        double area;
        public float X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }

        }

        public float Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        // public abstract void Draw(Graphics g,Color p);//@@
        public abstract bool isInside(double otherx, double othery);
        public abstract void clear(Graphics g);

        public abstract void Draw(Graphics g, Color p);
        public abstract double getArea();
    }



}
