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
       
    }

    public void SpawnNextBlocks()
    {
       int random = Random.Range(0, blocks.Length);
       
        GameObject newBlock = Instantiate(blocks[random], posSpawn.position, Quaternion.identity,tranformSpawn);
        updateBlockChild(newBlock);
        autofind.checkblockType(newBlock.GetComponent<TetrisBlock>().blockType);
        autofind.block = newBlock;
        autofind.spawnBlockShadow();
    }

    public void updateBlockChild( GameObject newblock)
    {
        blockschild.Clear();
        blockschild.AddRange(newblock.GetComponent<TetrisBlock>().blockchild);
    }
}
