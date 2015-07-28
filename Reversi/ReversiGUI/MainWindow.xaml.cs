using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReversiGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        GameViewModel gameViewModel;
        public Window1()
        {
            InitializeComponent();
            gameViewModel = new GameViewModel();
            gameManager.DataContext = gameViewModel;
            gameViewModel.IsGameOver.PropertyChanged += GameOverEvent;
        }

        public void UserMessage(String message)
        {
            MessageBox.Show(message);
        }

        public bool UserQuestion(String title, String message)
        {
            if (MessageBox.Show(message, title, MessageBoxButton.YesNo) == MessageBoxResult.No)
                return false;
            return true;
        }

        private void GameOverEvent(object sender, PropertyChangedEventArgs e)
        {
            int offset = gameViewModel.getnStones(gameViewModel.PlayerWithMostStones.Value) - gameViewModel.getnStones(gameViewModel.PlayerWithMostStones.Value.Other);
            String message = "Congratulations on your victory " + gameViewModel.PlayerWithMostStones.Value.ToString() + "\nYou won with " + offset;
            if (offset == 0)
                message = "Oh my... It's a draw!";

            UserMessage(message);
            if (UserQuestion("Play again?", "Do You Want A Rematch?"))
                Rematch();
        }
        
        private void Rematch()
        {
            gameViewModel = new GameViewModel();
            gameManager.DataContext = gameViewModel;
            gameViewModel.IsGameOver.PropertyChanged += GameOverEvent;
            CloseMenu(null, null);
        }

        private void Quit() {
            if (UserQuestion("Leave?", "are you sure you want to leave?")) { 
                Close();
            }
        }


        private void keyHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Quit();
        }

        private void OpenMenu(object sender, RoutedEventArgs e)
        {
            MenuOverlay.Visibility = System.Windows.Visibility.Visible;
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            if (UserQuestion("Restart?", "are you sure you want to restart? All data will be lost."))
            {
                Rematch();                
            }
        }

        private void CloseMenu(object sender, RoutedEventArgs e)
        {
            MenuOverlay.Visibility = System.Windows.Visibility.Collapsed;
        }
        
        private void QuitGame(object sender, RoutedEventArgs e)
        {
             Quit();
        }
    }
}
