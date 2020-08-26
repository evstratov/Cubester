using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlicking : MonoBehaviour
{
    Text timeText;
    bool isStart = false;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        timeText = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float.TryParse(timeText.text, out time);

        if (time > 2.5f || time <= 0)
        {
            StopCoroutine(FlickCoroutine());
            isStart = false;
        } else if (time < 2.5f && !isStart)
        {
            isStart = true;
            StartCoroutine(FlickCoroutine());
        }
    }

    IEnumerator FlickCoroutine()
    {
        while (isStart)
        {
            timeText.color = new Color32(255, 0, 0, 255);

            // наращиваем размер
            for (int i = 0; i < 10; i++)
            {
                timeText.fontSize = timeText.fontSize + 2;
                yield return new WaitForSeconds(0.03f);
            }
            // убавляем размер
            timeText.color = new Color32(255, 255, 255, 255);

            for (int i = 0; i < 10; i++)
            {
                timeText.fontSize = timeText.fontSize - 2;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}
