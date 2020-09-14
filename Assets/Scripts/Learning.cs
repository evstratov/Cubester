using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Learning : MonoBehaviour
{
    [SerializeField]
    public GameObject leftRightFinger;
    [SerializeField]
    public GameObject upDownFinger;
    [SerializeField]
    public Animation animLeftRight;
    [SerializeField]
    public Animation animUpDown;

    [SerializeField]
    public GameObject panelConfirmation;

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

    private IEnumerator LeftRightCoroutine()
    {
        leftRightFinger.SetActive(true);
        Utils.FirstPhase = true;

        animLeftRight.wrapMode = WrapMode.Loop;
        animLeftRight.Play();

        // пока не свайпнули в Cube.cs
        while (Utils.FirstPhase)
        {
            yield return new WaitForSeconds(0.1f);
        }

        leftRightFinger.SetActive(false);
        StartCoroutine("UpDownCoroutine");
    }

    private IEnumerator UpDownCoroutine()
    {
        upDownFinger.SetActive(true);
        Utils.SecondPhase = true;

        animUpDown.wrapMode = WrapMode.Loop;
        animUpDown.Play();

        // пока не свайпнули в Cube.cs
        while (Utils.SecondPhase)
        {
            yield return new WaitForSeconds(0.1f);
        }

        upDownFinger.SetActive(false);

        ShowConfirmationPanel();
    }

    private void ShowConfirmationPanel()
    {
        panelConfirmation.SetActive(true);
        Utils.ConfirmationPanelShowing = true;
    }

    public void ConfirmationButtonClick()
    {
        Utils.ConfirmationPanelShowing = false;
        panelConfirmation.SetActive(false);
    }
}
