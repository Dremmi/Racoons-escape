using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Traffic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrafficCarSpawner : MonoBehaviour
{
    [SerializeField] private  int minSpawnCars = 4;
    [SerializeField] private  int maxSpawnCars = 16;
    [SerializeField] private Waypoints[] waypoints;
    [SerializeField] private int roadSpawns;
    
    private TrafficCarFactory _trafficCarFactory;
    private List<Vector3> _spawnPositions;

    public void Launch(TrafficConfig trafficConfig, eBlockType blockType)
    {
        _trafficCarFactory = new TrafficCarFactory(trafficConfig, blockType);
        _spawnPositions = GetSpawnPoints();

        var range = Random.Range(minSpawnCars, maxSpawnCars);
        for (var i = 1; i <= range; i++)
        {
            Spawn();
        }
    }
    

    private List<Vector3> GetSpawnPoints()
    {
        return waypoints.Select(t => new TrafficLane(t))
            .SelectMany(lane => lane.GetPoints(roadSpawns))
            .ToList();
    }

    private void Spawn()
    {
        var spawnPosition = _spawnPositions.RandomItem();
        var rotation = spawnPosition.x < 0f ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity;
        _trafficCarFactory.Spawn(spawnPosition, rotation, transform);
        _spawnPositions.Remove(spawnPosition);
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null)
            return;
        
        var points = GetTrafficLanes()
            .SelectMany(lane => lane.GetPoints(roadSpawns))
            .ToList();
        
        Gizmos.color = Color.white;
        foreach (var point in points)
        {
            Gizmos.DrawSphere(point, 0.5f);
        }
    }

    private IEnumerable<TrafficLane> GetTrafficLanes()
    {
        Gizmos.color = Color.red;
        foreach (var waypoint in waypoints)
        {
            Gizmos.DrawLine(waypoint.Point1.position, waypoint.Point2.position);
            yield return new TrafficLane(waypoint);
        }
    }
}