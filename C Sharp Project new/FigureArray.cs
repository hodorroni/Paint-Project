using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Project_new
{
    [Serializable]
    class FigureArray
    {
        protected List<Figure> myList;

        public FigureArray()
        {
            myList = new List<Figure>();
        }
        //public void Add(int i,Figure other)
        //{        
        //    myList.Insert(i, other);
        //}
        public void Add(Figure other)
        {
            myList.Add(other);
        }
        public int getLength()
        {
            int x = myList.Count;
            return x;
        }
        public void Remove(int index)
        {
            myList.RemoveAt(index);
        }
        public Figure this[int index]
        {
            get
            {
                if (index >= myList.Count)
                {
                    return (Figure)null;
                }
                return (Figure)myList[index];
            }
            set
            {
                if (index <= myList.Count)
                {
                    myList[index] = value;
                }
            }

        }
        public void DrawAll(Graphics g, List<Color> l1)
        {
            Color[] p1 = l1.ToArray();
            for (int i = 0; i < myList.Count; i++)
            {
                myList[i].Draw(g, p1[i]);
            }
        }

        public void ClearAll(Graphics g, List<Color> l1)   //clears the whole figures by paiting to white and deleting their color list
        {
            for (int i = 0; i < myList.Count; i++)
            {
                myList[i].clear(g);
            }
            l1.Clear(); //clears the colors least from winform
            myList.Clear();//clears the figures list
        }
        ~FigureArray()
        {
            myList.Clear();
        }
    }

}
