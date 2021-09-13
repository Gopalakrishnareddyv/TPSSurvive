using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public GameObject help;
    public void Main()
    {
        main.SetActive(true);
        options.SetActive(false);
        help.SetActive(false);
    }
    public void Help()
    {
        main.SetActive(false);
        options.SetActive(false);
        help.SetActive(true);
    }
    public void Options()
    {
        main.SetActive(false);
        options.SetActive(true);
        help.SetActive(false);
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
