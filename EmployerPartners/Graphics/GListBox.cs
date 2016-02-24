using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace EmployerPartners
{
        public class GListBox : ListBox
        {
            private ImageList _myImageList;
            public ImageList ImageList
            {
                get { return _myImageList; }
                set { _myImageList = value; }
            }
            public GListBox()
            {
                // Set owner draw mode
                this.DrawMode = DrawMode.OwnerDrawFixed;
            }
            protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                GListBoxItem item;
                Rectangle bounds = e.Bounds;
                Size imageSize = _myImageList.ImageSize;
                try
                {
                    item = (GListBoxItem)Items[e.Index];
                    if (item.ImageIndex != -1)
                    {
                        ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                        e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                            bounds.Left + imageSize.Width, bounds.Top);
                    }
                    else
                    {
                        e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor),
                            bounds.Left, bounds.Top);
                    }
                }
                catch
                {
                    if (e.Index != -1)
                    {
                        e.Graphics.DrawString(Items[e.Index].ToString(), e.Font,
                            new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                    }
                    else
                    {
                        e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor),
                            bounds.Left, bounds.Top);
                    }
                }
                base.OnDrawItem(e);
            }
            
        }//End of GListBox class

        // GListBoxItem class 
        public class GListBoxItem
        {
            private string _myText;
            private int _myImageIndex;
            // properties 
            public string Text
            {
                get { return _myText; }
                set { _myText = value; }
            }
            public int ImageIndex
            {
                get { return _myImageIndex; }
                set { _myImageIndex = value; }
            }
            //constructor
            public GListBoxItem(string text, int index)
            {
                _myText = text;
                _myImageIndex = index;
            }
            public GListBoxItem(string text) : this(text, -1) { }
            public GListBoxItem() : this("") { }
            public override string ToString()
            {
                return _myText;
            }
        }//End of GListBoxItem class

    public class ListBox : System.Windows.Forms.ListBox
    {
        public bool CanGrow;
        public bool GrowLeft;
        private bool IsGrown;
        public int GrowWidth;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (CanGrow && !IsGrown)
            {
                this.Width = this.Width + GrowWidth;
                IsGrown = true;
                if (GrowLeft)
                    this.Location = new Point(this.Location.X - GrowWidth, this.Location.Y);
            }
        }
        public void ResetWidthLocation()
        {
            if (IsGrown)
            {
                this.Width -= GrowWidth;
                if (GrowLeft)
                    this.Location = new Point(this.Location.X + GrowWidth, this.Location.Y);
                IsGrown = false;
            }
        }
    } 
}
