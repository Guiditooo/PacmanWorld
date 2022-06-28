using System.Collections.Generic;
using UnityEngine;
using GeneralFunctions;
using System;

//TODO Hacer que mientras mas grande sea la grilla, mas atras vaya la camara

namespace CustomTiles
{
    public class TileMap2 : MonoBehaviour
    {
        [SerializeField] private GameObject floorPrefab;
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private Transform test;

        static public int DefaultRows { get; private set; } = 10;
        static public int DefaultColumns { get; private set; } = 10;

        List<Tile> tileList = new List<Tile>();
        static public int Rows { get; private set; }
        static public int Columns { get; private set; }

        private Int2 mapPadding; //To start counting. Percent
        private int mapSize;
        private int tileSize;
        public void Create(Int2 padding, int tileMapSize, int rowCount, int columnCount)
        {
            mapPadding = padding; //padding es un porcentaje. Hay que pasarlo al tamaño de la pantalla. No se como.
            mapSize = tileMapSize; 
            Rows = rowCount;
            Columns = columnCount;

            tileSize = GetTileNeededSize();

            int startingPosX = padding.X + tileSize / 2;
            int startingPosY = padding.Y + tileSize / 2;

            Int2 startingPos = new Int2(startingPosX, startingPosY);

            
            Vector3 aux = Camera.main.ViewportToScreenPoint(test.position);

            Debug.Log("Unity Pos> " + test.position); 
            Debug.Log("Screen Pos> " + aux);
            Debug.Log("Unity Global Pos> " + Camera.main.ViewportToWorldPoint(test.position));

            test.position = test.position + Camera.main.ScreenToViewportPoint(Vector3.zero);

            
            for (int y = 0; y < columnCount; y++)
            {
                for (int x = 0; x < rowCount; x++)
                {
                    GameObject newGO = Instantiate(floorPrefab, new Vector3(startingPosX + tileSize*x, startingPosY + tileSize * y), Quaternion.identity, transform);
                    newGO?.GetComponent<Floor>().SetSize(tileSize);
                    newGO.name = "[ " + x + " - " + y +" ]";
                    tileList.Add(newGO?.GetComponent<Floor>());
                }
            }
        }

        private int GetTileNeededSize()
        {
            int size = Tile.DefaultSize;

            if(Rows > DefaultRows || Columns > DefaultColumns)
            {
                if (Rows > Columns)
                {
                    size = mapSize / Rows;
                }
                else
                {
                    size = mapSize / Columns;
                }
            }

            return size;
        }
        

        public static void CreateMap()
        {

        }

        public void SetRows(int newRows)
        {

        }
        public void SetColumns(int newColumns)
        {

        }
        public void SetSize(int newSize)
        {
            foreach (Tile tile in tileList)
            {
                
            }
        }
        /*
        private TileMap defaultMap()
        {
            TileMap tm;

            return TileMap();
        }
        */
    }
}
