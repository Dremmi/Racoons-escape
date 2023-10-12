using UniRx;
using UnityEngine;

namespace Sound
{
    public abstract class ImpactSound : MonoBehaviour
    {
        [SerializeField] private bool _isSoundDisposed;

        private void OnEnable()
        {
            MessageBroker
                .Default
                .Receive<OnGameStartMessage>()
                .Subscribe(message =>
                {
                    InitializeImpactSound();
                });
        }

        private void InitializeImpactSound()
        {
            Debug.Log("Sound is ready");
            _isSoundDisposed = false;
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if(_isSoundDisposed)
                return;
            
            OnAnyCollision(collision);
        }
        
        protected virtual void OnAnyCollision(Collision collision) { }
    }
}