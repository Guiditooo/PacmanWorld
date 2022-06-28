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

        private static List<Tile> tileList = new List<Tile>();
        public static Int2 InitialCharacterPos { get; set; } = Int2.one;
        public static Int2 InitialStalkerPos { get; set; } = GetMapBounds()-2;
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
                    GameObject newGO;
                    if (x == 0 || x == rowCount - 1 || y == 0 || y == columnCount-1)
                    {
                        newGO = Instantiate(wallPrefab, pos, Quaternion.identity, transform);
                        newGO.name = "[ " + x + " - " + y + " ] - " + wallPrefab.name;
                        newGO.GetComponent<Wall>().Position = new Int2(x, y);
                        tileList.Add(newGO?.GetComponent<Wall>());
                    }
                    else
                    {
                        newGO = Instantiate(floorPrefab, pos, Quaternion.identity, transform);
                        newGO.name = "[ " + x + " - " + y + " ] - " + floorPrefab.name;
                        newGO.GetComponent<Floor>().Position = new Int2(x, y);
                        tileList.Add(newGO?.GetComponent<Floor>());
                    }
                    if (x == 1 && y == 1)
                    {
                        InitialCharacterPos = new Int2(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
                    }
                }
            }

        }

        private void SetTileSize()
        {
            tileSize =  floorPrefab.GetComponent<SpriteRenderer>().size.x;
        }

        public static bool CheckForValidTile(Int2 posToCheck)
        {
            foreach (Tile tile in tileList)
            {
                if(posToCheck == tile.Position)
                {
                    return tile.GetTileType() == TileType.Floor;
                }

            }
            return false;
        }

    }
}