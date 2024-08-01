using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    public GameObject loadGraphic;

    public void Load(int index)
    {
        loadGraphic.SetActive(true);
        SceneManager.LoadSceneAsync(index);
    }

    public void Reset()
    {
        loadGraphic.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
