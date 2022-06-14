using UnityEngine;

using GeneralFunctions;

namespace CustomTiles
{
    public abstract class Tile : MonoBehaviour
    {
        public Int2 Position {get; set;}
        public static int Size { get; private set; }

        private TileType type = TileType.Floor;
        //Collectible

        private SpriteRenderer spriteRenderer;

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