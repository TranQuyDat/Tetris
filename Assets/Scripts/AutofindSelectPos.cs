using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AutofindSelectPos : MonoBehaviour
{
    public List<GameObject> blockshadow;
    public Transform posSpawn;
    public Transform parent;
    public GameObject block;
    public TetrisBoard tetrisBoard;

    TetrisBlockType blockType;
    GameObject cur_blockshadow;
    int MaxspawnShpadow ;
    List<GameObject> lisSpawned;
    List<PosValid> validPositions;
    private void Awake()
    {
        validPositions = new List<PosValid>();
        lisSpawned = new List<GameObject>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        

    }
    public void checkblockType(TetrisBlockType type)
    {
        this.blockType = type;
    }


    GameObject selectBlockBytype()
    {
        foreach(GameObject block in blockshadow)
        {
            if(block.GetComponent<TetrisBlockshadow>().blockType == blockType)
            {
                
                return block;
            }
        }
        return null;
    }

    //spawn shadown block
    public void SpawnRandomBlockShadow(int index)
    {
        posSpawn.position = validPositions[index].pos;
        GameObject block = selectBlockBytype();
        block.transform.position = posSpawn.position;
        if (!tetrisBoard.iscollidingShadow(block, posSpawn.position))
        {
            cur_blockshadow = Instantiate(block, parent);

           
            tetrisBoard.setGridofblock(cur_blockshadow, 3); // set value cell cua Tetrisboard
            lisSpawned.Add(cur_blockshadow);
            cur_blockshadow.GetComponent<TetrisBlockshadow>().block = this.block;
        }
    }

  
 
    public void Reload()
    {
        if (lisSpawned == null) return;
        foreach(GameObject game in lisSpawned)
        {
            tetrisBoard.setGridofblock(game, 0);
            Destroy(game);
        }
        lisSpawned.Clear();
    }
    public void spawnBlockShadow()
    {
        Reload();
        validPositions = tetrisBoard.posvalid(selectBlockBytype());
        Debug.Log(validPositions.Count);
        MaxspawnShpadow = 3 ;
        for (int i = 0; i < MaxspawnShpadow; i++)
        {
            int maxindex = validPositions.Count;
            int random = Random.Range(0, maxindex);
            if (random != null)
                SpawnRandomBlockShadow(random);
            
        }
    }

}
