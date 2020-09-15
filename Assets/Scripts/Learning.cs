using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Learning : MonoBehaviour
{
    #region Fingers
    [SerializeField]
    public GameObject leftRightFinger;
    [SerializeField]
    public GameObject upDownFinger;
    [SerializeField]
    public Animation animLeftRight;
    [SerializeField]
    public Animation animUpDown;
    #endregion

    [SerializeField]
    public GameObject targetPointer;


    [SerializeField]
    public GameObject tapToContinueButton;
    [SerializeField]
    public Text tapToContinueText;

    [SerializeField]
    public GameObject helpTextObject;
    private Text helpText;

    // Start is called before the first frame update
    void Start()
    {
        if (Utils.FirstPlay)
        {
            targetPointer.SetActive(true);
            StartCoroutine("LeftRightCoroutine");
        }
    }

    private IEnumerator LeftRightCoroutine()
    {
        leftRightFinger.SetActive(true);

        helpTextObject.SetActive(true);
        helpText = helpTextObject.GetComponent<Text>();

        helpText.text = LocalizationManager.GetTranslate(LocalizationManager.SIDE_SWIPE_KEY);

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

        helpText.text = LocalizationManager.GetTranslate(LocalizationManager.VERTICAL_SWIPE_KEY);

        animUpDown.wrapMode = WrapMode.Loop;
        animUpDown.Play();

        // пока не свайпнули в Cube.cs
        while (Utils.SecondPhase)
        {
            yield return new WaitForSeconds(0.1f);
        }

        upDownFinger.SetActive(false);

        ShowTapToContinueButton();
    }

    private void ShowTapToContinueButton()
    {
        tapToContinueText.text = LocalizationManager.GetTranslate(LocalizationManager.TAP_TO_CONTINUE_KEY);
        tapToContinueButton.SetActive(true);
        Utils.TapToContinueButtonShowing = true;

        helpText.text = LocalizationManager.GetTranslate(LocalizationManager.HURRY_UP_KEY);
    }

    public void TapToContinueButtonClick()
    {
        Utils.TapToContinueButtonShowing = false;
        targetPointer.SetActive(false);
        tapToContinueButton.SetActive(false);
        helpTextObject.SetActive(false);
    }
}
