using System.Collections.Generic;
using UnityEngine;

public class PosValid
{
    GameObject block;
    public void setBlockvalue(Vector3 positionCopy, Quaternion rotationCopy )
    {
        block = new GameObject();
        block.transform.position = positionCopy;
        block.transform.rotation = rotationCopy;
    }

    public GameObject getBlock(GameObject block)
    {
        block.transform.position = this.block.transform.position;
        block.transform.rotation = this.block.transform.rotation;
        return block;
    }
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
            Debug.Log(v);
            if (grid[(int)v.x, (int)v.y] == 3) return true;

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
        int yMax = 0;
        int yMin = 16;
        List<Vector2> listpos = new List<Vector2>();
        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v = (transform.position);
            if (v.y <= yMin && value == 0) yMin = (int)v.y;  // tìm y max
            if (v.y >= yMax && value == 1) yMax = (int)v.y;  // tìm x max
            grid[(int)v.x, (int)v.y] = value;
        }
        foreach (Transform transform in shadowblock.transform)
        {
            Vector2 v = round(transform.position);
            if (v.y == yMin && value == 0) listpos.Add(v); 
            if (v.y == yMax && value == 1) listpos.Add(new Vector2(v.x,v.y+1)); 
        }
        for (int i = 0; i < listpos.Count; i++) grid[(int)listpos[i].x, (int)listpos[i].y] = 4; 
    }



    //check xem cac khoi block co trong grid khong
    public bool checkIsinsideGrid(Vector2 pos)
    {
        return (pos.x >= 0) && pos.x < width && pos.y >= 0 && pos.y<height;
    }

    // doi don vi 
    public Vector2 round(Vector2 pos)
    {
        return new Vector2(pos.x/0.4f, pos.y/0.4f);
    }

    //check xem position co hop le khong
    public List<PosValid> posvalid(GameObject block)
    {
        List<PosValid> listPosValid = new List<PosValid>();
        
        for (int k = 0; k < 4; k++)
        {
            
            block.GetComponent<TetrisBlockshadow>().Rotate();
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (grid[i, j] == 1) { continue; }
                    if (!rule(new Vector2(i, j), block)) { continue; }
                    PosValid newpos = new PosValid();
                    newpos.setBlockvalue(new Vector2(i, j), block.transform.rotation);
                    listPosValid.Add(newpos);
                }
            }
        }
      
        return listPosValid;
    }


    //check cac position cua child trong block co hop le khong
    public bool rule(Vector2 posspawn, GameObject block)
    {
        int dem = 0;
        block.transform.position = posspawn;
        foreach (Transform transform in block.transform)
        {
            Vector2 v = transform.position;
            if (!checkIsinsideGrid(v)) return false;
            if (grid[(int)v.x, (int)v.y] == 1) return false;
            if (grid[(int)v.x, (int)v.y] != 4) continue;
            else { dem++; }
        }
        return dem > 0;
    }

}
