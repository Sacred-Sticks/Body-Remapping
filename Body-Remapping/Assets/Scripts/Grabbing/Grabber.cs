using System;
using ReferenceVariables;
using UnityEngine;

namespace Grabbing
{
    [RequireComponent(typeof(Rigidbody))]
    public class Grabber : MonoBehaviour
    {
        [SerializeField] private FloatVariable grabbingInput;
        [SerializeField] private FloatReference radius;
        
        private Rigidbody body;
        private Rigidbody grabbedBody;
        private Joint localJoint;
        private Joint grabbedJoint;

        private bool isGrabbing;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
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

        private void GrabObject()
        {
            if (isGrabbing)
                return;
            SearchForGrabbable();
            if (!grabbedBody)
                return;
            isGrabbing = true;
            // Input is pressed, the grabber is within the radius of a grabbable object
            localJoint = gameObject.AddComponent<FixedJoint>();
            grabbedJoint = grabbedBody.gameObject.AddComponent<FixedJoint>();
            localJoint.connectedBody = grabbedBody;
            grabbedJoint.connectedBody = body;
        }

        private void ReleaseGrabbedObject()
        {
            if (!isGrabbing)
                return;
            if (!grabbedBody)
                return;
            isGrabbing = false;
            if (localJoint)
                Destroy(localJoint);
            if (grabbedJoint)
                Destroy(grabbedJoint);
        }

        private void SearchForGrabbable()
        {
            var results = Physics.OverlapSphere(transform.position, radius.Value);
            foreach (var col in results)
            {
                var result = col.gameObject;
                if (result == gameObject)
                    continue;

                result.TryGetComponent<Grabbable>(out var grabbableComponent);
                
                while (result.transform.parent)
                {
                    if (!grabbableComponent)
                    {
                        result = result.transform.parent.gameObject;
                        result.TryGetComponent(out grabbableComponent);
                        continue;
                    }
                    grabbedBody = grabbableComponent.GetComponent<Rigidbody>();
                    return;
                }

                if (!grabbableComponent)
                    continue;
                grabbedBody = result.GetComponent<Rigidbody>();
                return;
            }
            grabbedBody = null;
        }
    }
}
