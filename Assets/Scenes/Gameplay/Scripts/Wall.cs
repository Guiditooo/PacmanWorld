using GeneralFunctions;

namespace CustomTiles
{
    public class Wall : Tile
    {
        public Wall(Int2 pos, Int2 gridPos) : base(pos, gridPos, TileType.Wall)
        {

        }
    }
}
