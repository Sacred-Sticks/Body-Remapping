using System;
using ReferenceVariables;
using Unity.VisualScripting;
using UnityEngine;

namespace Grabbing
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private FloatVariable grabbingInput;

        private Rigidbody body;
        private Rigidbody grabbableBody;

        private Joint localJoint;
        private Joint grabbingJoint;

        private bool isGrabbing;

        private void Awake()
        {
            grabbingInput.ValueChanged += OnGrabbingInputValueChanged;
        }

        private void OnGrabbingInputValueChanged(object sender, EventArgs e)
        {
            switch (grabbingInput.Value)
            {
                case 0:
                    ReleaseGrabbedObject();
                    break;
                case 1:
                    GrabObject();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(grabbingInput));
            }
        }

        private void ReleaseGrabbedObject()
        {
            if (!isGrabbing)
                return;
            if (!grabbableBody)
                return;
            isGrabbing = false;
            if (localJoint)
                Destroy(localJoint);
            if (grabbingJoint)
                Destroy(grabbingJoint);
        }

        private void GrabObject()
        {
            if (isGrabbing)
                return;
            if (!grabbableBody)
                return;
            isGrabbing = true;
            // Input is pressed, the grabber is within the radius of a grabbable object
            localJoint = body.AddComponent<FixedJoint>();
            grabbingJoint = grabbableBody.AddComponent<FixedJoint>();
            localJoint.connectedBody = grabbableBody;
            grabbingJoint.connectedBody = body;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (isGrabbing)
                return;
            if (!other.gameObject.TryGetComponent<Grabbable>(out var grabbable))
                return;
            var otherBody = grabbable.Body;
            grabbableBody = otherBody;
        }

        private void OnTriggerExit(Collider other)
        {
            if (isGrabbing)
                return;
            if (!other.gameObject.TryGetComponent<Grabbable>(out var grabbable))
                return;
            var otherBody = grabbable.Body;
            if (grabbable.Body == grabbableBody)
                grabbableBody = null;
        }
    }
}
