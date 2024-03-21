namespace ReversiMVC.View.Console;
using System;
using ReversiMVC.Model.Console;    

public class ConsoleOutput
{
    private ReversiEvents game;

    public void ListenTo(ReversiEvents game)
    {
        this.game = game;
        game.GameStarted += OnGameStarted;
        game.FieldUpdated += RenderGameBoardWithPossibleMoves;
        game.PlayerWon += OnPlayerWon;
        game.MatchDrawn += OnMatchDrawn;
    }

    private void OnMatchDrawn()
    {
        Console.WriteLine("Game is over! It's a draw.");
    }

    private void OnPlayerWon(Player player)
    {
        Console.WriteLine($"Game is over! Player {player.Name} won!");
    }



    public void OnFieldUpdated(Cell[,] field)
    {
        /*Console.Write($" {v(field[0,0])} ║ {v(field[0,1])} ║ {v(field[0,2])} ║ {v(field[0,3])} ║ {v(field[0,4])} ║ {v(field[0,5])} ║ {v(field[0,6])} ║ {v(field[0,7])} \n");
        Console.Write("═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══\n");
        Console.Write($" {v(field[1,0])} ║ {v(field[1,1])} ║ {v(field[1,2])} ║ {v(field[1,3])} ║ {v(field[1,4])} ║ {v(field[1,5])} ║ {v(field[1,6])} ║ {v(field[1,7])} \n");
        Console.Write("═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══\n");
        Console.Write($" {v(field[2,0])} ║ {v(field[2,1])} ║ {v(field[2,2])} ║ {v(field[2,3])} ║ {v(field[2,4])} ║ {v(field[2,5])} ║ {v(field[2,6])} ║ {v(field[2,7])} \n");
        Console.Write("═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══\n");
        Console.Write($" {v(field[3,0])} ║ {v(field[3,1])} ║ {v(field[3,2])} ║ {v(field[3,3])} ║ {v(field[3,4])} ║ {v(field[3,5])} ║ {v(field[3,6])} ║ {v(field[3,7])} \n");
        Console.Write("═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══\n");
        Console.Write($" {v(field[4,0])} ║ {v(field[4,1])} ║ {v(field[4,2])} ║ {v(field[4,3])} ║ {v(field[4,4])} ║ {v(field[4,5])} ║ {v(field[4,6])} ║ {v(field[4,7])} \n");
        Console.Write("═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══\n");
        Console.Write($" {v(field[5,0])} ║ {v(field[5,1])} ║ {v(field[5,2])} ║ {v(field[5,3])} ║ {v(field[5,4])} ║ {v(field[5,5])} ║ {v(field[5,6])} ║ {v(field[5,7])} \n");
        Console.Write("═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══\n");
        Console.Write($" {v(field[6,0])} ║ {v(field[6,1])} ║ {v(field[6,2])} ║ {v(field[6,3])} ║ {v(field[6,4])} ║ {v(field[6,5])} ║ {v(field[6,6])} ║ {v(field[6,7])} \n");
        Console.Write("═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══\n");
        Console.Write($" {v(field[7,0])} ║ {v(field[7,1])} ║ {v(field[7,2])} ║ {v(field[7,3])} ║ {v(field[7,4])} ║ {v(field[7,5])} ║ {v(field[7,6])} ║ {v(field[7,7])} \n");
        string v(Cell cell)
        {
            return cell.IsEmpty ? " " : cell.MarkedByPlayer.Name;
        }*/
        RenderGameBoardWithPossibleMoves(field);
    }
    
    public void RenderGameBoardWithPossibleMoves(Cell[,] field)
    {
        
        for (int x = 0; x < field.GetLength(0); x++)
        {
            for (int y = 0; y < field.GetLength(1); y++)
            {
                if (field[x, y].IsPossibleMove)
                {
                    Console.Write("* ");
                }
                else
                {
                    Console.Write(GetCellValue(field[x, y]) + " ");
                }
            }
            Console.WriteLine();
        }
        
    }

    private string GetCellValue(Cell cell)
    {
        return cell.IsEmpty ? " " : cell.MarkedByPlayer.Name;
    }

    private void OnGameStarted(Cell[,] field)
    {
        Console.WriteLine("Game is started! Make your first move with 'MOVE X Y'");
    }
}