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
    /// Interaction logic for GameController.xaml. Houses logic for game as a whole: Loads cards for players, plays cards, and checks for winners. 
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
            p.Hand.AddRange(hand);
        }

        //**Handles border checking when a card is played 
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

            else if ((bottomAdj is not null) && (bottomAdj.Top < card.Bottom))
            {
                bottomAdj.FlipOwner(card.Owner, bottomAdj.Owner);
                cardFlipped = bottomAdj;
            }

            else if ((rightAdj is not null) && (rightAdj.Left < card.Right))
            {
                rightAdj.FlipOwner(card.Owner, rightAdj.Owner);
                cardFlipped = rightAdj;
            }

            else if ((topAdj is not null) && (topAdj.Bottom < card.Top))
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
            Player winner = new Player(Player.Color.Purple);
            if(p1Count > p2Count)
            {
                winner = player1;
            }
            else if(p2Count > p1Count)
            {
                winner = player2;
            }

            GameOver?.Invoke(winner);
        }
    }
}
