using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EchidnaEscape_01
{
    public class Parse
    {
        // Read in string, break it into array of strings, if no index then "help", else if only 1 index, then help or quit, else break first index into verb and remainder into nouns
        public Game game;
        public View view;
        public Room room;
        public string[] commands;
        public string InputVerb;
        public string InputNoun;
        public string helpMessage = "Try different actions with the items in the room. Examples: EXAMINE {ITEM}, or USE {ITEM}. Type INTRO to see the opening screen text.";

        // If user enters nothing (which is the default too), intro view is shown.
        //      Otherwise, convert user's input to lowercase, and split at space into an array of strings
        public Parse(string s, View view, Game game, Room room)
        {
            this.game = game;
            this.view = view;
            this.room = room;

            s = string.IsNullOrWhiteSpace(s) ? "help" : s;
            s = s.Replace("the ", "");
            commands = s.Trim().ToLower().Split(' ');

            InputVerb = commands[0];

            InputNoun = "";
            for (int i = 1; i < commands.Length; i++)
            {
                InputNoun += commands[i];
            }

            // Water level tracks the number of moves made
            if (room.GetRoomContentsByName("button").BeenUsed)
                room.WaterLevel++;

            // If waterlevel is at a certain height (5) then shark gets added to the room contents 
            if (room.WaterLevel == 6)
            {
                room.RoomContents.Add(room.GetRoomContentsByName("shark"));
                room.RoomContentsAsStringList.Add("Shark");
            }
            // we want this to:
            //      ideally if no second part, and verb is not 'intro' or 'help' or 'quit', add a follow-up command prompt saying "'verb' what?"
        }
// TODO: something ain't working, so we need to fix it here (e.g.: after 5 or 6 moves 'use water' broke stuff)
        public string ExecuteCommand()
        {
            if (!room.DoesItemExistInRoom("echidna") && room.WaterLevel > 0 )
            {
                string scn = "Water0" + room.WaterLevel + "_NoEchidna";
                room.CurrentScene = room.GarageScenes.ScenesDictionary[scn];
            }
            else if (room.DoesItemExistInRoom("echidna") && room.WaterLevel > 0)
            {
                string scn = "Water0" + room.WaterLevel + "_EchidnaAndButton";
                room.CurrentScene = room.GarageScenes.ScenesDictionary[scn];
            }
            // before updating each of the views check if water level > 0 if so, use scene with corresponding water level
            if (!room.DoesItemExistInRoom(InputNoun) && InputNoun != "")
            {
                view = new View(room.CurrentScene, room.RoomContentsAsStringList, Display.Wrap($"I'm sorry, {InputNoun.ToUpper()} is not available in the room!", 60));
                return view.UpdateScreenAndGetInput();
            }

            switch (InputVerb)
            {
                case "intro":
                    view = new View(room.CurrentScene, room.RoomContentsAsStringList, Display.Wrap(room.IntroMessage, 60));
                    return view.UpdateScreenAndGetInput();
                case "examine":
                    view = new View(room.CurrentScene, room.RoomContentsAsStringList, Display.Wrap(room.GetRoomContentsByName(InputNoun).Examine(), 60));

                    return view.UpdateScreenAndGetInput();
                case "take":
                case "use":
                    string UseItemSceneUpdate = room.GetRoomContentsByName(InputNoun).Use(room);
                    view = new View(room.CurrentScene, room.RoomContentsAsStringList, Display.Wrap(UseItemSceneUpdate, 60));
                    if (!room.ExitClosed)
                        game.gameState = GameState.Won;
                    return view.UpdateScreenAndGetInput();
                // TODO: Add 'swim' or 'tread water' 
                case "help":
                    view = new View(room.CurrentScene, room.RoomContentsAsStringList, Display.Wrap(helpMessage, 60));
                    return view.UpdateScreenAndGetInput();
                default:
                    view = new View(room.CurrentScene, room.RoomContentsAsStringList, Display.Wrap("Command not found. Type 'help' for a list of commands.", 60));
                    return view.UpdateScreenAndGetInput();
            }
        }

    }
}
