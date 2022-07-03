using GeneralFunctions;
using UnityEngine;

namespace CustomTiles
{
    public class Floor : Tile
    {

        private bool hasPoint;
        public bool HasPoint
        {
            get 
            { 
                return hasPoint; 
            }
        }
        
        public Floor(Int2 pos, Int2 gridPos) : base(pos, gridPos, TileType.Floor)
        {

        }

        private void Start()
        {
            hasPoint = true;
        }

        public void GrabPoint()
        {
            hasPoint = false;
        }

    }
}
