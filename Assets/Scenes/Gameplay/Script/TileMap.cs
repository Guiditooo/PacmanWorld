using System.Collections.Generic;
using UnityEngine;

//TODO Hacer que mientras mas grande sea la grilla, mas atras vaya la camara

namespace CustomTiles
{
    public class TileMap
    {
        List<Tile> tileList = new List<Tile>();
        static public int Rows { get; private set; }
        static public int Columns { get; private set; }
        public bool Created { get; private set; }

        public TileMap()
        {
            
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
