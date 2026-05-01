using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TripleTriad
{
    public class Card
    {
        private string name;
        private ImageSource icon;

        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Left { get; private set; }
        public int Right { get; private set; }
        public string Name { get; private set; }
        //public Player.Color CardColor { get;  set; }
        public Player Owner { get; set; }
        public Card(string name, int top, int bottom, int left, int right, Player owner)
        {
            Name = name;
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
            Owner = owner;
            //CardColor = cardcolor;
        }

        public Player FlipOwner(Player p1, Player p2)
        {
            Owner = Owner == p1 ? p2 : p1;
            return Owner;
        }
    }
}
