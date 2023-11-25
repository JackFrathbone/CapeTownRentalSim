using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [Header("References")]
    private FurnitureController _currentFurnitureController;

    [Header("Data")]
    [SerializeField] List<GameObject> _furniturePrefabsToSpawn = new List<GameObject>();

    private void Update()
    {
        if(_currentFurnitureController == null && _furniturePrefabsToSpawn.Count != 0)
        {
            _currentFurnitureController = Instantiate(_furniturePrefabsToSpawn[0], transform.position, Quaternion.identity, transform).GetComponent<FurnitureController>();
            _furniturePrefabsToSpawn.Remove(_furniturePrefabsToSpawn[0]);
        }
    }
}
