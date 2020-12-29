using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadTutorial1Scene()
    {
        SceneManager.LoadScene("Tutorial1");
    }
    public void LoadTutorial2Scene()
    {
        SceneManager.LoadScene("Tutorial2");
    }
    public void LoadTutorial3Scene()
    {
        SceneManager.LoadScene("Tutorial3");
    }
    public void LoadTutorial4Scene()
    {
        SceneManager.LoadScene("Tutorial4");
    }
}
