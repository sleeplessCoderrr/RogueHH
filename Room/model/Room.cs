using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public GameObject _tiles;
    public GameObject[] _floorDecorations { get; set; }
    public GameObject[] _roomDecorations { get; set; }

    public Room(GameObject tiles, GameObject[] floorDecorations, GameObject[] roomDecorations)
    {
        this._tiles = tiles;
        this._floorDecorations = floorDecorations;
        this._roomDecorations = roomDecorations;
    }
}
