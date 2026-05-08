using System;
using System.Collections.Generic;
using System.Text;

namespace TripleTriad
{
    public class CardCollection
    {
        public static List<Card> AllCards(Player p) => new List<Card>
        {
            //1 Star cards
             new Card("Chocobo", 3, 2, 1, 7, 1, p),
             new Card("Moogle", 2, 3, 7, 1, 1, p),
             new Card("Coeurl", 2, 2, 5, 5, 1, p),
             new Card("Tonberry", 2, 7, 2, 2, 1, p),

             //2 Star cards
             new Card("Chimera", 7, 2, 2, 7, 2, p),
             new Card("Blue_Dragon", 2, 7, 7, 2, 2, p),
             new Card("Demon_Wall", 6, 2, 3, 7, 2, p),
             new Card("Ifrit", 7, 6, 7, 1, 2, p),

             //3 Star cards
             new Card("Garuda", 7, 1, 7, 6, 3, p),
             new Card("Y'shtola", 7, 1, 4, 8, 3, p),
             new Card("Titan", 1, 7, 6, 7, 3, p),
             new Card("Thancred", 2, 8, 7, 3, 3, p),

             //4 Star cards
             new Card("Shiva", 1, 8, 8, 8, 4, p),
             new Card("Odin", 8, 1, 8, 8, 4, p),
             new Card("UltimaWeapon", 7, 9, 1, 8, 4, p),
             new Card("Leviathan", 8, 8, 1, 8, 4, p),
        };
    }
}
