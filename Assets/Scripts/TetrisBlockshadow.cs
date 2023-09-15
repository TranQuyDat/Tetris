using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockshadow : MonoBehaviour
{
    public TetrisBlockType blockType;
    public GameObject block;
    public int countoftype;
    
    float rotationAmount = 0;
    GameObject tetrisBoard_obj;
    GameObject tetrisSpawner_obj;
    TetrisBoard tetrisBoard;
    TetrisSpawner tetrisSpawner;
    private void Start()
    {
        tetrisBoard_obj = GameObject.FindGameObjectWithTag("board");
        tetrisSpawner_obj = GameObject.FindGameObjectWithTag("spawn");
        tetrisBoard = tetrisBoard_obj.GetComponent<TetrisBoard>();
        tetrisSpawner = tetrisSpawner_obj.GetComponent<TetrisSpawner>();

    }
    public bool isvalid()
    {
        foreach (Transform child in transform) 
        {
            Vector2 v = child.position;
            if (tetrisBoard.checkIsinsideGrid(transform.position) == false) return false;
            if (tetrisBoard.grid[(int)v.x,(int)v.y] == 1) return false;
        }
        return true;
    }

    private void OnMouseDown()  
    {
        TetrisBlock tetrisBlock = block.GetComponent<TetrisBlock>();
        tetrisBlock.Move(this.transform.position);
        block.transform.rotation = this.transform.rotation;
        foreach(Transform tran in block.transform)
        { 
            tran.rotation = this.transform.GetChild(0).rotation;
        }
        tetrisBoard.setGridofblock(this.gameObject,1);
        Destroy(this.gameObject);
        tetrisSpawner.SpawnNextBlocks();
    }

    public void Rotate()
    {
        // Xử lý xoay khối block.
        if (blockType == TetrisBlockType.O) return;//hình vuông không cần xoay
        //LI:90,
        rotationAmount = 90f;
        transform.Rotate(Vector3.forward * rotationAmount);
        foreach (Transform tran in transform)
        {
            tran.Rotate(Vector3.forward * -90f);
            
        }
    }

    public void reload()
    {
        transform.Rotate( new Vector3(0,0,0));
        foreach (Transform tran in transform)
        {
            tran.Rotate(new Vector3(0, 0, 0));
        }
    }
}
