using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Traffic
{
    public class TrafficCarFactory
    {
        private readonly TrafficConfig _configs;
        private readonly eBlockType _blockType;

        public TrafficCarFactory(TrafficConfig configs, eBlockType blockType)
        {
            _configs = configs;
            _blockType = blockType;
        }

        public TrafficCar Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            var config = GetConfig(_blockType);
            var trafficCar = Object.Instantiate(config.Prefab, position, rotation, parent);
            trafficCar.Launch(config.Speed);

            return trafficCar;
        }

        private TrafficCarConfig GetConfig(eBlockType type)
        {
            return type switch
            {
                eBlockType.City => _configs.CityTraffic.RandomItem(),
                eBlockType.Desert => _configs.DesertTraffic.RandomItem(),
                eBlockType.Highway => _configs.HighwayTraffic.RandomItem(),
                eBlockType.Forest => _configs.ForestTraffic.RandomItem(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}