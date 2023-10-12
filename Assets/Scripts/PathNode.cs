using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode 
{

    public bool walkable;
    public int gridx;
    public int gridy;
    public int gcost = 0;
    public int hcost = 0;
    public Vector2Int gridpos;
    public PathNode parentnode;

    public PathNode(Vector2Int gridpos, bool walkable)
    {
        this.gridpos = gridpos;
        this.walkable = walkable; 
    }

    public int fCost
    {
        get 
        {
            return gcost + hcost;
        }
    }
    public Vector2Int getgridPos()
    {
        return gridpos;
    }

    
}
