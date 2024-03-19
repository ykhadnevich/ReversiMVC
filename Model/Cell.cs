using System;

namespace ReversiMVC.Model.Console
{
    public class Cell
    {
        public Player MarkedByPlayer { get; private set; }

        public bool IsEmpty => MarkedByPlayer == null;
        
        public bool IsPossibleMove { get; set; }
        internal void MarkBy(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player), "Player cannot be null.");

            /*if (!IsEmpty)
                throw new InvalidOperationException("Cell is already marked.");*/

            MarkedByPlayer = player;
        }

        
    }
}