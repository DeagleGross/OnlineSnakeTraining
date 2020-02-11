using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Media;
using SafeboardSnake.Core.Models.DataTransferContracts;
using SafeboardSnake.WPF.Annotations;

namespace SafeboardSnake.WPF.Models
{
    public class GameViewModel : INotifyPropertyChanged
    {
        private string _additionalInfo;
        private string _userName;
        private string _playerScoreboard;

        private int _rows;
        private int _columns;
        private List<Brush> _cells;

        private int _roundNumber;
        private int _turnNumber;
        private int _timeTillNextTurn;

        public string AdditionalInfo
        {
            get => _additionalInfo;
            set
            {
                _additionalInfo = value;
                OnPropertyChanged(nameof(AdditionalInfo));
            }
        }

        public int TurnNumber
        {
            get => _turnNumber;
            set
            {
                _turnNumber = value;
                OnPropertyChanged(nameof(TurnNumber));
            }
        }

        public int RoundNumber
        {
            get => _roundNumber;
            set
            {
                _roundNumber = value;
                OnPropertyChanged(nameof(RoundNumber));
            }
        }

        public int TimeTillNextTurn
        {
            get => _timeTillNextTurn;
            set
            {
                _timeTillNextTurn = value;
                OnPropertyChanged(nameof(TimeTillNextTurn));
            }
        }

        public List<Brush> Cells
        {
            get => _cells;
            set
            {
                _cells = value;
                OnPropertyChanged(nameof(Cells));
            }
        }

        public string PlayerScoreboard
        {
            get => _playerScoreboard;
            set
            {
                _playerScoreboard = value;
                OnPropertyChanged(nameof(PlayerScoreboard));
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        public int Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        public int Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                OnPropertyChanged(nameof(Rows));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CallCellsChanged()
        {
            OnPropertyChanged(nameof(Cells));
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
