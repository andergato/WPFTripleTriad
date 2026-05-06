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
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public Player Player { get; set; }
        public PlayerControl(Player player, int playerNum, Dictionary<Card, CardControl> cardDict)
        {
            InitializeComponent();

            Player = player;
            int leftOffset = 15;
            int topOffset = 0;

            //Sets up UI for each players hand and sets up card control
            foreach (Card card in player.Hand)
            {
                //if (leftOffset >= 300)
                //{
                //    leftOffset = 50;
                //    topOffset += 150;
                //}

                CardControl cardControl = cardDict[card];
                Canvas.SetLeft(cardControl, leftOffset);
                Canvas.SetTop(cardControl, topOffset);
                HandCanvas.Children.Add(cardControl);
                //leftOffset += 140;
                topOffset += 170;

                if (playerNum == 0)
                {
                    player.PlayerColor = Player.Color.Blue;
                }
                else
                {
                    player.PlayerColor = Player.Color.Red;
                }
            }

        }

        //Changes owner of a card
        public void FlipOwner(Board board, CardControl c, Player player2)
        {
            Card card = c.Card;

            Player.Hand.Remove(card);
            player2.Hand.Add(card);

            c.FlipCard();
        }
}
}
