using UnityEngine;
public class Head : MonoBehaviour
{

    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;

    private void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        var rotation = followObject.rotation;
        var forwardAxis = Vector3.forward;
        var forwardDirection = rotation * forwardAxis;
        rootObject.forward = forwardDirection;
        //rootObject.forward = Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized;

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }

}
