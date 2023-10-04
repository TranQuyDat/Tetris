using System.Collections.Generic;
using UnityEngine;

public class PosValid
{
    public Vector2 pos;
}


public class TetrisBoard : MonoBehaviour
{
    public int width = 7;
    public int height = 17;

    public int[,] grid  ;

    private void Awake()
    {
        grid = new int[width, height];
        initGridboard();
    }
  
    private void Update()
    {     
    }
    
    public bool checkEmptyValid(int x , int y)
    {
        if (grid[x, y] == 1) return true;
        int d = 0;
        int curY = -2;
        int maxj = 1;
        for (int i = -1; i <= 1; i++)
        {
            for(int j = -1;j<= maxj; j++)
            {
                if (d > 1) return false;
                if (i == 1) maxj = 0;
                if (i == 0 && j == 0) continue;
                if (grid[x+i,y+j] == 1 && 
                   ( (curY < y+j && j!=0) || (curY <= y+j && j == 0) ))
                {
                    curY = y + j;
                    d++;
                    i += 1;
                    j = -2;
                }
            }
        }
        return true;
    }
    //
   /* public void setvalueEmt_notvalid(int x, int y)
    {
        for (int i = y-1; i >= 0; i--)
        {
            for(int j  = 0; j < height; j++)
            {
                if (grid[x + j, i] == 1 && grid[x - j, i] == 1)
                {

                }
                if ()
                {

                }
            }
        }
    }*/

    // khoi tao grid
    public void initGridboard()
    {
        for (int i = 0; i < width; i++) grid[i, 0] = 4;
        for(int i =0; i < width; i++)
        {
            for(int j = 1;j< height; j++)
            {
                grid[i,j] = 0;
            }
        }
    }
    //check va cham giua 2 shadowBlock
   public bool iscollidingShadow(GameObject shadowblock,Vector2 pos)
    {
        
        shadowblock.transform.position = pos;
        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v =( transform.position);
            int x = Mathf.RoundToInt(v.x);
            int y = Mathf.RoundToInt(v.y);
            if (grid[x,y] == 3 || grid[x,y] == 1) return true;

        }
        return false;
    } 

   //set grid value
    public void setGrid(Vector2 pos,int value)
    {
        grid[(int)pos.x, (int)pos.y] = value;
    }

    //set gia tri cua cac cell trong grid theo block
    public void setGridofblock(GameObject shadowblock, int value)
    {
        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v = round(transform.position);
            
            int x = Mathf.RoundToInt(v.x);
            int y = Mathf.RoundToInt(v.y);

            if (grid[x,y] == 1) return;
            grid[x,y] = value;
            
           
        }
    }

    public void setGridwheninsert(GameObject shadowblock)
    {
        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v = round(transform.position);
            int x = Mathf.RoundToInt(v.x);
            int y = Mathf.RoundToInt(v.y);
            grid[x, y] = 1;
            if (y + 1 != 17 && grid[x,y + 1] != 1 ) grid[x,y + 1] = 4;
            Debug.Log("after insert:grif[" + x + "," + y + "] = " + grid[x, y]);
            
        }
    }

    public void setGridwhendelete(GameObject shadowblock)
    {

        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v = round(transform.position);
            int x = Mathf.RoundToInt(v.x);
            int y = Mathf.RoundToInt(v.y);
            
            if (grid[x,y] == 1) return;
            grid[x, y] = 0;
            if (y==0 ||y > 0 && grid[x, y - 1] == 1) grid[x, y] = 4;
        }

    }

   

    //check xem cac khoi block co trong grid khong
    public bool checkIsinsideGrid(Vector2 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        return (x >= 0) && x < width 
            && y >= 0 && y < height;
    }

    // doi don vi 
    public Vector2 round(Vector2 pos)
    {
        return new Vector2(pos.x/0.4f, pos.y/0.4f);
    }

    //check cac kieu block xem position co hop le khong

    public List<PosValid> posvalid(GameObject block)
    {
        block.GetComponent<TetrisBlockshadow>().Rotate();
        return posvalidof(block);
    }

    //check block xem position co hop le khong
    public List<PosValid> posvalidof(GameObject block)
    {

        List<PosValid> newpos = new List<PosValid>();
        for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (grid[i, j] == 1) { continue; }
                    if (!rule(new Vector2(i, j), block)) { continue; }
                PosValid posvalid = new PosValid();
                posvalid.pos = new Vector2(i, j);
                newpos.Add(posvalid);
                }
            }
        return newpos;
    }


    //check cac position cua child trong block co hop le khong
    public bool rule(Vector2 posspawn, GameObject block)
    {
        int dem = 0;
        block.transform.position = posspawn;
       
        foreach (Transform transform in block.transform)
        {
            Vector2 v = transform.position;
            int x = Mathf.RoundToInt(v.x);
            int y = Mathf.RoundToInt(v.y);

            if (!checkIsinsideGrid(v)) return false;
            if (grid[x,y] == 1) return false;
            if (grid[x,y] != 4) continue;
            else { dem++; }
        }
        return dem > 0;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for(int i = 0; i <=width; i++)
        {
            Gizmos.DrawLine(new Vector3(i*0.4f, -1 * 0.4f, 0), new Vector3(i*0.4f, 16 * 0.4f, 0));
        }
        
        for(int i = -1; i < height; i++)
        {
            Gizmos.DrawLine(new Vector3(0, i * 0.4f, 0), new Vector3(7*0.4f, i * 0.4f, 0));
        }
        
    }

}
