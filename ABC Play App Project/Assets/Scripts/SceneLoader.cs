using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void BackToDashboard()
    {
        SceneManager.LoadScene("Dashboard");
        Debug.Log("BACK!");
    }

    public void PlayARCamera()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToARCamera()
    {
        SceneManager.LoadScene("ARCamera");
    }

    public void GoToVideo()
    {
        SceneManager.LoadScene("Video");
    }

    public void GoToHowTo()
    {
        SceneManager.LoadScene("HowTo");
    }

    public void GoToAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void OpenCard()
    {
        Application.OpenURL("https://bit.ly/35xwues");
    }
}
