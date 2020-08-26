using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class Point : MonoBehaviour
{
    public float step = 5;

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
        if (cube.gameObject.transform.position == gameObject.transform.position)
        {
            cubeScript.AddScore(1);
            RelocatePoint();
        }
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
        /*if (other.gameObject.tag == "Cube")
        {
            if (other.gameObject.transform.position == gameObject.transform.position)
            {
                cubeScript.AddScore(1);
                RelocatePoint();
            }
        }*/
    }

    private void RelocatePoint()
    {
        Vector3 newPos;

        newPos = GenerateNewPoints();


        transform.position = newPos;

        // transform.position = transform.position + new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
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
