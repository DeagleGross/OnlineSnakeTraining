using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SafeboardSnake.Core.Models;
using SafeboardSnake.WPF.Models;
using SafeboardSnake.WPF.Services;

namespace SafeboardSnake.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameWorker _gameWorker = new GameWorker();

        public GameViewModel GameView { get; set; }
            = new GameViewModel();

        public MainWindow()
        {
            DataContext = GameView;
            InitializeComponent();
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _gameWorker.GameViewModel = GameView;
            await _gameWorker.LaunchGame();
        }

        private async void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    await Task.Run(() => App.RestService.ChangeDirection("Up"));
                    break;
                case Key.Right:
                    await Task.Run(() => App.RestService.ChangeDirection("Right"));
                    break;
                case Key.Down:
                    await Task.Run(() => App.RestService.ChangeDirection("Down"));
                    break;
                case Key.Left:
                    await Task.Run(() => App.RestService.ChangeDirection("Left"));
                    break;
                default:
                    return;
            }   
        }

        private async void ReloadGame(object sender, RoutedEventArgs e)
        {
            await _gameWorker.LaunchGame();
        }
    }
}
