using System;
using System.Collections;
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
        public event Action<Player> GameOver;

        public Board board;
        public Player player1;
        public Player player2;
        public Player lastTurn; // Last player who took a turn
        static List<Card> allCards;
        //Initalize all game elements (board, players, cards)
        public GameController()
        {
            board = new Board();
            player1 = new Player(Player.Color.Blue);
            player2 = new Player(Player.Color.Red);
            lastTurn = player2;
            LoadCards(player1);
            LoadCards(player2);
        }

        //Load playable cards into players' hands
        private void LoadCards(Player p)
        {
            //if (p == player1)
            //{
                //Card card1 = new Card("Chocabo", 3, 2, 1, 7, p);
                //addToHand(p, card1);
                //Card card2 = new Card("Chimera", 7, 2, 2, 7, p);
                //addToHand(p, card2);
                //Card card3 = new Card("Blue Dragon", 2, 7, 7, 2, p);
                //addToHand(p, card3);
                //Card card4 = new Card("Garuda", 7, 1, 7, 6, p);
                //addToHand(p, card4);
                //Card card5 = new Card("Shiva", 1, 8, 8, 8, p);
                //addToHand(p, card5);
                var random = new Random();
                allCards = CardCollection.AllCards(p);
                List<Card> hand = new List<Card>();

                for(int i = 1; i < 5; i++)
                {
                    var filteredItems = allCards.Where(x => x.Stars == i).ToList();
                    if (filteredItems.Any())
                    {
                        var result = filteredItems[random.Next(filteredItems.Count)];
                        hand.Add(result);
                    allCards.Remove(result);
                    }
                    if(i == 2)
                    {
                        filteredItems = allCards.Where(x => x.Stars == i).ToList();
                        if (filteredItems.Any())
                        {
                            var result = filteredItems[random.Next(filteredItems.Count)];
                            hand.Add(result);
                            allCards.Remove(result);
                        }
                    }
                }

            //var hand = allCards.Take(0..5).ToList();
            p.Hand.AddRange(hand);
            //}
            //else
            //{
            //    //Card card1 = new Card("Moogle", 2, 3, 7, 1, p);
            //    //addToHand(p, card1);
            //    //Card card2 = new Card("Demon Wall", 6, 2, 3, 7, p);
            //    //addToHand(p, card2);
            //    //Card card3 = new Card("Ifrit", 7, 6, 7, 1, p);
            //    //addToHand(p, card3);
            //    //Card card4 = new Card("Y'shtola", 7, 1, 4, 8, p);
            //    //addToHand(p, card4);
            //    //Card card5 = new Card("Odin", 8, 1, 8, 8, p);
            //    //addToHand(p, card5);

            //    var allCards = CardCollection.AllCards(p);
            //    var hand = allCards.Take(5..10).ToList();
            //    p.Hand.AddRange(hand);
            //}
        }

        //Add new card to player's hand
        public void addToHand(Player p, Card c)
        {
            p.Hand.Add(c);
        }

        //Handles border checking when a card is played 
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
                leftAdj.FlipOwner(card.Owner, leftAdj.Owner);
                cardFlipped = leftAdj;
            }

            if ((rightAdj is not null) && (rightAdj.Left < card.Right))
            {
                rightAdj.FlipOwner(card.Owner, rightAdj.Owner);
                cardFlipped = rightAdj;
            }

            if ((bottomAdj is not null) && (bottomAdj.Top < card.Bottom))
            {
                bottomAdj.FlipOwner(card.Owner, bottomAdj.Owner);
                cardFlipped = bottomAdj;
            }

            if ((topAdj is not null) && (topAdj.Bottom < card.Top))
            {
                topAdj.FlipOwner(card.Owner, topAdj.Owner);
                cardFlipped = topAdj;
            }

            CardFlipped?.Invoke(cardFlipped);
            lastTurn = card.Owner;
        }

        //Checks if player did not just takes their turn and returns true or false depending on if they did or not
        public bool checkTurn(Player p)
        {
            return p != lastTurn ? true : false;
        }

        //Checks winner if board is full
        public void CheckWinner()
        {
            if (!board.CheckFull()) return;

            int p1Count = 0;
            int p2Count = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Card cell = board.GetCellState(i, j);
                    if (cell == null) continue;
                    if (cell.Owner == player1) p1Count++;
                    else p2Count++;
                }
            }

            Player winner = p1Count > p2Count ? player1 : player2;
            GameOver?.Invoke(winner);
        }
    }
}
