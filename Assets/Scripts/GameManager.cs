using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadLevel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
