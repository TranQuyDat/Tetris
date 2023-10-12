using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPathway 
{
    List<PathNode> openlist;
    List<PathNode> closelist;
    int[,] grid;
    PathNode startNode;
    PathNode endNode;

    public FindPathway(Vector2Int startPos, Vector2Int endPos, int[,] grid)
    {
        this.startNode = new PathNode(startPos,true);
        this.endNode = new PathNode(endPos,true);
        this.grid = grid;
    }
    public List<PathNode> getlistNeighborofnode(Vector2Int pos)
    {
        List<PathNode> lisneighbor = new List<PathNode>();
        for(int i = -1;i <= 1; i++)
        {
            for(int j = -1; j <= 1; j++)
            {
                if ((i == 0 && j == 0) || (i != 0 && j != 0)) continue;

                if (pos.x > 0 && pos.y > 0 && grid[pos.x+i, pos.y+j] != 1  )
                {
                    PathNode node = new PathNode(pos, false);
                    lisneighbor.Add(node);
                    continue;
                }

                if(grid[pos.x + Mathf.Abs(i), pos.y + Mathf.Abs(j)] != 1)
                {
                    PathNode node = new PathNode(pos, false);
                    lisneighbor.Add(node);
                }
            }
        }

        return lisneighbor;
    }

    public int getdistance(Vector2Int nodeA, Vector2Int nodeB)
    {
        return Mathf.RoundToInt(Vector2Int.Distance(nodeA, nodeA)) ; 
    }

    public List<Vector2Int> findpath()
    {
        openlist = new List<PathNode>();
        closelist = new List<PathNode>();
        List<Vector2Int> path = new List<Vector2Int>();
        List<PathNode> listneigbor = new List<PathNode>();
        openlist.Add(startNode);

        /*while (openlist.Count > 0)
        {
            PathNode cur_node = openlist[0];
            if (cur_node.gridpos == endNode.gridpos)
            {
                return repath();
            }
            listneigbor = getlistNeighborofnode(cur_node.gridpos);

            for (int i = 0; i < openlist.Count; i++)
            {
                if (openlist[i].fCost < cur_node.fCost ||
                    (openlist[i].fCost < cur_node.fCost && openlist[i].hcost < cur_node.hcost))
                {
                    cur_node = openlist[i];
                }
            }
            openlist.Remove(cur_node);
            closelist.Add(cur_node);

            foreach (PathNode n in listneigbor)
            {
                if (closelist.Contains(n)) continue;

                int newpathcosttoneighbor = cur_node.gcost + getdistance(cur_node.gridpos, n.gridpos);
                if (newpathcosttoneighbor < cur_node.gcost || !openlist.Contains(n))
                {
                    n.gcost = newpathcosttoneighbor;
                    n.hcost = getdistance(n.gridpos, endNode.gridpos);
                    n.parentnode = cur_node;
                    if (!openlist.Contains(n)) openlist.Add(n);
                }
            }

        }*/
        return null;

        }
   
    

    public List<Vector2Int> repath()
    {
        PathNode node = endNode;
        List<Vector2Int> path = new List<Vector2Int>();
        while (node != startNode)
        {
            path.Add(node.gridpos);
        }

        return path;
    }

}
