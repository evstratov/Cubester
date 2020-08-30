﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject cube;

    private Vector3 offset;

    private Vector3 velocity;
    void Start()
    {
        offset = new Vector3(0f, 1.5f, -2.5f);
    }

    void Update()
    {
        if (cube != null)
        {
            //transform.position = cube.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, cube.transform.position + offset, ref velocity, 0.15f, 30);
        } else
        {
            cube = GameObject.FindWithTag("Cube");
        }
    }
}
