using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room 
{
    public GameObject Floor { get; set; }
    public List<GameObject> Decorations { get; set; }
    public List<GameObject> Tunnels { get; set; }

    public Room()
    {
        Decorations = new List<GameObject>();
        Tunnels = new List<GameObject>();
    }
}
