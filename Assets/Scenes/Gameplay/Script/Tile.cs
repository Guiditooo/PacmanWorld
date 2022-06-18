using UnityEngine;

using GeneralFunctions;

namespace CustomTiles
{
    
    public abstract class Tile : MonoBehaviour
    {
        public static int DefaultSize { get; private set; } = 60;
        public Int2 Position {get; set;}
        public Int2 GridPosition {get; set;}
        public static int Size { get; private set; } = 10;

        private TileType type;
        
        private SpriteRenderer spriteRenderer;

        public Tile(Int2 position, Int2 GridPos, TileType newType)
        {
            type = newType;
            Position = position; 
            GridPosition = GridPos;
        }
        public Tile(int xPos, int yPos, Int2 GridPos, TileType newType)
        {
            type = newType;
            Position = new Int2(xPos, yPos);
            GridPosition = GridPos;
        }
        public Tile(Int2 position, int xGridPos, int yGridPos, TileType newType)
        {
            type = newType;
            Position = position;
            GridPosition = new Int2(xGridPos, yGridPos);
        }
        public Tile(int xPos, int yPos, int xGridPos, int yGridPos, TileType newType)
        {
            type = newType;
            Position = new Int2(xPos, yPos);
            GridPosition = new Int2(xGridPos, yGridPos);
        }
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSize(int newSize)
        {
            Size = newSize;
            spriteRenderer.size = new Vector2(Size, Size);
        }

        public override string ToString() 
        {
            string aux;
            switch (type)
            {
                case TileType.Floor:
                    aux = "F";
                    break;
                case TileType.Wall:
                    aux = "W";
                    break;
                default:
                    aux = "U";
                    break;
            }
            return aux + "@" + Position.ToString();
        }

    }
}