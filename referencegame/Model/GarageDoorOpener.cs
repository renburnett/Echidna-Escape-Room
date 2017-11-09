
using System;
using System.Collections.Generic;

namespace ReferenceGame
{
    public class GarageDoorOpener : Item
    {
        public GarageDoorOpener()
        {
            LongName = "Garage Door Opener";
            CompactName = "garagedooropener";

            ItemActionMessages.Add("examine", "Sweet! Looks like a Genie GT-90, or perhaps a Stanley XT-60");
            ItemActionMessages.Add("use", "You click the button on the door opener, rusty gears creak as your exit is revealed! You and the glistening echidna parade out of the room.\nYou won! Play again? Yes?");
        }

        public override string Use(Room room)
        {
            room.ExitClosed = false;
            return ItemActionMessages["use"];
        }
    }
}
