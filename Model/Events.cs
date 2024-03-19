namespace ReversiMVC.Model.Console;

using System;

    public class ReversiEvents : Reversi
    {
        public event Action<Cell[,]> GameStarted;
        
        public event Action<Cell[,]> FieldUpdated;
        
        public event Action<Player> PlayerWon;
        
        public event Action MatchDrawn;
        
        public ReversiEvents(Player firstPlayer, Player secondPlayer) : base(firstPlayer, secondPlayer)
        {
        }

        protected override void PrepareField()
        {
            base.PrepareField();
            GameStarted?.Invoke(GetField());
        }

        protected override void MarkCell(int x, int y)
        {
            base.MarkCell(x, y);
            FieldUpdated?.Invoke(GetField());
        }

        protected override void EndGame(Player winner = null)
        {
            
            base.EndGame(winner);
            if (winner == null)
            {
                MatchDrawn?.Invoke();
            }
            else
            {
                PlayerWon?.Invoke(winner);
            }
        }
    }