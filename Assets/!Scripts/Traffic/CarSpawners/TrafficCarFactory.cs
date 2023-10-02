using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Traffic
{
    public class TrafficCarFactory
    {
        private TrafficConfig _configs;
        private eBlockType _blockType;

        public TrafficCarFactory(TrafficConfig configs, eBlockType blockType)
        {
            _configs = configs;
            _blockType = blockType;
        }

        public TrafficCar Spawn(Vector3 position)
        {
            var config = GetConfig(_blockType);
            var trafficCar = Object.Instantiate(config.Prefab, position, Quaternion.identity);
            trafficCar.Launch(config.Speed);
            
            return trafficCar;
        }
        
        public TrafficCar Spawn(Vector3 position, Quaternion rotation)
        {
            var config = GetConfig(_blockType);
            var trafficCar = Object.Instantiate(config.Prefab, position, rotation);
            trafficCar.Launch(config.Speed);
            
            return trafficCar;
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
            switch (type)
            {
                case eBlockType.City:
                    return _configs.CityTraffic.RandomItem();
                case eBlockType.Desert:
                    return _configs.DesertTraffic.RandomItem();
                case eBlockType.Highway:
                    return _configs.HighwayTraffic.RandomItem();
                case eBlockType.Forest:
                    return _configs.ForestTraffic.RandomItem();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}