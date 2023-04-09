using System;
using ReferenceVariables;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] private Vector2Variable movementInput;
        [SerializeField] private FloatReference speed;
        [SerializeField] private Transform headset;
        
        private Rigidbody body;
        private Vector3 velocity;
        
        private void Awake()
        {
            movementInput.ValueChanged += OnMovementInputValueChanged;
            body = GetComponent<Rigidbody>();
        }

        private void OnMovementInputValueChanged(object sender, EventArgs e)
        {
            var direction = new Vector3(movementInput.Value.x, 0, movementInput.Value.y);
            var forward = headset.forward;
            forward.y = 0;
            var right = headset.right;
            right.y = 0;
            velocity = right * direction.x + forward * direction.z;
            velocity *= speed.Value;
        }

        private void FixedUpdate()
        {
            body.velocity = velocity;
        }
    }
}
