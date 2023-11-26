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

    private void Start()
    {
        PlayerPrefs.DeleteAll();

        _screenshot = GetComponent<Screenshot>();
        LoadNextLevel();
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
            _screenshot.TakeLevelScreenshot(_onLevel);
            _endScreen.SetActive(true);
        }
    }

    //Loads next level in the list
    public void LoadNextLevel()
    {
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
