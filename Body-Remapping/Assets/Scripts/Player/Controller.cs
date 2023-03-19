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
            velocity = direction.normalized * speed.Value;
        }

        private void FixedUpdate()
        {
            body.velocity = velocity;
        }
    }
}
