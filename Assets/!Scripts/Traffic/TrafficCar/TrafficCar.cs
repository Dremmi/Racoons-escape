using UnityEngine;

namespace Traffic
{
    [RequireComponent(typeof(TrafficCarMovement))]
    [RequireComponent(typeof(TrafficCarAI))]
    public class TrafficCar : MonoBehaviour
    {
        private LayerMask _layerMask;
        private TrafficCarMovement _carMovement;
        private TrafficCarAI _trafficCarAI;

        public void Launch(int speed)
        {
            _carMovement = GetComponent<TrafficCarMovement>();
            _carMovement.Launch(speed);
        }


    }
}