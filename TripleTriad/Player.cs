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
        public List<Card> Hand { get; set; } = new List<Card>();

        public Player.Color PlayerColor { get; set; }

        public Player(Player.Color pc)
        {
            PlayerColor = pc;
        }       
    }
}
