using System;
using System.Collections.Generic;
using System.Text;

namespace TripleTriad
{
    internal class CardCollection
    {
        public static List<Card> AllCards(Player p) => new List<Card>
        {
            //Hand 1
             new Card("Chocobo", 3, 2, 1, 7, p),
             new Card("Chimera", 7, 2, 2, 7, p),
             new Card("Blue_Dragon", 2, 7, 7, 2, p),
             new Card("Garuda", 7, 1, 7, 6, p),
             new Card("Shiva", 1, 8, 8, 8, p),
             
             

            //Hand 2
            new Card("Moogle", 2, 3, 7, 1, p),
            new Card("Demon_Wall", 6, 2, 3, 7, p),
            new Card("Ifrit", 7, 6, 7, 1, p),
            new Card("Y'shtola", 7, 1, 4, 8, p),
            new Card("Odin", 8, 1, 8, 8, p),
        };
    }
}
