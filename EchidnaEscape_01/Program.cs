using System;
using System.Collections.Generic;

namespace EchidnaEscape_01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate game 
            Game game = new Game();

            string input = "intro";
            // while game not over, play on, playa
            while (game.gameState == GameState.PlayOn &&
                    input != "quit")
            {
                game.player.NumberOfMovesTaken++;
                // Parse goes here and loop through returns here each time
                Parse parse = new Parse(input, game.view, game, game.currentRoom);
                input = parse.ExecuteCommand();

                // If game won, run the Win screen, play again?, if no, gameOver = true
                // If game lost, run the Lose screen, play again?, if no, gameOver = true
                // If neither, continue loop through game
                if (game.gameState == GameState.Won)
                {
                    string playagain = Console.ReadLine().ToLower();
                    if (playagain == "yes")
                    {
                        game = new Game();
                        continue;
                    }
                    else
                        break;

                }
                else if (game.gameState == GameState.Lost)
                {
                    game.view = new View(game.LoseScreen, game.currentRoom.RoomContentsAsStringList, Display.Wrap("You lost! Play again? Yes?", 60));
                    game.view.UpdateScreenAndGetInput();

                    string playagain = Console.ReadLine().ToLower();
                    if (playagain == "yes")
                    {
                        game = new Game();
                        continue;
                    }
                    else
                        break;
                }

            }

        }
    }
}
