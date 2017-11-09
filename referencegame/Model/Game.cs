// Game.cs
//      includes basic elements to play game and instantiate items in room

using System;
using System.Collections.Generic;

namespace ReferenceGame
{
    public enum GameState
    {
        Won,
        Lost,
        PlayOn
    }

    public class Game
    {
        public GameState gameState = GameState.PlayOn;

        public Room currentRoom;
        public View view;
        public Player player = new Player();

        public List<string> WinScreen = new List<string>() { @"*\o/*" };
        public List<string> LoseScreen = new List<string>() { @"(,~_~)" };
 
        public Game()
        {
            Room garage = new Room("Garage")
            {
                IntroMessage = "You're in a room with only a few items in it and no apparent way in or out. You see a button on the wall, one of those honey containers shaped like a bear, and a blanket piled up in the corner. ",
            };

            currentRoom = garage;
        }

    }
}
