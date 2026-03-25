using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameObject MainMenu;
    //void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.Escape))
    //    {
    //        if(MainMenu.activeSelf)
    //        {
    //            MainMenu.SetActive(false);
    //        }
    //        else
    //        {
    //            MainMenu.SetActive(true);
    //        }
    //    }
    //}

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
