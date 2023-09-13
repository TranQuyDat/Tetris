using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpawner : MonoBehaviour
{
    public GameObject[] blocks;
    public Transform posSpawn;
    public Transform tranformSpawn;
    public int blockstoSpawn = 3;
    public AutofindSelectPos autofind;
    public List<GameObject> blockschild = new List<GameObject>();
    private List<GameObject> blockSpawed = new List<GameObject>();
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ////demo spawn
        //if (!Input.GetKeyDown(KeyCode.Mouse0)) return;
        //blockSpawed.RemoveAt(0);
        //SpawnNextBlocks();
    }

    public void SpawnNextBlocks()
    {
        for (int i = 0; i < blockstoSpawn; i++)
        {
            int random = Random.Range(0, blocks.Length);
            blockSpawed.Add(blocks[random]);
        }
       
        GameObject newBlock = Instantiate(blockSpawed[0], posSpawn.position, Quaternion.identity,tranformSpawn);
        updateBlockChild(newBlock);
        autofind.checkblockType(blockSpawed[0].GetComponent<TetrisBlock>().blockType);
        autofind.block = newBlock;
        autofind.spawnBlockShadow();
    }

    public void updateBlockChild( GameObject newblock)
    {
        blockschild.Clear();
        blockschild.AddRange(newblock.GetComponent<TetrisBlock>().blockchild);
    }
}
