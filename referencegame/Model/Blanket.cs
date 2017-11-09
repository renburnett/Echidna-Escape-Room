
using System;
using System.Collections.Generic;

namespace ReferenceGame
{
    public class Blanket : Item
    {
        private Room _room;

        public Blanket()
        {
            LongName = "Blanket";
            CompactName = "blanket";

            ItemActionMessages.Add("examine", "Looks like one of those heavy furniture blankets movers use.");
            ItemActionMessages.Add("use", "You wrap the heavy blanket around your shoulders. Hark! An adorable echidna was hiding under the blanket!");
        }

        public override string Use(Room room)
        {
            _room = room;

            if (BeenUsed)
                return $"Sorry, {LongName} has already been used.";
            else
            {
                BeenUsed = true;

                Item echidna = _room.GetHiddenRoomContentsByName("echidna");
                _room.RoomContents.Add(echidna);
                _room.RoomContentsAsStringList.Add("Echidna");

                if (_room.WaterLevel == 0)
                {
                    _room.CurrentScene = _room.GarageScenes.ScenesDictionary["Water00_EchidnaNotButton"];
                }
                else if (_room.WaterLevel > 0)
                {
                    string scn = "Water0" + room.WaterLevel + "_EchidnaAndButton";
                    _room.CurrentScene = _room.GarageScenes.ScenesDictionary[scn];
                }

                if (_room.GetRoomContentsByName("honeybear").BeenUsed)
                {
                    return ItemActionMessages["use"] + "\nThe echidna scampers to slurp up the ants. The happy echidna is now your buddy.";
                }
                else
                    return ItemActionMessages["use"];
            }

        }

    }

}
