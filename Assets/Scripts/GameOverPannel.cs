using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverPannel : MonoBehaviour
{
    public void RestartButtonClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
