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
    /// Interaction logic for CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        public Card Card { get; private set; }

        public CardControl(Card card)
        {
            InitializeComponent();
            // Set up visual card components
            Card = card;
            //if (card.Owner.PlayerColor == Player.Color.Red)
            //{
            string colorName = Card.Owner.PlayerColor.ToString();
            card.ImagePath = @$"Images/Cards/{colorName}Cards/{Card.Name}{colorName}.png";

            CardImage.Source = new BitmapImage(
                new Uri($"pack://application:,,,/{card.ImagePath}", UriKind.Absolute)
            );
        }
       
        // Allow card to be moved and dropped
        private void CardControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
            }
        }

        //Change card's UI color based on backend 
        public void FlipCard()
        {
            //if (Card.Owner.PlayerColor == Player.Color.Red)
            //{
            //    Card.ImagePath = $"{Card.Name}{Card.Owner.PlayerColor}.jpg";
            //}
            //else
            //{
            //    DragCard.Fill = Brushes.Blue;
            //}
            string colorName = Card.Owner.PlayerColor.ToString();
            Card.ImagePath = @$"Images/Cards/{colorName}Cards/{Card.Name}{colorName}.png";

            CardImage.Source = new BitmapImage(
                new Uri($"pack://application:,,,/{Card.ImagePath}", UriKind.Absolute)
            );
        }
    }
}
