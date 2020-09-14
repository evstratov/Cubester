using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallElement : MonoBehaviour
{
    public bool isForwardWall;
    public float coord;

    private float delay = 1f;

    private Vector3 moveVector = Vector3.up;

    void Start()
    {
        float deltaCoord = Random.Range(-50, 50) / 100f;
        delay += Random.Range(-30, 30) / 100f;

        //if (isForwardWall)
        //{
        //    moveVector = Vector3.forward;
        //    transform.position = transform.position + new Vector3(0, 0, deltaCoord);
        //}
        //else
        //{
        //    moveVector = Vector3.right;
        //    transform.position = transform.position + new Vector3(deltaCoord, 0, 0);
        //}
    }
    private void FixedUpdate()
    {
        //if (isForwardWall)
        //{
        //    if (transform.position.z > coord + 0.5f)
        //    {
        //        moveVector = Vector3.back;
        //    }
        //    if (transform.position.z < coord - 0.5f)
        //    {
        //        moveVector = Vector3.forward;
        //    }
        //}
        //else
        //{
        //    if (transform.position.x > coord + 0.5f)
        //    {
        //        moveVector = Vector3.left;
        //    }
        //    if (transform.position.x < coord - 0.5f)
        //    {
        //        moveVector = Vector3.right;
        //    }
        //}

        if (transform.position.y > coord + 0.5f)
        {
            moveVector = Vector3.down;
        }
        if (transform.position.y < coord - 0.5f)
        {
            moveVector = Vector3.up;
        }

        transform.Translate(moveVector * Time.deltaTime * delay);
    }
}
