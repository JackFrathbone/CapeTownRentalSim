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

    [SerializeField]FurnitureSpawner _furnitureSpawner;

    [Header("Data")]
    [SerializeField] float _timerDefault;
    private float _timer;
    private GameObject _currentLoadedLevel;

    private int _currentLevelScore;

    private void Start()
    {
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
            _endScreen.SetActive(true);
        }
    }

    //Loads next level in the list
    public void LoadNextLevel()
    {
        if(_levels.Count != 0)
        {
            if(_currentLoadedLevel != null)
            {
                _levels.Remove(_levels[0]);
                Destroy(_currentLoadedLevel);
                _furnitureSpawner.ClearPlacedFurniture();
            }

            _timer = _timerDefault;
            _endScreen.SetActive(false);

            _currentLoadedLevel = Instantiate(_levels[0]);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddLevelScore()
    {
        _currentLevelScore++;
    }
}
