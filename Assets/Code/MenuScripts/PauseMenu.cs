using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Container;

    public bool isPaused;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
       
    }

    public void ResumeButton()
    {
        ResumeGame();
    }

    public void MainMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void PauseGame()
    {
        Container.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Container.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

}
