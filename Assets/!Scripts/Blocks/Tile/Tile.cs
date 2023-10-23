using UnityEngine;
public class Tile : MonoBehaviour
{
    private BuildingSpawner _buildingSpawner;
    private TrafficCarSpawner _trafficCarSpawner;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficConfig _trafficConfig;

    public void LaunchTraffic(TrafficConfig trafficConfig, eBlockType blockType)
    {
        // Debug.Log($"Traffic spawned on tile : {gameObject.name}");
        _trafficConfig = trafficConfig;
        _trafficCarSpawner = GetComponent<TrafficCarSpawner>();
        if (_trafficCarSpawner != null)
            _trafficCarSpawner.Launch(_trafficConfig, blockType);
    }

    public void LaunchBuildings(BuildingSpawnConfig buildingSpawnConfig)
    {
        _buildingSpawnConfig = buildingSpawnConfig;
        _buildingSpawner = GetComponent<BuildingSpawner>();
        if (_buildingSpawner != null)
            _buildingSpawner.Launch(_buildingSpawnConfig);
    }
}