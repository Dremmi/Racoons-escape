using UnityEngine;
using System.Collections.Generic;
using Extensions;
using Unity.Mathematics;

public class BuildingSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> buildingSpawnPoints;
    [SerializeField] private List<GameObject> _testNewBuldings;
    [SerializeField] private Building buildingPrefab;
    [SerializeField] private List<int> smallBuildingIndexes;

    private BuildingSpawnConfig _buildingSpawnConfig;
    
    private bool _isOnLeftStreetSide;

    public void Launch(BuildingSpawnConfig buildingSpawnConfig)
    {
        // _buildingSpawnConfig = buildingSpawnConfig;
        //
        // for (int i = 0; i < buildingSpawnPoints.Count; i++)
        // {
        //     Building building = Instantiate(buildingPrefab, transform.position + buildingSpawnPoints[i], Quaternion.identity, transform);
        //     
        //     _isOnLeftStreetSide = buildingSpawnPoints[i].x < 0;
        //
        //     building.Launch(_buildingSpawnConfig, smallBuildingIndexes, i, _isOnLeftStreetSide);
        // }
        for (int i = 0; i < buildingSpawnPoints.Count; i++)
        {
            Instantiate(_testNewBuldings.RandomItem(),
                buildingSpawnPoints[i].position,
                buildingSpawnPoints[i].position.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity,
                transform);
        }
    }
}