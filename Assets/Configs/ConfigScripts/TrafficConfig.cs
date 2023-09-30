using System.Collections.Generic;
using UnityEngine;
using Traffic;

[CreateAssetMenu(fileName = "TrafficConfig", menuName = "Configs/TrafficConfig")]
    public class TrafficConfig : ScriptableObject
    {
        [Header("TrafficCars")]
        public List<TrafficCar> CityTraffic;
        public List<TrafficCar> DesertTraffic;
        public List<TrafficCar> ForestTraffic;
        public List<TrafficCar> HighwayTraffic;
    }
