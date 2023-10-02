using System;
using UnityEngine;

namespace Traffic
{
    [Serializable]
    public class TrafficCarConfig
    {
        [SerializeField] private TrafficCar _prefab;
        [SerializeField] private MinMax _speed;

        public TrafficCar Prefab => _prefab;
        public int Speed => _speed.Random();
    }
    
    [Serializable]
    public class MinMax
    {
        [SerializeField] private int _min;
        [SerializeField] private int _max;

        public int Random()
        {
            return UnityEngine.Random.Range(_min, _max);
        }
    }
}