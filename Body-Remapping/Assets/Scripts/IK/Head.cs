using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;

    private void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        //rootObject.forward = followObject.forward.y > 0 ?
        //    Vector3.ProjectOnPlane(-followObject.up, Vector3.up).normalized :
        //    Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized;

        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }
}
