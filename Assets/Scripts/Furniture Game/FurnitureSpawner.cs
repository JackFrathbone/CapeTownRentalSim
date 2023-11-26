using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [Header("References")]
    private FurnitureController _currentFurnitureController;

    [Header("Data")]
    [SerializeField] List<GameObject> _furniturePrefabsToSpawn = new();
    private List<GameObject> _playthroughFurniturePrefabs = new();
    private List<GameObject> _placedFurniture = new();

    private void Start()
    {
        ResetFurnitureList();
    }

    private void Update()
    {
        if(_currentFurnitureController == null && _playthroughFurniturePrefabs.Count != 0)
        {
            _currentFurnitureController = Instantiate(_playthroughFurniturePrefabs[0], transform.position, Quaternion.identity, transform).GetComponent<FurnitureController>();
            _playthroughFurniturePrefabs.Remove(_playthroughFurniturePrefabs[0]);

            _placedFurniture.Add(_currentFurnitureController.gameObject);
        }
    }

    private void ResetFurnitureList()
    {
        _playthroughFurniturePrefabs.Clear();
        foreach (GameObject obj in _furniturePrefabsToSpawn)
        {
            _playthroughFurniturePrefabs.Add(obj);
        }
    }

    public void ClearPlacedFurniture()
    {
        foreach(GameObject obj in _placedFurniture)
        {
            Destroy(obj, 0.1f);
        }

        ResetFurnitureList();
    }
}
