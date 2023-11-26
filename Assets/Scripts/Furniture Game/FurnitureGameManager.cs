using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FurnitureGameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] GameObject _endScreen;

    [SerializeField] TextMeshProUGUI _levelName;

    //A list of the specific apartment prefabs that are loaded
    [SerializeField] List<GameObject> _levels = new();

    [SerializeField] FurnitureSpawner _furnitureSpawner;

    private Screenshot _screenshot;

    [Header("Data")]
    [SerializeField] float _timerDefault;
    private float _timer;
    private GameObject _currentLoadedLevel;

    private int _onLevel;
    private int _currentLevelScore;

    private bool _gameStarted;

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        _screenshot = GetComponent<Screenshot>();

        _furnitureSpawner.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_gameStarted)
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;

                _timerText.text = _timer.ToString("F0") + " seconds left...";
                _screenshot.TakeLevelScreenshot(_onLevel);
            }
            else
            {
                OpenEndScreen();
                //Invoke("OpenEndScreen", 0.15f);
            }
        }
    }

    private void OpenEndScreen()
    {
        _endScreen.SetActive(true);
        _furnitureSpawner.gameObject.SetActive(false);

        if(_onLevel == 2)
        {
            _endScreen.GetComponentInChildren<TextMeshProUGUI>().text = "That's all the houses for today, whew hope you got that all! Now it's time for you to fill our your applications...";
        }
    }

    //Loads next level in the list
    public void LoadNextLevel()
    {
        _gameStarted = true;
        _furnitureSpawner.gameObject.SetActive(true);


        if (_levels.Count != 0)
        {
            if (_currentLoadedLevel != null)
            {
                _onLevel++;
                SetLevelEndScore(_onLevel);

                Destroy(_currentLoadedLevel);
                _furnitureSpawner.ClearPlacedFurniture();
            }

            _timer = _timerDefault;
            _endScreen.SetActive(false);

            _currentLoadedLevel = Instantiate(_levels[0]);
            _levels.Remove(_levels[0]);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        if (_onLevel == 0)
        {
            _levelName.text = "<Place 1: Gardens>";
        }
        else if (_onLevel == 1)
        {
            _levelName.text = "<Place 2: Observatory>";
        }
        else if (_onLevel == 2)
        {
            _levelName.text = "<Place 3: CBD>";
        }
    }

    public void AddLevelScore()
    {
        _currentLevelScore++;
    }

    private void SetLevelEndScore(int level)
    {
        PlayerPrefs.SetInt("Level" + level.ToString() + "Score", _currentLevelScore);
        _currentLevelScore = 0;
    }
}
