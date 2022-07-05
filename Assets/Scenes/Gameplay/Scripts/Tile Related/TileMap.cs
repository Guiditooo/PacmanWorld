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

        [SerializeField] private Transform tilesFolder;

        private static float tileSize = 1;
        public static float GetTileSize() => tileSize;
        public static int Rows { get; private set; } = 15;
        public static int Columns { get; private set; } = 15;

        private static Tile[,] tileArray;
        public static TileType GetTileAtGridPos(Int2 tile)
        {
            return tileArray[tile.X, tile.Y].GetTileType();
        }
        public static TileType GetTileAtGridPos(int x, int y)
        {
            return tileArray[x, y].GetTileType();
        }
        public static Int2 GetMapBounds()
        {
            return new Int2(Rows, Columns);
        }
        private void Awake()
        {
            SetTileSize();
        }

        public void CreateTileMap()
        {
            tileArray = new Tile[TileMap.Rows,TileMap.Columns];

            for (int y = 0; y < Columns; y++)
            {
                for (int x = 0; x < Rows; x++)
                {
                    GameObject newGO;
                    if (x == 0 || x == Rows - 1 || y == 0 || y == Columns-1)
                    {
                        newGO = Instantiate(wallPrefab, TileConversor.GridToWorld(x, y), Quaternion.identity, tilesFolder);
                        newGO.name = "[ " + x + " - " + y + " ] - " + wallPrefab.name;
                        newGO.GetComponent<Wall>().Position = new Int2(x, y);
                        tileArray[x,y] = newGO.GetComponent<Wall>();
                    }
                    else
                    {
                        newGO = Instantiate(floorPrefab, TileConversor.GridToWorld(x, y), Quaternion.identity, tilesFolder);
                        newGO.name = "[ " + x + " - " + y + " ] - " + floorPrefab.name;
                        newGO.GetComponent<Floor>().Position = new Int2(x, y);
                        tileArray[x,y] = newGO.GetComponent<Floor>();
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
            return tileArray[posToCheck.X,posToCheck.Y].GetTileType() == TileType.Floor;

        }

        

    }
}