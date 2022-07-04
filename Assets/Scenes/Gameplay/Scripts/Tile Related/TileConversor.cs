using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralFunctions;


namespace CustomTiles
{
    public class TileConversor : MonoBehaviour
    {
        public static Vector3 GridToWorld(Int2 gridPos)
        {
            return new Vector3(TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * gridPos.X, TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * gridPos.Y);
        }
        public static Vector3 GridToWorld(int x, int y)
        {
            return new Vector3(TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * x, TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * y);
        }
    }
}