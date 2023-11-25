using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FurnitureController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float _rotationSpeed;
    [Tooltip("Which tiles this furniture can be placed on")]
    [SerializeField] List<TileBase> _validTiles = new List<TileBase>();

    [Header("References")]
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    [Header("Data")]
    private bool _followMouse = false;
    private bool _firstClick = true;

    private bool _isBlocked = false;
    private bool _canPlace = false;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        if (_firstClick)
        {
            _firstClick = false;
            _followMouse = true;
        }
        else if (!_firstClick && _canPlace)
        {
            Destroy(GetComponent<CustomCollisionDetection>());
            _spriteRenderer.color = Color.white;
            this.gameObject.tag = "Wall";
            Destroy(this);
        }
        else
        {
            return;
        }
    }

    private void Update()
    {
        if (_followMouse)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _rigidbody2D.MovePosition(mousePosition);

            float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
            transform.Rotate(Vector3.forward * (_rotationSpeed * 100) * scrollWheelInput * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _isBlocked = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _isBlocked = false;
        }
    }

    // Updates which current tiles the furniture is colliding with, and if not blocked by walls allows it to be placed
    public void UpdateTileInfo(TileBase tile)
    {
        if (tile == null)
        {
            _canPlace = false;
            _spriteRenderer.color = Color.white;
            return;
        }

        if (_validTiles.Contains(tile) && !_isBlocked)
        {
            _canPlace = true;
            _spriteRenderer.color = Color.green;
        }
        else
        {
            _canPlace = false;
            _spriteRenderer.color = Color.red;
        }
    }
}
