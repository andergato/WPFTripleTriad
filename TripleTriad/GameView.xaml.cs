using System;
using System.Collections.Generic;
using System.Numerics;
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
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView
    {
        private GameController _gameController;
        private CardControl _cardControl;
        public Dictionary<Card, CardControl> cardDict = new Dictionary<Card, CardControl>();

        public GameView()
        {
            InitializeComponent();
            _gameController = new GameController();
            LoadGame();
        }

        //Loads board and adds it to game view
        private void LoadGame()
        {
            loadCards(_gameController.player1);
            loadCards(_gameController.player2);

            PlayerControl p1c = new PlayerControl(_gameController.player1, 0, cardDict);
            PlayerControl p2c = new PlayerControl(_gameController.player2, 1, cardDict);
            BoardControl bc = new BoardControl(_gameController.board, _gameController);

            _gameController.CardFlipped += (card) =>
            {
                if (card != null && cardDict.ContainsKey(card))
                    cardDict[card].FlipCard();
            };

            _gameController.GameOver += (winner) =>
            {
                Grid.SetColumn(WinOverlay, 1);
                WinnerText.Text = $"{winner.PlayerColor} wins!";
                WinOverlay.Visibility = Visibility.Visible;
            };

            Grid.SetColumn(p1c, 0);
            Grid.SetColumn(bc, 1);
            Grid.SetColumn(p2c, 2);

            RootGrid.Children.Add(bc);
            RootGrid.Children.Add(p1c);
            RootGrid.Children.Add(p2c);
        }

        //Load up card dictionary
        private void loadCards(Player player)
        {
            foreach (Card card in player.Hand)
            {
                cardDict.Add(card, new CardControl(card));
            }
        }

        //Reset game
        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            var menu = new MenuView();
            menu.StartGame_Click(sender, e);
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            RulesPage.Visibility = Visibility.Collapsed;
        }
    }
}
