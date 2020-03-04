using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    [HideInInspector]
    public int x, y = 0;

    [HideInInspector]
    public int visited = -1;    // -1 for unvisited, 0 for visited, 1 for closed

    [HideInInspector]
    public int fScore = 1;

    [HideInInspector]
    public int costSoFar = 0;

    [HideInInspector]
    public GameObject connection;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
