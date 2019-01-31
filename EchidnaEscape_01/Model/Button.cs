
using System;
using System.Collections.Generic;

namespace EchidnaEscape_01
{
    public class Button : Item
    {
        private Room _room;

        public Button()
        {
            LongName = "Button";
            CompactName = "button";

            ItemActionMessages.Add("examine", "It's a big beautiful button");
            ItemActionMessages.Add("use", "You press the button and hear a click followed by a whir, a thunk, and what sounds like water flowing through pipes.");
        }

        public override string Use(Room room)
        {
            _room = room;

            if (BeenUsed)
                return $"Sorry, {LongName} has already been used.";
            else
            {
                BeenUsed = true;

                if(!_room.DoesItemExistInRoom("echidna"))
                {
                    _room.CurrentScene = _room.GarageScenes.ScenesDictionary["Water00_ButtonNoEchidna"];
                }
                else
                {
                    _room.CurrentScene = _room.GarageScenes.ScenesDictionary["Water00_EchidnaAndButton"];
                }
                return ItemActionMessages["use"];
            }
        }

    }

}
