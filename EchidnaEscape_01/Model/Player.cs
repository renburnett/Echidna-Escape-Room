using System;
using System.Collections.Generic;

namespace EchidnaEscape_01
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
