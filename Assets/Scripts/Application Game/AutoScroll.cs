using UnityEngine;
using UnityEngine.UI;

public class AutoScroll : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _scrollSpeed = 100f;

    [Header("References")]
    [SerializeField] ScrollRect _scrollView;

    [Header("Data")]
    private Vector2 _startPosition;
    private Vector2 _endPosition;
    private bool _startScrolling = false;

    private void Start()
    {
        // Store the initial position of the ScrollView content
        _startPosition = _scrollView.content.localPosition;

        // Calculate the desired end position based on the ScrollView's size and scroll direction
        _endPosition = _startPosition + new Vector2(0f, _scrollView.content.rect.height - _scrollView.viewport.rect.height);
    }

    private void Update()
    {
        if (_startScrolling)
        {
            // Move the ScrollView content towards the end position at a constant speed
            _scrollView.content.localPosition = Vector2.MoveTowards(_scrollView.content.localPosition, _endPosition, _scrollSpeed * Time.deltaTime);
        }
    }

    public void ResetPosition()
    {
        _startScrolling = true;
        _scrollView.content.localPosition = _startPosition;
    }

}
