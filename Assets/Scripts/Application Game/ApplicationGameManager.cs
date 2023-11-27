using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationGameManager : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Text that is used to generate the names of the buttons that are to be matched")]
    [SerializeField] List<string> _requiredDocsStrings = new();

    [Header("References")]
    [SerializeField] List<Button> _docButtons = new();
    [SerializeField] List<Button> _appButtons = new();

    [SerializeField] TextMeshProUGUI _applicationLabelText;

    private AutoScroll _autoScroll;
    private MatchController _matchController;

    [SerializeField] RenderTexture _level1RenderTexture;
    [SerializeField] RenderTexture _level2RenderTexture;
    [SerializeField] RenderTexture _level3RenderTexture;

    [SerializeField] RawImage _levelEndImage;

    [SerializeField] GameObject _endScreenPass;
    [SerializeField] GameObject _endScreenFail;
    [SerializeField] GameObject _endScreenEnd;

    [Header("Data")]
    private int _currentApplication = 1;
    private int _currentLevel = 0;
    private int _firstChoiceLevel;
    private int _secondChoiceLevel;
    private int _thirdChoiceLevel;


    private void Start()
    {
        _autoScroll = GetComponent<AutoScroll>();
        _matchController = GetComponent<MatchController>();

        _endScreenPass.SetActive(false);
        _endScreenFail.SetActive(false);
        _endScreenEnd.SetActive(false);

        SetScores();
        SetLabel();
        SetButtons();
    }

    private void SetScores()
    {
        int _level1Score = PlayerPrefs.GetInt("Level1Score");
        int _level2Score = PlayerPrefs.GetInt("Level2Score");
        int _level3Score = PlayerPrefs.GetInt("Level3Score");

        if (_level1Score >= _level2Score && _level1Score >= _level3Score)
        {
            _firstChoiceLevel = 1;
            _level1Score = -1;
        }
        else if (_level2Score >= _level1Score && _level2Score >= _level3Score)
        {
            _firstChoiceLevel = 2;
            _level2Score = -1;
        }
        else if (_level3Score >= _level1Score && _level3Score >= _level1Score)
        {
            _firstChoiceLevel = 3;
            _level3Score = -1;
        }

        if (_level1Score >= _level2Score && _level1Score >= _level3Score)
        {
            _secondChoiceLevel = 1;
            _level1Score = -1;
        }
        else if (_level2Score >= _level1Score && _level2Score >= _level3Score)
        {
            _secondChoiceLevel = 2;
            _level2Score = -1;
        }
        else if (_level3Score >= _level1Score && _level3Score >= _level1Score)
        {
            _secondChoiceLevel = 3;
            _level3Score = -1;
        }

        if (_level1Score != -1)
        {
            _thirdChoiceLevel = 1;
        }
        else if (_level2Score != -1)
        {
            _thirdChoiceLevel = 2;
        }
        else if (_level3Score != -1)
        {
            _thirdChoiceLevel = 3;
        }
    }

    private void SetLabel()
    {
        if (_currentApplication == 1)
        {
            if (_firstChoiceLevel == 1)
            {
                _currentLevel = 1;
                _applicationLabelText.text = "Rental Application-Gardens";
            }
            else if (_firstChoiceLevel == 2)
            {
                _currentLevel = 2;
                _applicationLabelText.text = "Rental Application-Observatory";
            }
            else if (_firstChoiceLevel == 3)
            {
                _currentLevel = 3;
                _applicationLabelText.text = "Rental Application-CBD";
            }
        }
        else if (_currentApplication == 2)
        {
            if (_secondChoiceLevel == 1)
            {
                _currentLevel = 1;
                _applicationLabelText.text = "Rental Application-Gardens";
            }
            else if (_secondChoiceLevel == 2)
            {
                _currentLevel = 2;
                _applicationLabelText.text = "Rental Application-Observatory";
            }
            else if (_secondChoiceLevel == 3)
            {
                _currentLevel = 3;
                _applicationLabelText.text = "Rental Application-CBD";
            }
        }
        else if (_currentApplication == 3)
        {
            if (_thirdChoiceLevel == 1)
            {
                _currentLevel = 1;
                _applicationLabelText.text = "Rental Application-Gardens";
            }
            else if (_thirdChoiceLevel == 2)
            {
                _currentLevel = 2;
                _applicationLabelText.text = "Rental Application-Observatory";
            }
            else if (_thirdChoiceLevel == 3)
            {
                _currentLevel = 3;
                _applicationLabelText.text = "Rental Application-CBD";
            }
        }
    }

    private void SetButtons()
    {
        ListShuffle(_docButtons);
        ListShuffle(_appButtons);

        for (int i = 0; i < _requiredDocsStrings.Count; i++)
        {
            _docButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _requiredDocsStrings[i];
            _appButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = _requiredDocsStrings[i];
        }
    }

    private void ListShuffle<T>(List<T> list)
    {
        System.Random random = new();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public void LoadEndScreen()
    {
        bool passed = _matchController.MatchCheck(8);

        if (passed)
        {
            ShowEndScreenPass(_currentLevel);
        }
        else
        {
            ShowEndScreenFail(_currentLevel);
        }
    }

    public void LoadNextApplication()
    {
        _currentApplication++;
        SetLabel();
        _autoScroll.ResetPosition();

        foreach (Button button in _appButtons)
        {
            button.GetComponent<Image>().color = Color.red;
        }
    }

    private void ShowEndScreenPass(int level)
    {
        _endScreenPass.SetActive(true);

        TextMeshProUGUI text = _endScreenPass.GetComponentInChildren<TextMeshProUGUI>();

        if(level == _firstChoiceLevel)
        {
            text.text = "Great, you managed to your get your 1st choice!\r\nHope you are happy in your new place!";
        }
        else if (level == _secondChoiceLevel)
        {
            text.text = "Great, you managed to your get your 2nd choice!\r\nHope you are happy in your new place!";
        }
        else if (level == _thirdChoiceLevel)
        {
            text.text = "Great, you managed to your get your 3rd choice!\r\nHope you are happy in your new place!";
        }

        switch (level)
        {
            case 1:

                _levelEndImage.texture = _level1RenderTexture;
                break;
            case 2:
                _levelEndImage.texture = _level2RenderTexture;
                break;
            case 3:
                _levelEndImage.texture = _level3RenderTexture;
                break;
        }
    }

    private void ShowEndScreenFail(int level)
    {
        if(level != 3)
        {
            _endScreenFail.SetActive(true);
        }
        else
        {
            _endScreenEnd.SetActive(true);
        }
    }
}
