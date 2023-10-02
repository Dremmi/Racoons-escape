using System;
using Traffic;
using UnityEngine;

public class TrafficCarAI : MonoBehaviour
{
    private TrafficCarState _state;
    private TrafficCarMovement _movement;
    
    public void Launch(TrafficCarMovement movement)
    {
        _movement = movement;
    }

    private void Update()
    {
        RealiseRay();
        
    }

    private void RealiseRay()
    {
        
    }
}
