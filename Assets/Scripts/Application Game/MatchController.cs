using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MatchController : MonoBehaviour
{
    [Header("References")]
    private ApplicationGameManager _gameManager;

    [Header("Data")]
    private Button _selectedDocButton;
    private int _matchesMade;

    private void Start()
    {
        _gameManager = GetComponent<ApplicationGameManager>();
    }

    public void SelectDocButton(Button selectedButton)
    {
        _selectedDocButton = selectedButton;
    }

    public void SelectAppButton(Button selectedButton)
    {
        CheckButtonMatch(selectedButton);
    }

    private void CheckButtonMatch(Button selectedButton)
    {
        if (_selectedDocButton == null || selectedButton == null)
        {
            return;
        }


        if (_selectedDocButton.GetComponentInChildren<TextMeshProUGUI>().text == selectedButton.GetComponentInChildren<TextMeshProUGUI>().text)
        {
            if (selectedButton.GetComponent<Image>().color != Color.green)
            {
                selectedButton.GetComponent<Image>().color = Color.green;
                _matchesMade++;
            }
        }
    }

    public bool MatchCheck(int count)
    {
        if (_matchesMade == count)
        {
            _matchesMade = 0;
            return true;
        }
        else
        {
            _matchesMade = 0;
            return false;
        }
    }
}
