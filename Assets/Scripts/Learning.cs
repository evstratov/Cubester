using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Learning : MonoBehaviour
{
    [SerializeField]
    public GameObject leftRightFinger;
    [SerializeField]
    public GameObject upDownFinger;
    
    public Animation animLeftRight;
    public Animation animUpDown;

    private bool isFirstPlay;
    // Start is called before the first frame update
    void Start()
    {
        isFirstPlay = Utils.FirstPlay;

        if (isFirstPlay)
        {
            StartCoroutine("LeftRightCoroutine");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LeftRightCoroutine()
    {
        leftRightFinger.SetActive(true);
        animLeftRight.Play("RightLeftFinger");
        leftRightFinger.SetActive(false);
        yield return null;
    }
}
