using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TetrisBoard tetrisBoard;
    public TetrisSpawner tetrisSpawner;
    private void Start()
    {
        tetrisSpawner.SpawnNextBlocks();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
}
