using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public GameObject pausePanel;
    public void PauseButtonClick()
    {
        Utils.PauseGame = true;
        pausePanel.SetActive(true);
    }

    public void PlayButtonClick()
    {
        pausePanel.SetActive(false);
        Utils.PauseGame = false;
    }

    public void RestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void HomeButtonClick()
    {
        // TODO: main menu
    }
}
