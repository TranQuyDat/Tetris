using System.Collections;
using UnityEngine;

public enum TetrisBlockType { I, J, L, O, S, T, Z }

public class TetrisBlock : MonoBehaviour
{
    public TetrisBlockType blockType;
    public GameObject[] blockchild;
    bool ismoving =false;
    Vector3 newPosition;

    float rotationAmount=0;
    private void Start()
    {
    }
    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        
        if (!ismoving) return;
        moveto();
    }
    public void Move(Vector3 newPosition)
    {
        ismoving = true;
        this.newPosition = newPosition;
         
    }
    void moveto()
    {
        
        transform.position = newPosition;
        
        ismoving = false;
      
    }
    
    public void Rotate()
    {
        // Xử lý xoay khối block.
        if (blockType == TetrisBlockType.O) return;//hình vuông không cần xoay
        //LI:90,
        rotationAmount =  90f ;
        transform.Rotate(Vector3.forward * rotationAmount);
        foreach(Transform tran in transform)
        {
            tran.Rotate(Vector3.forward * -90f);
            Debug.Log(tran.name+":" +tran.position);
        }
    }
}
