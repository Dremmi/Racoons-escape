using System;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class TrafficLane
    {
        private Waypoints _waypoints;

        public TrafficLane(Waypoints waypoints)
        {
            _waypoints = waypoints;
        }

        public IEnumerable<Vector3> GetPoints(int step)
        {
            float length = _waypoints.GetLength();
            var result = new List<Vector3>();
            for (int i = 0; i < step; i++)
            {
                result.Add(new Vector3(_waypoints.GetFirstPoint().x,
                    0,
                    _waypoints.GetFirstPoint().z + (length / (step - 1)) * i));
            }

            return result;
        }
    }

    [Serializable]
    public struct Waypoints
    {
        public Transform point1;
        public Transform point2;

        public float GetLength()
        {
            return (point1.position - point2.position).magnitude;
        }

        public Vector3 GetFirstPoint()
        {
            return point1.position.z < point2.position.z ? point1.position : point2.position;
        }
    }
}

