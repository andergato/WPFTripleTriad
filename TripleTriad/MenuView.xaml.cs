using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Channels;
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
    /// Interaction logic for MenuView.xaml. Houses the UI interaction logic for the menu. 
    /// </summary>
    public partial class MenuView
    {
        public MenuView()
        {
            InitializeComponent();
        }

        // Starts game
        public void StartGame_Click(object sender, RoutedEventArgs e)
        {
            // Access the MainWindow and change its content to the GameView
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainContent.Content = new GameView();
        }

        //Closes game
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            CancelEventArgs c = new CancelEventArgs();
            System.Windows.Application.Current.Shutdown();
        }

    }
}
