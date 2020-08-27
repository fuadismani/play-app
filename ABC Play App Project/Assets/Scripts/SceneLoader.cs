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
    
}
