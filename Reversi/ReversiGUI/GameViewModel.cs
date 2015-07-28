using System;
using System.Collections.Generic;
using Reversi.Domain;
using Reversi.Cells;
using System.Windows.Input;
using Reversi.DataStructures;
using System.Windows;
using System.ComponentModel;
namespace ReversiGUI
{
    class GameViewModel
    {
        private Game game;
        public Game Game { get { return game;}}

        private IList<FieldViewModel> fieldList;
        public IList<FieldViewModel> FieldList { get { return fieldList; } }

        private ICell<bool> playerOneAI; 
        public ICell<bool> PlayerOneAI { get { return playerOneAI; } }

        private ICell<bool> playerTwoAI;
        public ICell<bool> PlayerTwoAI { get { return playerTwoAI; } }

        private ICell<int> playerTwoAILevel;
        public ICell<int> PlayerTwoAILevel { get { return playerTwoAILevel; } }

        private ICell<int> playerOneAILevel;
        public ICell<int> PlayerOneAILevel { get { return playerOneAILevel; } }

        public ICell<int> PlayerOneStones { get { return Game.StoneCount(Player.ONE); } }
        public ICell<int> PlayerTwoStones { get { return Game.StoneCount(Player.TWO); } }

        public Player PlayerOne { get { return Player.ONE; } }
        public Player PlayerTwo { get { return Player.TWO; } }

        private ICell<Player> currentPlayer;
        public ICell<Player> CurrentPlayer { get { return currentPlayer; } }

        private ICell<Player> playerWithMostStones;
        public ICell<Player> PlayerWithMostStones { get { return playerWithMostStones; } }

        private ICell<bool> isGameOver;
        public ICell<bool> IsGameOver { get { return isGameOver; } }

        public GameViewModel()
        {
            game = Game.CreateNew();
            playerOneAI = Cell.Create(false);
            playerTwoAI = Cell.Create(false);
            playerOneAILevel = Cell.Create(0);
            playerTwoAILevel = Cell.Create(0);
            playerWithMostStones = game.PlayerWithMostStones;
            isGameOver = game.IsGameOver;
            currentPlayer = game.CurrentPlayer;
            createBoard();
            StartAI();
        }

        private void createBoard()
        {
            fieldList = new List<FieldViewModel>();
            for (int i = 0; i < game.Board.Height; i++)
                for (int j = 0; j < game.Board.Width; j++)
                    fieldList.Add(new FieldViewModel(Game.Board[new Vector2D(i, j)], currentPlayer));
        }

        public void StartAI() { 
            var heuristic = new WeightedStoneCountHeuristic();
            game.CurrentPlayer.PropertyChanged += (sender,args) => {
                if (playerOneAI.Value && ((ICell<Player>)sender).Value == Player.ONE)
                    game.Board[ReversiArtificialIntelligence.CreateDirect(heuristic,playerOneAILevel.Value).FindBestMove(game)].PlaceStone();
                if (playerTwoAI.Value && ((ICell<Player>)sender).Value == Player.TWO)
                    game.Board[ReversiArtificialIntelligence.CreateDirect(heuristic, playerTwoAILevel.Value).FindBestMove(game)].PlaceStone();
            };
        }

        public int getnStones(Player player) {
            return game.StoneCount(player).Value;
        }
    }
}
