
using System;
using System.Collections.Generic;

namespace ReferenceGame
{
    public class Echidna : Item
    {
        private Room _room;

        public Echidna()
        {
            LongName = "Echidna";
            CompactName = "echidna";

            ItemActionMessages.Add("examine", "O.M.G. This thing is cute. It looks like a porcupine with a long snoot. And maybe even cuddly?");
            ItemActionMessages.Add("use", "Harry refuses your advances.");
        }

        public override string Use(Room room)
        {
            _room = room;

            if (BeenUsed)
                return $"Sorry, {LongName} has already been used. And he's not happy about your continued advances.";
            else
            {
                BeenUsed = true;

                if (_room.WaterLevel > 6 &&
                    _room.DoesItemExistInRoom("echidna"))
                {
                    _room.RoomContents.Add(_room.GetHiddenRoomContentsByName("garagedooropener"));
                    _room.RoomContentsAsStringList.Add("Garage Door Opener");
                    return "The echidna gives you a li'l kiss on the cheek then dives underwater and attacks the shark! The echidna is eating the shark! He eats a large hole in the shark's belly revealing a... garage door opener? ";
                }
                else
                {
                    BeenUsed = false;
                    return ItemActionMessages["use"];
                }
                
            }
        }

    }

}
