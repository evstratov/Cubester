﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Cube cubeScript;
    private GameObject cube;
    private Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.FindWithTag("Cube");
        if (cube != null)
            cubeScript = cube.GetComponent<Cube>();
    }

    // Update is called once per frame
    void Update()
    {
        /*colliders = Physics.OverlapSphere(transform.position, 0.0f);
        if (colliders.Length > 0)
        {
            if (colliders[0].GetComponentInParent<GameObject>().tag == "Cube")
            {
                cubeScript.AddScore(1);
                RelocatePoint();
            }
        }*/
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            cubeScript.AddScore(1);
            RelocatePoint();
        }
    }

    private void RelocatePoint()
    {
        transform.position = transform.position + new Vector3(1,0,1); 
    }
}