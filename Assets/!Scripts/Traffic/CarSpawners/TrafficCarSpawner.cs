using System;
using System.Collections.Generic;
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
        
        for (int i = 1; i <= Random.Range(minSpawnCars, maxSpawnCars); i++)
        {
            Spawn();
        }
    }
    

    private List<Vector3> GetSpawnPoints()
    {
        List<Vector3> spawnPoints = new List<Vector3>();
        List<TrafficLane> trafficLanes = new List<TrafficLane>();

        for (int i = 0; i < waypoints.Length; i++)
        {
            trafficLanes.Add(new TrafficLane(waypoints[i]));
        }

        foreach (var lane in trafficLanes)
        {
            foreach (var point in lane.GetPoints(roadSpawns))
            {
                spawnPoints.Add(point);
            }
        }

        return spawnPoints;
    }

    private void Spawn()
    {
        var spawnPosition = _spawnPositions.RandomItem();
        var trafficCar = _trafficCarFactory.Spawn(spawnPosition,
            spawnPosition.x < 0f ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.identity,
            transform);
        
        _spawnPositions.Remove(spawnPosition);
    }

    private void OnDrawGizmos()
    {
        if (waypoints == null)
            return;
        
        List<TrafficLane> lanes = new List<TrafficLane>();
        Gizmos.color = Color.red;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawLine(waypoints[i].point1.position, waypoints[i].point2.position);
            lanes.Add(new TrafficLane(waypoints[i]));
        }

        Gizmos.color = Color.white;
        foreach (var lane in lanes)
        {
            foreach (var point in lane.GetPoints(roadSpawns))
            {
                Gizmos.DrawSphere(point, 0.5f);
            }
        }
    }
}