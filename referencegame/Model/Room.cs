using System;
using System.Collections.Generic;
using System.Linq;

namespace ReferenceGame
{
    public class Room
    {
        public string Name { get; set; }
        public int WaterLevel { get; set; } = 0;
        public bool ExitClosed { get; set; } = true;

        public string IntroMessage;
        public Scenes GarageScenes = new Scenes();

        public List<Item> HiddenRoomContents = new List<Item>();
        public List<Item> RoomContents = new List<Item>();
        public List<string> RoomContentsAsStringList = new List<string>();
        public List<string> CurrentScene { get; set; } = new List<string>();

        public Room(string name)
        {
            Console.Title = "Glistening Echidna - Escape Room";
            Name = name;

            //GarageScenes.Add()

            CurrentScene = GarageScenes.ScenesDictionary["start"];

            // Room Items
            RoomContents.Add(new Honeybear());
            RoomContents.Add(new Button());
            RoomContents.Add(new Blanket());

            foreach (Item item in RoomContents)
            {
                RoomContentsAsStringList.Add(item.LongName);
            }

            HiddenRoomContents.Add(new Echidna());
            HiddenRoomContents.Add(new Shark());
            HiddenRoomContents.Add(new GarageDoorOpener());
        }

        public bool DoesItemExistInRoom(string cn)
        {
            if (RoomContents.FindIndex(x => x.CompactName == cn) >= 0)
                return true;
            else
                return false;
        }

        public Item GetRoomContentsByName(string cn)
        {
            return RoomContents.Find(x => x.CompactName == cn);
        }

        public Item GetHiddenRoomContentsByName(string cn)
        {
            return HiddenRoomContents.Find(x => x.CompactName == cn);
        }
    }
}
