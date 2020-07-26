using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject cube;

    private Vector3 offset;
    private Camera camera;
    private float cameraAngle;
    void Start()
    {
        camera = Camera.main;
        cameraAngle = camera.fieldOfView;
        offset = new Vector3(0f, 1.5f, -2.5f);
    }

    void Update()
    {
        if (cube != null)
        {
            transform.position = cube.transform.position + offset;
        } else
        {
            cube = GameObject.FindWithTag("Cube");
        }
    }
    void SwipeZoomPlus()
    {
        StartCoroutine("SwipeZoomPlusRoutine");
    }
    void SwipeZoomMinus()
    {
        StartCoroutine("SwipeZoomMinusRoutine");
    }
    IEnumerator SwipeZoomPlusRoutine()
    {
        while (camera.fieldOfView < 55)
        {
            camera.fieldOfView += 0.5f;
            yield return new WaitForSeconds(0.001f);
        }
    }
    IEnumerator SwipeZoomMinusRoutine()
    {
        while (camera.fieldOfView > cameraAngle)
        {
            camera.fieldOfView -= 0.5f;
            yield return new WaitForSeconds(0.001f);
        }
    }
}
