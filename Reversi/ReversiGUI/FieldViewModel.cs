using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reversi.Domain;
using Reversi.Cells;
using System.Windows.Input;
using Reversi.DataStructures;
using System.Windows.Data;
using System.Globalization;
namespace ReversiGUI
{
    class FieldViewModel{
        private class MoveCommand : ICommand
        {
            FieldViewModel gameViewModel;
            public MoveCommand(FieldViewModel gameViewModel)
            {
                this.gameViewModel = gameViewModel;
                gameViewModel.Square.IsValidMove.PropertyChanged += (sender, args) =>
                {
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, new EventArgs());
                    }
                };
            }

            public bool CanExecute(object parameter)
            {
                return gameViewModel.Square.IsValidMove.Value;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                gameViewModel.Square.PlaceStone();
            }
        }

        public Vector2D Position { get { return square.Position; } }

        public int X { get { return Position.X; } }
        public int Y { get { return Position.Y; } }

        public ICell<Player> Owner { get { return square.Owner; } }
        //public String OwnerName { get { return (square.Owner.Value == null)? "" : Owner.Value.ToString(); } }

        private ISquare square;
        public ISquare Square { get { return square; } }

        private ICommand move;
        public ICommand Move { get { return move; } }

        private ICell<Player> currentPlayer;
        public ICell<Player> CurrentPlayer { get { return currentPlayer; } }

        public FieldViewModel(ISquare square,ICell<Player> currentPlayer)
        {
            this.square = square;
            move = new MoveCommand(this);
            this.currentPlayer = currentPlayer;
        }
    }
}
