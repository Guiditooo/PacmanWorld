using GeneralFunctions;
using UnityEngine;

namespace CustomTiles
{
    public class Floor : Tile
    {
        public Floor(Int2 pos, Int2 gridPos) : base(pos, gridPos, TileType.Floor)
        {

        }

    }
}
