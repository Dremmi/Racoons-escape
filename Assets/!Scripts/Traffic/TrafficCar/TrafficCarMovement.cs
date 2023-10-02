using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Traffic
{
    public class TrafficCarMovement : MonoBehaviour
    {
        private int _speed;
        private float _slowSpeed;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        public void Launch(int speed)
        {
            _speed = speed;
        }

        private void FixedUpdate()
        {
            Drive();
        }

        public void Drive()
        {
            _rigidbody.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime * 0.01f);
        }
        
        public void SlowDown()
        {
            _rigidbody.MovePosition(transform.position + 
                                    transform.forward *
                                    Mathf.MoveTowards(_speed, _slowSpeed, Time.deltaTime));
        }
        public void Stop()
        {
            _rigidbody.MovePosition(transform.position +
                                    transform.forward *
                                    Mathf.MoveTowards(_speed, 0f, Time.deltaTime));
        }
    }
}