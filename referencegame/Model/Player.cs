using System;
using System.Collections.Generic;

namespace ReferenceGame
{
    public class Player
    {
        public string Name { get; set; }
        public List<Item> InventoryItems { get; set; }
        public int NumberOfMovesTaken { get; set; }

        public Player()
        {
            NumberOfMovesTaken = 0;
        }

    }
}
