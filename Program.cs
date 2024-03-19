using System;
using ReversiMVC.Controller.Console;
using ReversiMVC.Model.Console;
using ReversiMVC.View.Console;

namespace ReversiMVC
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new ReversiEvents(new Player("X"), new Player("O") );
            var output = new ConsoleOutput();
            output.ListenTo(game);

            var controller = new ReversiController();
            controller.StartProcessing(game);
        }
    }
}