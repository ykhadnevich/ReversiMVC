using ReversiMVC.View.Console;

namespace ReversiMVC.Controller.Console;
using System;
using ReversiMVC.Model.Console;

    public class ReversiController
    {
        private readonly ConsoleOutput output;

        public ReversiController()
        {
            output = new ConsoleOutput();
        }

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
                        
                        game.StartGame();
                        output.OnFieldUpdated(game.GetField());
                        break;
                    case "exit":
                        return;
                    case "move":
                        /*if (splitCommand.Length >= 3 && int.TryParse(splitCommand[1], out int x) && int.TryParse(splitCommand[2], out int y))
                        {
                            game.MakeMove(x, y);
                        }
                        else
                        {
                            Console.WriteLine("Invalid move format. Use 'MOVE X Y'.");
                        }*/
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
