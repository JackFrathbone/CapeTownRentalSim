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

    [Header("Data")]
    private int _currentApplication = 1;
    private int _firstChoiceLevel;
    private int _secondChoiceLevel;
    private int _thirdChoiceLevel;

    private bool _firstChoicePass;
    private bool _secondChoicePass;
    private bool _thirdChoicePass;


    private void Start()
    {
        _autoScroll = GetComponent<AutoScroll>();
        _matchController = GetComponent<MatchController>();

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
                _applicationLabelText.text = "Rental Application: Place 1";
            }
            else if (_firstChoiceLevel == 2)
            {
                _applicationLabelText.text = "Rental Application: Place 2";
            }
            else if (_firstChoiceLevel == 3)
            {
                _applicationLabelText.text = "Rental Application: Place 3";
            }
        }
        else if (_currentApplication == 2)
        {
            if (_secondChoiceLevel == 1)
            {
                _applicationLabelText.text = "Rental Application: Place 1";
            }
            else if (_secondChoiceLevel == 2)
            {
                _applicationLabelText.text = "Rental Application: Place 2";
            }
            else if (_secondChoiceLevel == 3)
            {
                _applicationLabelText.text = "Rental Application: Place 3";
            }
        }
        else if (_currentApplication == 3)
        {
            if (_thirdChoiceLevel == 1)
            {
                _applicationLabelText.text = "Rental Application: Place 1";
            }
            else if (_thirdChoiceLevel == 2)
            {
                _applicationLabelText.text = "Rental Application: Place 2";
            }
            else if (_thirdChoiceLevel == 3)
            {
                _applicationLabelText.text = "Rental Application: Place 3";
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

    public void LoadNextApplication()
    {
        _currentApplication++;
        SetLabel();
        _autoScroll.ResetPosition();

        foreach (Button button in _appButtons)
        {
            button.GetComponent<Image>().color = Color.red;
        }

        bool passed = _matchController.MatchCheck(8);

        if (_currentApplication == 1)
        {
            _firstChoicePass = passed;
        }
        else if (_currentApplication == 2)
        {
            _secondChoicePass = passed;
        }
        else if (_currentApplication == 3)
        {
            _thirdChoicePass = passed;
        }

    }
}
