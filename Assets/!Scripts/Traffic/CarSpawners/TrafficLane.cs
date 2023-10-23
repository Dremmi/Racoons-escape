using System;
using System.Collections.Generic;
using UnityEngine;

namespace Traffic
{
    public class TrafficLane
    {
        private readonly Waypoints _waypoints;

        public TrafficLane(Waypoints waypoints)
        {
            _waypoints = waypoints;
        }

        public IEnumerable<Vector3> GetPoints(int step)
        {
            var length = _waypoints.GetLength();
            for (var i = 0; i < step; i++)
            {
                var firstPoint = _waypoints.GetFirstPoint();
                yield return new Vector3(firstPoint.x, 0, firstPoint.z + length / (step - 1) * i);
            }
        }
    }

    [Serializable]
    public struct Waypoints
    {
        public Transform Point1;
        public Transform Point2;

        public readonly float GetLength()
        {
            var delta = Point1.position - Point2.position;
            return delta.magnitude;
        }

        public readonly Vector3 GetFirstPoint()
        {
            return Point1.position.z < Point2.position.z ? Point1.position : Point2.position;
        }
    }
}

