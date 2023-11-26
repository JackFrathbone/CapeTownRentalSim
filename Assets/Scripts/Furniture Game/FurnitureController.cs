using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FurnitureController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The name that shows up when first seeing the furniture")]
    [SerializeField] string _furnitureName;

    [SerializeField] float _rotationSpeed;
    [Tooltip("Which tiles this furniture can be placed on")]
    [SerializeField] List<TileBase> _validTiles = new();

    [Header("References")]
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private TextMeshPro _nameText;
    private FurnitureGameManager _gameManager;

    [Header("Data")]
    private bool _followMouse = false;
    private bool _firstClick = true;

    private bool _isBlocked = false;
    private bool _canPlace = false;
    private bool _canDelete = false;

    private int _collisionCount = 0;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _nameText = GetComponentInChildren<TextMeshPro>();
        _nameText.text = _furnitureName;

        _gameManager = GameObject.FindObjectOfType<FurnitureGameManager>();
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
            _nameText.gameObject.SetActive(false);
            Destroy(GetComponent<CustomCollisionDetection>());
            _spriteRenderer.color = Color.white;
            this.gameObject.tag = "Wall";
            _gameManager.AddLevelScore();
            Destroy(this);
        }
        else if (!_firstClick && _canDelete)
        {
            Destroy(gameObject);
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

            //Mouse scroll rotate
            float scrollDelta = Input.mouseScrollDelta.y;
            transform.Rotate(_rotationSpeed * scrollDelta * Vector3.forward);
            
            //Rotate via mouse right button
            if (Input.GetMouseButton(1))
            {
                transform.Rotate(_rotationSpeed * 0.1f * Vector3.forward);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _collisionCount++;
            CheckWallCollision();
        }
        else if (collision.CompareTag("Bin"))
        {
            _canDelete = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _collisionCount--;
            CheckWallCollision();
        }
        else if (collision.CompareTag("Bin"))
        {
            _canDelete = false;
        }
    }

    private void CheckWallCollision()
    {
        if (_collisionCount > 0)
        {
            _isBlocked = true;
        }
        else
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
