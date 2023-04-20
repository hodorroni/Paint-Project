using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Drawing.Imaging;
namespace C_Sharp_Project_new
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            colorList = new List<Color>();
            this.Width = 1400;
            this.Height = 700;
            bm = new Bitmap(pic.Width, pic.Height);
            g1 = Graphics.FromImage(bm);
            g1.Clear(Color.White);
            pic.Image = bm;
            p = new Pen(Color.Black, 6);
        }

        int circleCount = 0;
        int rhombusCount = 0;
        int ellipseCount = 0;
        int parallelogramCount = 0;
        int rectangleCount = 0;
        int squareCount = 0;
        Graphics g1;//for the pencil
        Bitmap bm;//for the pencil
        bool isInside = false;
        Point px; //for eraser and pencil to move on screen
        Point py; //for eraser and pencil to move on screen
        List<Point> pointList = new List<Point>(); //two store the two click points to draw a LINE
        bool drawOrRemove = false;
        ColorDialog cd = new ColorDialog();
        Color new_color;
        Pen p;
        FigureArray figureList = new FigureArray(); //figures list
        List<Color> colorList;   //list that saves the colors for each figure corresponding to the figure 
        int current = -1;
        int selected = 0;
        private void btn_color_Click(object sender, EventArgs e)
        {
            cd.ShowDialog();
            new_color = cd.Color;
            pic_color.BackColor = cd.Color;
            p.Color = cd.Color;
        }

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            current = -1;
            drawOrRemove = true;//set true the color select for the pencil to know if to keep drawing or not
            for (int i = 0; i < figureList.getLength(); i++)//checking if we got any shapes and if we clicked on any of them
            {
                if (figureList[i].isInside(e.X, e.Y))
                {//if clicked inside the figure
                    current = i;     //to move the objects need to save his index
                    string s = e.Button.ToString();
                    if (s == "Right")//if clicked with right button mouse to remove
                    {
                        if (figureList[i] is Ellipse)
                        {
                            ellipseCount--;
                            if (figureList[i] is Circle)
                            {
                                circleCount--;
                            }
                        }
                        if (figureList[i] is Parallelogram)
                        {
                            parallelogramCount--;
                            if (figureList[i] is Rectangle)
                            {
                                rectangleCount--;
                                if (figureList[i] is Square)
                                {
                                    squareCount--;
                                }
                            }
                            if (figureList[i] is Diamond)
                            {
                                rhombusCount--;
                            }
                        }
                        figureList.Remove(current);//remove him from the figureArray
                        colorList.RemoveAt(i);//remove the color from the list
                        current = -1;
                        pic.Invalidate();//redraw
                        data_btn.PerformClick();
                        return;

                    }
                    else if (s == "Left")//if i pressed on some figure in with left click
                    {                    
                        isInside = true; // flag if to move the object on the screen or not
                        colorList.RemoveAt(i);//remove the color from the list because i want to change the color for him
                        colorList.Insert(i, p.Color);//insert instead in the i position the current color that pressed so it will be parallel to the figures in the array
                        pic.Invalidate();
                        data_btn.PerformClick();
                        return;
                    }
                    break;
                }
            }
            if (current < 0)//add the shape
            {
                switch (selected)
                {
                    case 0:
                        colorList.Add(p.Color);//add the current color the default is black if not selected
                        figureList.Add(new Rectangle(e.X, e.Y, 30, 15));
                        parallelogramCount++;
                        rectangleCount++;
                        break;
                    case 1:
                        py = e.Location; //get the location when pressed on the screen for the pencil
                        break;
                    case 2:
                        py = e.Location;//get the location when pressed on the screen for eraser
                        break;
                    case 3:
                        colorList.Add(p.Color);
                        figureList.Add(new Ellipse(e.X, e.Y, 15));
                        ellipseCount++;
                        break;
                    case 4:  //for the Line
                        pointList.Add(e.Location);  //add the current location when pressing down the screen
                        if (pointList.Count == 2)//if 2 points in the list then i want it
                        {
                            return;//dont want to invalidate till there is at least two points in the list
                        }
                        break;
                    case 6:
                        colorList.Add(p.Color);
                        //Ellipse e1 = new Circle(e.X, e.Y, 15);
                        //Circle c1 = new Ellipse(e.X, e.Y, 15);
                        // f1.Add(j, e1);
                        figureList.Add(new Circle(e.X, e.Y, 15));
                        ellipseCount++;
                        circleCount++;
                        break;
                    case 8:
                        colorList.Add(p.Color);
                        figureList.Add(new Parallelogram(e.X, e.Y, 24, 12));
                        parallelogramCount++;
                        break;
                    case 9:
                        colorList.Add(p.Color);
                        figureList.Add(new Diamond(e.X, e.Y, 24, 24));
                        parallelogramCount++;
                        rhombusCount++;
                        break;
                    case 10:
                        colorList.Add(p.Color);
                        figureList.Add(new Square(e.X, e.Y, 20, 20));
                        parallelogramCount++;
                        squareCount++;
                        rectangleCount++;
                        break;


                }
                // current = f1.getLength() - 1;// to move the objects get the position which to move
                pic.Invalidate();//redraw again
                data_btn.PerformClick();
            }
        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            selected = 0;
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//for the objects with poly
            g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//for the objects Line pencil eraser....
            if (selected == 5)//if clear button
            {
                ellipse_count.Text = 0.ToString();
                rhombus_count.Text = 0.ToString();
                figure_count.Text = 0.ToString();
                circle_count.Text = 0.ToString();
                Parallelogram_count.Text = 0.ToString();
                rectangle_count.Text = 0.ToString();
                square_count.Text = 0.ToString();
                figureList.ClearAll(g, colorList);//use the polymorphysem to clearAll the shapes
            }
            else if (pointList.Count == 2) //drawing the Line between the two points stored in the pointList
            {
                g1.DrawLine(p, pointList[0], pointList[1]);//the two points that are stored the first clicked one and the second clicked one
                pointList.Clear();//clear the list to get another two points to draw line between them
            }
            figureList.DrawAll(g, colorList);


        }

        private void btn_pencil_Click(object sender, EventArgs e)//pencil button
        {
            selected = 1;
        }

        private void pic_MouseMove(object sender, MouseEventArgs e)  //to draw with the pencil
        {
            px = e.Location;//get the location when moving with the mouse
            if (selected == 1 && drawOrRemove)//if selected meaning i chose pencil and drawOrRemove is a flag to know if to keep drawing
            {
                g1.DrawLine(p, px, py);
                py = px;    //update py where we stopped with the mouse
            }
            else if (selected == 2 && drawOrRemove)//if eraser for the pencil
            {
                Pen p1 = new Pen(Color.White, 30);
                g1.DrawLine(p1, px, py);
                py = px;
            }
            else if (current >= 0 && isInside)//if any figures in the list and we want to move them
            {
                Figure c = figureList[current];//set the figure to the point on the screen 
                c.X = e.X;
                c.Y = e.Y;
            }
            pic.Invalidate();
           // data_btn.PerformClick();
        }

        private void btn_eraser_Click(object sender, EventArgs e)//eraser
        {
            selected = 2;
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)//finished drawing wih the pencil  then put color as false to stop drawing
        {
            current = -1;//to reset the moving of the object
            drawOrRemove = false;//reset the drawing/erasing/Line
            isInside = false; // for moving the object on the screen 
            pic.Invalidate();
            data_btn.PerformClick();
        }

        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            selected = 3;
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            selected = 4;
        }

        private void save_btn_Click(object sender, EventArgs e)     //save the draws not figures
        {

            var sfd = new SaveFileDialog();
            sfd.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                RectangleF r1 = new RectangleF(0, 0, pic.Width, pic.Height);
                Bitmap btm = bm.Clone(r1, bm.PixelFormat);
                btm.Save(sfd.FileName, ImageFormat.Jpeg);
                MessageBox.Show("Image saved Succesfully");
                // formatter.Serialize(stream, pointList);
            }
        }


        private void load_btn_Click(object sender, EventArgs e)    //loads the figures
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();// + "..\\myModels";
            openFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream stream = File.Open(openFileDialog1.FileName, FileMode.Open);
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                figureList = (FigureArray)binaryFormatter.Deserialize(stream);
                colorList = (List<Color>)binaryFormatter.Deserialize(stream);
                //  pointList = (List<Point>)binaryFormatter.Deserialize(stream);
                pictureBox1.Invalidate();
                data_btn.PerformClick();
            }
        }

        private void btn_clear_Click(object sender, EventArgs e) //clears the whole screen
        {
            selected = 5;
            g1.Clear(Color.White); //polymorphisem for the figures to clear them
            pic.Image = bm;  //for the draws
            pic.Invalidate();  //
            data_btn.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)   //figure collection save
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();// + "..\\myModels";
            saveFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, figureList);
                    formatter.Serialize(stream, colorList);
                    MessageBox.Show("Figures saved");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)   //load the draws
        {
            //var sfd = new OpenFileDialog();
            //sfd.InitialDirectory = Directory.GetCurrentDirectory();
            //sfd.Filter="Image(*.jpg)|*.jpg|(*.*|*.*";
            //sfd.FilterIndex = 1;
            //sfd.RestoreDirectory = true;
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    Stream stream = File.Open(sfd.FileName, FileMode.Open);
            //    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    RectangleF r1 = new RectangleF(0, 0, pic.Width, pic.Height);
            //    Bitmap btm = bm.Clone(r1, bm.PixelFormat);

            //}
            OpenFileDialog of = new OpenFileDialog();
            //For any other formats
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (of.ShowDialog() == DialogResult.OK)
            {
                pic.ImageLocation = of.FileName;

            }
        }

        //for the pallate color
        static Point set_point(PictureBox pb, Point pt)    //to get the location when pressing on the picture box
        {
            float pX = 1f * pb.Image.Width / pb.Width;
            float pY = 1f * pb.Image.Height / pb.Height;
            return new Point((int)(pt.X * pX), (int)(pt.Y * pY));
        }

        //private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        //{
        //    Point point = set_point(pictureBox1, e.Location);
        //    pic_color.BackColor = ((Bitmap)pictureBox1.Image).GetPixel(point.X, point.Y);
        //    new_color = pic_color.BackColor;
        //    p.Color = pic_color.BackColor;

        //}

        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)  //plate of colors
        {
            Point point = set_point(pictureBox1, e.Location);
            pic_color.BackColor = ((Bitmap)pictureBox1.Image).GetPixel(point.X, point.Y); //update the small rectangle with colo background
            new_color = pic_color.BackColor; //set the small rectangle with the background color who chose
            p.Color = pic_color.BackColor;//update the global pen to this color
        }

        private void Circle_btn_Click(object sender, EventArgs e)
        {
            selected = 6;
        }


        //from here its for the Fill Button
        private void validate(Bitmap bm, Stack<Point> sp, int x, int y, Color old_color, Color new_color)//to fill the old color shape to new one
        {
            Color cx = bm.GetPixel(x, y);
            if (cx == old_color)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, new_color);
            }
        }

        public void Fill(Bitmap bm, int x, int y, Color new_clr)
        {
            Color old_color = bm.GetPixel(x, y);
            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, new_clr);
            if (old_color == new_clr) return;
            while (pixel.Count > 0)
            {
                Point pt = (Point)pixel.Pop();
                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    p.Color = new_clr;   //@@update the global brush for the figures
                    validate(bm, pixel, pt.X - 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y - 1, old_color, new_clr);
                    validate(bm, pixel, pt.X + 1, pt.Y, old_color, new_clr);
                    validate(bm, pixel, pt.X, pt.Y + 1, old_color, new_clr);
                }
            }
        }

        private void btn_fill_Click(object sender, EventArgs e)//Fill click
        {
            selected = 7;
        }

        private void pic_MouseClick(object sender, MouseEventArgs e)
        {
            if (selected == 7 && isInside == false)  //isinside is to know if i pressed on the figure or not
            {
                Point point = set_point(pic, e.Location);
                Fill(bm, point.X, point.Y, new_color);
            }
        }

        private void button1_Click(object sender, EventArgs e)  //Parallelogram
        {
            selected = 8;
        }

        private void button4_Click(object sender, EventArgs e)//Diamond
        {
            selected = 9;
        }

        private void button5_Click(object sender, EventArgs e)//Square
        {
            selected = 10;
        }

        private void data_btn_Click(object sender, EventArgs e)
        {
            ellipse_count.Text = ellipseCount.ToString();
            rhombus_count.Text = rhombusCount.ToString();
            figure_count.Text = figureList.getLength().ToString();
            circle_count.Text = circleCount.ToString();
            Parallelogram_count.Text = parallelogramCount.ToString();
            rectangle_count.Text = rectangleCount.ToString();
            square_count.Text = squareCount.ToString();
        }
    }

}
