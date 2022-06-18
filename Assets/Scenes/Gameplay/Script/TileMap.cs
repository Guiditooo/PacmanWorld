using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralFunctions;

namespace CustomTiles
{
    public class TileMap : MonoBehaviour
    {
        [SerializeField] private GameObject floorPrefab;
        [SerializeField] private GameObject wallPrefab;

        private static float tileSize = 1;
        public static float GetTileSize() => tileSize;
        public static int Rows { get; private set; } = 15;
        public static int Columns { get; private set; } = 15;

        private List<Tile> tileList = new List<Tile>();

        public static Vector2 InitialTilePos { get; private set; }

        public static Int2 GetMapBounds()
        {
            return new Int2(Rows, Columns);
        }

        private void Awake()
        {
            SetTileSize();
        }

        public void Create(int rowCount, int columnCount)
        {

            Rows = rowCount;
            Columns = columnCount;

            for (int y = 0; y < columnCount; y++)
            {
                for (int x = 0; x < rowCount; x++)
                {
                    Vector3 pos = new Vector3(tileSize / 2 + tileSize * x, tileSize / 2 + tileSize * y);
                    GameObject newGO = Instantiate(floorPrefab, pos, Quaternion.identity, transform);
                    newGO.name = "[ " + x + " - " + y + " ]";
                    newGO.GetComponent<Floor>().Position = new Int2(x, y);
                    tileList.Add(newGO?.GetComponent<Floor>());
                    if (x == 0 && y == 0)
                    {
                        InitialTilePos = new Vector2(pos.x, pos.y);
                    }
                }
            }
        }

        private void SetTileSize()
        {
            tileSize =  floorPrefab.GetComponent<SpriteRenderer>().size.x;
        }

    }
}