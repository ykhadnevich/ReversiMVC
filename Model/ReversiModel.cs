namespace ReversiMVC.Model.Console
{
    using System;
    using System.Collections.Generic;

    public class Reversi
    {
        private const int FieldSize = 8; // Corrected to 8 for Reversi

        private readonly Player firstPlayer;
        private readonly Player secondPlayer;
        private Cell[,] field;
        
        public Player CurrentPlayer { get; private set; }
        public Player Winner { get; private set; }
        public bool IsEnded { get; private set; }

        public Cell[,] GetField() => field.Clone() as Cell[,];
        public Player GetCellValue(int x, int y) => field[x, y].MarkedByPlayer;

        public Reversi(Player firstPlayer, Player secondPlayer)
        {
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
        }

        public void StartGame()
        {
            CurrentPlayer = firstPlayer;
            PrepareField();
            
        }

        public void MakeMove(int x, int y)
        {
            
            if (IsEnded)
            {
                Console.WriteLine("Game is already ended.");
            }

            if (x < 0 || x >= FieldSize || y < 0 || y >= FieldSize)
            {
                Console.WriteLine("Invalid cell coordinates.");
            }

            if (!IsValidMove(x, y))
            {
                Console.WriteLine("Invalid move. Try again.");
            }
            
            MarkCell(x, y);
            
            CheckGameEnd();
        }
        
        public void MakeRandomMove()
        {
            Random random = new Random();
            int x, y;
            
            do
            {
                x = random.Next(FieldSize);
                y = random.Next(FieldSize);
            } while (!IsValidMove(x, y));

            
            MakeMove(x, y);
        }

        private void CalculateAndSetPossibleMoves()
        {
            for (int x = 0; x < FieldSize; x++)
            {
                for (int y = 0; y < FieldSize; y++)
                {
                    
                    field[x, y].IsPossibleMove = IsValidMove(x, y);
                    
                }
            }
        }
        
        protected virtual bool IsValidMove(int x, int y)
        {
            if (x < 0 || x >= FieldSize || y < 0 || y >= FieldSize || !field[x, y].IsEmpty)
            {
                return false;
            }

            Player currentPlayerMarker = (CurrentPlayer == firstPlayer) ? firstPlayer : secondPlayer;
            Player opponentMarker = (CurrentPlayer == firstPlayer) ? secondPlayer : firstPlayer;
            
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }

                    int r = x + i;
                    int c = y + j;
                    bool foundOpponentPiece = false;
                    
                    while (r >= 0 && r < FieldSize && c >= 0 && c < FieldSize && field[r, c].MarkedByPlayer == opponentMarker)
                    {
                        r += i;
                        c += j;
                        foundOpponentPiece = true;
                    }
                    
                    if (foundOpponentPiece && r >= 0 && r < FieldSize && c >= 0 && c < FieldSize && field[r, c].MarkedByPlayer == currentPlayerMarker && (r != x + i || c != y + j))
                    {
                        return true;
                    }
                }
            }

            return false;
        
        }
        
        
        protected virtual void MarkCell(int x, int y)
        {
            field[x, y].MarkBy(CurrentPlayer);
            Player opponent = (CurrentPlayer == firstPlayer) ? secondPlayer : firstPlayer;
            List<(int, int)> directions = new List<(int, int)>
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1),           (0, 1),
                (1, -1),  (1, 0),  (1, 1)
            };

            foreach (var (dx, dy) in directions)
            {
                int row = x + dx;
                int col = y + dy;
                bool foundOpponentPiece = false;

                while (row >= 0 && row < FieldSize && col >= 0 && col < FieldSize && field[row, col].MarkedByPlayer == opponent)
                {
                    foundOpponentPiece = true;
                    row += dx;
                    col += dy;
                }

                if (foundOpponentPiece && row >= 0 && row < FieldSize && col >= 0 && col < FieldSize && field[row, col].MarkedByPlayer == CurrentPlayer)
                {
                    // Flip the opponent's pieces between (x, y) and (row, col)
                    int r = x + dx;
                    int c = y + dy;
                    while (r != row || c != col)
                    {
                        field[r, c].MarkBy(CurrentPlayer);
                        r += dx;
                        c += dy;
    
                        // Terminate the loop when reaching the destination cell
                        if (r == row && c == col)
                        {
                            break;
                        }
                    }

                }
            }
            SwitchPlayer();
            CalculateAndSetPossibleMoves();
        }

        protected virtual void PrepareField()
        {
            field = new Cell[FieldSize, FieldSize];
            for (int x = 0; x < FieldSize; x++)
            {
                for (int y = 0; y < FieldSize; y++)
                {
                    /*if (IsValidMove(x, y))
                        Console.Write("* ");
                    else */
                        field[x, y] = new Cell();
                }
            }
            
            int mid = FieldSize / 2;
            field[mid - 1, mid - 1].MarkBy(firstPlayer);
            field[mid - 1, mid].MarkBy(secondPlayer);
            field[mid, mid - 1].MarkBy(secondPlayer);
            field[mid, mid].MarkBy(firstPlayer);
            
            CalculateAndSetPossibleMoves();
        }

        
        private void CheckGameEnd()
        {
            var hasEmptyCells = false;
            foreach (var row in GetAllRows())
            {
                var player = row[0].MarkedByPlayer;
                if (player != null && row.All(cell => cell.MarkedByPlayer == player))
                {
                    EndGame(player);
                    return;
                }

                if (row.Any(cell => cell.IsEmpty))
                {
                    hasEmptyCells = true;
                }
            }

            if (!hasEmptyCells)
            {
                EndGame();
            }
        }

        private IEnumerable<List<Cell>> GetAllRows()
        {
            for (int i = 0; i < FieldSize; i++)
            {
                yield return GetRow(i);
                yield return GetColumn(i);
            }

            yield return GetDiagonal(true);
            yield return GetDiagonal(false);
        }

        private List<Cell> GetRow(int rowIndex)
        {
            var row = new List<Cell>();
            for (int i = 0; i < FieldSize; i++)
            {
                row.Add(field[rowIndex, i]);
            }
            return row;
        }

        private List<Cell> GetColumn(int colIndex)
        {
            var column = new List<Cell>();
            for (int i = 0; i < FieldSize; i++)
            {
                column.Add(field[i, colIndex]);
            }
            return column;
        }

        private List<Cell> GetDiagonal(bool isMain)
        {
            var diagonal = new List<Cell>();
            for (int i = 0; i < FieldSize; i++)
            {
                diagonal.Add(field[isMain ? i : FieldSize - 1 - i, i]);
            }
            return diagonal;
        }

        
        protected virtual void EndGame(Player winner = null)
        {
            IsEnded = true;
            Winner = winner;
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = (CurrentPlayer == firstPlayer) ? secondPlayer : firstPlayer;
        }
    }
}
