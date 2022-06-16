using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using GeneralFunctions;
using CustomTiles;

[CustomEditor(typeof(MapManager))]
public class MapManager : MonoBehaviour //Se supone que funca una vez que se selecciono el nivel.
{
    [SerializeField] [Range(65, 70)] private int gridSizePercentX = 65;
    [Space(15)]
    [SerializeField] [Range(5, 25)] private int gridPaddingPercentX = 5;
    [SerializeField] [Range(5, 25)] private int gridPaddingPercentY = 5;
    [Space(15)]
    [SerializeField] private CustomTiles.TileMap tileMap;
    [Space(15)]


    private Int2 gridSizePercent;
    private Int2 gridPaddingPercent;

    private Transform tileMapPos;
    private void Awake()
    {
        tileMapPos = tileMap.transform;
        gridSizePercent = new Int2(gridSizePercentX, gridSizePercentX);
        gridPaddingPercent = new Int2(gridPaddingPercentX, gridPaddingPercentY);        
    }

    private void Start()
    {
        tileMap.Create(new Int2(gridPaddingPercentX, gridPaddingPercentY), 600, 15, 15);
    }



}
