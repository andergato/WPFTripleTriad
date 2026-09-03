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
using System.Xml.Linq;

namespace TripleTriad
{
    /// <summary>
    /// Interaction logic for BoardControl.xaml. Handles logic with board and each card's interaction with it. 
    /// </summary>
    public partial class BoardControl : UserControl
    {
        public event Action<Card, int, int> CardDropped;
        public Board Board { get; }
        public GameController _GameController;

        public BoardControl(Board board, GameController gc)
        {
            InitializeComponent();
            Board = board;
            _GameController = gc;
        }

        // **Drops a card from the canvas onto the board
        public void Border_Drop(object sender, DragEventArgs e)
        {
            // Check if the card is dropped on the board and if spot is not already taken
            var targetBorder = sender as Border;
            if (targetBorder == null || targetBorder.Child != null) return;

            //// Check the payload is actually a card, it is the player's turn, and it is not a card already on the board being moved
            if ((CardControl)e.Data.GetData(typeof(CardControl)) is not CardControl droppedCard || !_GameController.checkTurn(droppedCard.Card.Owner) || LogicalTreeHelper.GetParent(droppedCard).ToString() != "System.Windows.Controls.Canvas") return;

            // Remove circle from its current parent
            RemoveFromParent(droppedCard);

            clearValues(droppedCard);

            // Place it inside the dropped cell
            targetBorder.Child = droppedCard;

            //Let board object know where each item is
            Board.SetCellState(Grid.GetRow(targetBorder), Grid.GetColumn(targetBorder), droppedCard.Card);

            int row = Grid.GetRow(targetBorder);
            int col = Grid.GetColumn(targetBorder);
            _GameController.PlayCard(row, col, droppedCard.Card);

            CardDropped?.Invoke(droppedCard.Card, row, col);

            _GameController.CheckWinner();
        }

        //UI logic to remove card from hand
        private void RemoveFromParent(UIElement element)
        {
            var parent = LogicalTreeHelper.GetParent(element);

            switch (parent)
            {
                case Border b:
                    b.Child = null;
                    break;
                case Panel p:
                    p.Children.Remove(element);
                    break;
            }
        }

        //Clears previous formatting and centers card inside new borders
        private void clearValues(CardControl droppedCard)
        {
            droppedCard.ClearValue(Canvas.LeftProperty);
            droppedCard.ClearValue(Canvas.TopProperty);
            droppedCard.ClearValue(MarginProperty);
            droppedCard.HorizontalAlignment = HorizontalAlignment.Center;
            droppedCard.VerticalAlignment = VerticalAlignment.Center;
            droppedCard.Width = 110;
            droppedCard.Height = 110;
        }
    }
}
