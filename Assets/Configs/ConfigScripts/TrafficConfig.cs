using System;
using System.Collections.Generic;
using UnityEngine;
using Traffic;

[CreateAssetMenu(fileName = "TrafficConfig", menuName = "Configs/TrafficConfig")]
    public class TrafficConfig : ScriptableObject
    {
        public List<TrafficCarConfig> CityTraffic;
        public List<TrafficCarConfig> DesertTraffic;
        public List<TrafficCarConfig> ForestTraffic;
        public List<TrafficCarConfig> HighwayTraffic;
    }
