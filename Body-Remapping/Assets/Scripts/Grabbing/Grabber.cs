using System;
using System.Linq;
using ReferenceVariables;
using UnityEngine;

namespace Grabbing
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private FloatVariable gripInput;
        [SerializeField] private FloatReference inputTolerance;
        [Space]
        [SerializeField] private Transform palm;
        [SerializeField] private FloatReference reachDistance;

        private Rigidbody body;

        private bool isGrabbing;
        private GameObject heldObject;
        private Transform grabPoint;
        private FixedJoint localJoint;
        private FixedJoint externalJoint;

        private Grabbable grabbed;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            gripInput.ValueChanged += OnGripChanged;
        }

        private void OnDisable()
        {
            gripInput.ValueChanged -= OnGripChanged;
            Release();
        }

        private void OnGripChanged(object sender, EventArgs e)
        {
            if (gripInput.Value < 1 - inputTolerance.Value)
                Release();
            if (gripInput.Value > inputTolerance.Value)
                Grab();
        }

        private void Grab()
        {
            if (isGrabbing || heldObject)
                return;
            var objectToGrab = Physics.OverlapSphere(palm.position, reachDistance.Value)
                .FirstOrDefault(c => c.GetComponent<Grabbable>());
            if (!objectToGrab)
                return;
            grabbed = objectToGrab.GetComponent<Grabbable>();
            heldObject = objectToGrab.gameObject;

            isGrabbing = true;

            localJoint = CreateJoint(gameObject, grabbed.Body);
            externalJoint = CreateJoint(heldObject, body);
        }

        private void Release()
        {
            if (localJoint)
                Destroy(localJoint);
            if (externalJoint)
                Destroy(externalJoint);
            if (grabbed)
            {
                grabbed.Body.collisionDetectionMode = CollisionDetectionMode.Discrete;
                grabbed.Body.interpolation = RigidbodyInterpolation.None;
                grabbed = null;
            }

            isGrabbing = false;
            heldObject = null;
        }

        private FixedJoint CreateJoint(GameObject origin, Rigidbody connectedBody)
        {
            var joint = origin.AddComponent<FixedJoint>();
            joint.connectedBody = connectedBody;
            joint.breakForce = float.PositiveInfinity;
            joint.breakTorque = float.PositiveInfinity;

            connectedBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            connectedBody.interpolation = RigidbodyInterpolation.Interpolate;
            return joint;
        }
    }
}
