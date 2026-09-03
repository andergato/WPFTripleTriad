using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TripleTriad
{
    public class Card
    {
        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Left { get; private set; }
        public int Right { get; private set; }
        public string Name { get; private set; }
        public int Stars { get; private set; }
        public string ImagePath { get; set; }
        public Player Owner { get; set; }
            
        public Card(string name, int top, int bottom, int left, int right, int stars, Player owner, string imagePath = "Images/placeholder.jpg")
        {
            Name = name;
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
            Stars = stars;
            Owner = owner;
            ImagePath = imagePath;
        }

        public Player FlipOwner(Player p1, Player p2)
        {
            Owner = Owner == p1 ? p2 : p1;
            return Owner;
        }
    }
}
