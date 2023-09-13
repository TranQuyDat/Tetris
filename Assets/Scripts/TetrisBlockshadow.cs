using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockshadow : MonoBehaviour
{
    public TetrisBlockType blockType;
    public TetrisBoard tetrisBoard;
    public GameObject block;
    float rotationAmount = 0;
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
        block.GetComponent<TetrisBlock>().Move(this.transform.position);
        foreach (Transform transform in transform)
        {
            Debug.Log(transform.name + ":" + transform.position);
        }
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
}
