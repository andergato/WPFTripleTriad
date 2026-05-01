using System;
using System.Collections.Generic;
using System.Text;

namespace TripleTriad
{
    public class Player
    {
        public enum Color
        {
            Red,
            Blue
        }
        public static Player create()
        {
            return new Player();
        }

        public List<Card> Hand { get; set; } = new List<Card>();
        public List<Card> Claimed { get; set; } = new List<Card>();
        public Color PlayerColor { get; set; }
    }
}
