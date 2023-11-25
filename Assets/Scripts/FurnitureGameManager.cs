using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FurnitureGameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] GameObject _endScreen;

    [Header("Data")]
    [SerializeField] float _timerDefault;
    private float _timer;

    private void Start()
    {
        _timer = _timerDefault;
        _endScreen.SetActive(false);
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;

            _timerText.text = _timer.ToString("F0") + " seconds left...";
        }
        else
        {
            _endScreen.SetActive(true);
        }
    }

    //Loads next level in the list
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
