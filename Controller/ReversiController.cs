using ReversiMVC.View.Console;

namespace ReversiMVC.Controller.Console;
using System;
using ReversiMVC.Model.Console;

public class ReversiController
{
    private readonly ConsoleOutput output;

    

    public void StartProcessing(ReversiEvents game)
    {
        Console.WriteLine("Welcome to the Reversi game! Type START to begin.");

        while (true)
        {
            string command = Console.ReadLine();
            var splitCommand = command.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            switch (splitCommand[0].ToLower())
            {
                case "start":
                    Console.WriteLine("Choose mode: PvP or PvE");
                    string modeInput = Console.ReadLine().ToLower();
                    switch (modeInput)
                    {
                        case "1":
                            game.StartGame();
                            output.OnFieldUpdated(game.GetField());
                            break;
                        case "2":
                            game.StartGame();
                            output.OnFieldUpdated(game.GetField());
                            game.PlayGame();
                            break;

                    }
                    break;
                case "exit":
                    return;
                case "move":
                    
                    var x = int.Parse(splitCommand[1]);
                    var y = int.Parse(splitCommand[2]);
                    game.MakeMove(x, y);

                    break;
                default:
                    Console.WriteLine("Invalid command.");
                    break;
            }
            
        }
    }
}