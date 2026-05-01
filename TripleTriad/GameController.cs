using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TripleTriad
{
    /// <summary>
    /// Interaction logic for GameController.xaml
    /// </summary>
    public partial class GameController
    {
        public event Action<Card> CardFlipped;

        public Board board;
        public Player player1;
        public Player player2;
        

        //Initalize all game elements (board, players, cards)
        public GameController()
        {
            board = new Board();
            player1 = new Player(Player.Color.Blue);
            player2 = new Player(Player.Color.Red);

            LoadCards(player1);
            LoadCards(player2);
        }

        //Load playable cards into players' hands
        private void LoadCards(Player p)
        {
            if (p == player1)
            {
                Card card1 = new Card("Chocabo", 20, 2, 3, 4, p);
                addToHand(p, card1);

            }
            else
            {
                Card card1 = new Card("Black Mage", 5, 10, 3, 6, p);
                addToHand(p, card1);

            }
        }

        //Add new card to player's hand
        public void addToHand(Player p, Card c)
        {
            p.Hand.Add(c);
        }

        
        public void PlayCard(int row, int col, Card droppedCard)
        {
            Card? topAdj = null;
            Card? leftAdj = null;
            Card? rightAdj = null;
            Card? bottomAdj = null;
            Card? cardFlipped = null;

            Card card = droppedCard;

            if (row - 1 >= 0)
            {
                topAdj = board.GetCellState(row - 1, col);
            }
            if (col - 1 >= 0)
            {
                leftAdj = board.GetCellState(row, col - 1);
            }
            if (col + 1 <= 2)
            {
                rightAdj = board.GetCellState(row, col + 1);
            }
            if (row + 1 <= 2)
            {
                bottomAdj = board.GetCellState(row + 1, col);
            }

            if ((leftAdj is not null) && (leftAdj.Right < card.Left))
            {
                card.FlipOwner(card.Owner, leftAdj.Owner);
                cardFlipped = leftAdj;
            }

            if ((rightAdj is not null) && (rightAdj.Left < card.Right))
            {
                card.FlipOwner(card.Owner, rightAdj.Owner);
                cardFlipped = rightAdj;
            }

            if ((bottomAdj is not null) && (bottomAdj.Top < card.Bottom))
            {
                card.FlipOwner(card.Owner, bottomAdj.Owner);
                cardFlipped = bottomAdj;
            }

            if ((topAdj is not null) && (topAdj.Bottom < card.Top))
            {
                topAdj.Owner = topAdj.FlipOwner(card.Owner, topAdj.Owner);
                cardFlipped = topAdj;
            }

            CardFlipped?.Invoke(cardFlipped);
        }


    }
}
