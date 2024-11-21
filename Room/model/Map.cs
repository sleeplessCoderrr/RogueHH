using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private GameObject _room;
    private bool _isRoom;

    public Map(GameObject room, bool isRoom)
    {
        _room = room;
        _isRoom = isRoom;
    }
    
}
