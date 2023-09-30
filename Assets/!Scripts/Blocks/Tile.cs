using UnityEngine;
public class Tile : MonoBehaviour
{
    private BuildingSpawner _buildingSpawner;
    private TrafficCarSpawner _trafficCarSpawner;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficConfig _trafficConfig;

    public void Launch(BuildingSpawnConfig buildingSpawnConfig, TrafficConfig trafficConfig, eBlockType blockType)
    {
        _buildingSpawnConfig = buildingSpawnConfig;
        _trafficConfig = trafficConfig;

        _buildingSpawner = GetComponent<BuildingSpawner>();
        _trafficCarSpawner = GetComponent<TrafficCarSpawner>();


        if (_buildingSpawner != null)
            _buildingSpawner.Launch(_buildingSpawnConfig);
        
        if (_trafficCarSpawner != null)
            _trafficCarSpawner.Launch(_trafficConfig, blockType);
    }
}