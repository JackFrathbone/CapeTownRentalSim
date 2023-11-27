using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomCollisionDetection : MonoBehaviour
{
    [Header("References")]
    private Tilemap _tilemap;
    private FurnitureController _furnitureController;

    void Start()
    {
        _furnitureController = GetComponent<FurnitureController>();
    }

    void Update()
    {
        if (_tilemap == null)
        {
            GameObject tilemapObj = GameObject.FindGameObjectWithTag("Floor");

            if (tilemapObj != null)
            {
                _tilemap = tilemapObj.GetComponent<Tilemap>();
            }

            return;
        }

        Vector3 position = transform.position;
        Vector3Int cell = _tilemap.WorldToCell(position);

        if (_tilemap.HasTile(cell))
        {
            // Get the type of the tile that was collided with
            TileBase tile = _tilemap.GetTile(cell);
            _furnitureController.UpdateTileInfo(tile);
        }
        else
        {
            _furnitureController.UpdateTileInfo(null);
        }
    }
}