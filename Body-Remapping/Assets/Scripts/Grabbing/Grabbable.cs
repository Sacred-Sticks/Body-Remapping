using UnityEngine;

namespace Grabbing
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabbable : MonoBehaviour
    {
        public Rigidbody Body
        {
            get
            {
                return body;
            }
            private set
            {
                body = value;
            }
        }
    
        private Rigidbody body;

        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
        }
    }
}
