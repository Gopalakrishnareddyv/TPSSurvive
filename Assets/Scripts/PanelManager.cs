using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public void Main()
    {
        main.SetActive(true);
        options.SetActive(true);
    }
    public void Options()
    {
        main.SetActive(false);
        options.SetActive(true);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
