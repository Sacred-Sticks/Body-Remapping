using UnityEngine;
public class Head : MonoBehaviour
{

    [SerializeField] private Transform rootObject, followObject;
    [SerializeField] private Vector3 positionOffset, rotationOffset, headBodyOffset;

    private void LateUpdate()
    {
        rootObject.position = transform.position + headBodyOffset;
        //rootObject.forward = Vector3.ProjectOnPlane(followObject.up, Vector3.up).normalized;

        var position = transform.right * positionOffset.x + transform.forward * positionOffset.z;
        transform.position = position;
        
        transform.position = followObject.TransformPoint(positionOffset);
        transform.rotation = followObject.rotation * Quaternion.Euler(rotationOffset);
    }

}
