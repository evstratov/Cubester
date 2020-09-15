using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float step = 5;

    private Cube cubeScript;
    private GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        cube = GameObject.FindWithTag("Cube");
        if (cube != null)
            cubeScript = cube.GetComponent<Cube>();
        if (!Utils.FirstPlay)    
            RelocatePoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (cube.gameObject.transform.position == gameObject.transform.position)
        {
            cubeScript.AddScore(1);
            RelocatePoint();
        }
    }

    private void RelocatePoint()
    {
        Vector3 newPos;
        newPos = GenerateNewPoints();
        transform.position = newPos;
    }

    private Vector3 GenerateNewPoints()
    {
        Vector3 newPos;
        //  получаем новые координаты следующей точки
        float x = Random.Range(0, 50);
        x = Mathf.Round(x / 10);
        float z = step - x;

        // получаем знаки случайным образом
        // 0 - отрицательный
        // 1 - положительный
        int signX;
        int signZ;

        while (true)
        {
            signX = Random.Range(0, 100) > 50 ? signX = -1 : signX = 1;
            signZ = Random.Range(0, 100) > 50 ? signZ = -1 : signZ = 1;

            // проверяем точки на существование в пределах карты
            newPos = cube.transform.position + new Vector3(signX * x, 0, signZ * z);

            RaycastHit hit;
            Ray ray = new Ray(newPos, -Vector3.up);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null && hit.collider.gameObject.tag == "Level")
            {
                return newPos;
            }
        }
    }
}
