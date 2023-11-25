using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class CustomCollisionDetection : MonoBehaviour
{
    [Header("References")]
    private Tilemap _tilemap;
    private FurnitureController _furnitureController;

    void Start()
    {
        GameObject tilemapObj = GameObject.FindGameObjectWithTag("Floor");
        _tilemap = tilemapObj.GetComponent<Tilemap>();
        _furnitureController = GetComponent<FurnitureController>();
    }

    void Update()
    {
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