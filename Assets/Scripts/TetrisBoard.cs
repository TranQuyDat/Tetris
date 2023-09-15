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
    void Start()
    {
        
    }
    private void Update()
    {
       
    }
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
            if (grid[(int)Mathf.Round(v.x), (int)Mathf.Round(v.y)] == 3) return true;

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
        int yMin = 16;
        
        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v = round(transform.position);
            v = new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
            if (grid[(int)v.x, (int)v.y] == 1) return;
            if (v.y <= yMin && value == 0) yMin = (int)v.y;  // tìm y min
            grid[(int)v.x, (int)v.y] = value;

            //================!=3===============

            if ((int)v.y + 1 == 17 || value == 3) return;
            //nếu mà value =1 thì tim các vị trí phía trên nó để gán = 4 nếu ô đấy trống tức !=1
            if (grid[(int)v.x, (int)v.y+1] !=1 && value == 1)
            {
                grid[(int)v.x, (int)v.y+1] = 4;
            }
        }

        if (value == 3 || value == 1) return;
        List<Vector2> listpos = new List<Vector2>();
        
        // nếu mà value =0 thì tim vị trí thấp nhất để gán lại  =4
        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v = round(transform.position);
            v = new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
            if (v.y == yMin && value == 0) listpos.Add(v); 
        }

        for (int i = 0; i < listpos.Count; i++)
        {
            grid[(int)listpos[i].x, (int)listpos[i].y] = 4;
        }
    }



    //check xem cac khoi block co trong grid khong
    public bool checkIsinsideGrid(Vector2 pos)
    {
        return (Mathf.Round(pos.x) >= 0) && Mathf.Round(pos.x) < width 
            && Mathf.Round(pos.y) >= 0 && Mathf.Round(pos.y) < height;
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
        Debug.Log("posspawn : " + posspawn);
        block.transform.position = posspawn;
       
        foreach (Transform transform in block.transform)
        {
            Vector2 v = transform.position;
            Debug.Log("posspawn : " + posspawn+" ,"+transform.name+":"+v);
            if (!checkIsinsideGrid(v)) return false;
            if (grid[(int)Mathf.Round(v.x),(int) Mathf.Round(v.y)] == 1) return false;
            if (grid[(int)Mathf.Round(v.x), (int)Mathf.Round(v.y)] != 4) continue;
            else { dem++; }
        }
        return dem > 0;
    }

}
