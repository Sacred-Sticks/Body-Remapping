using System;
using UnityEngine;

[Serializable]
public class VRMap
{
    [SerializeField] private Transform targetVR;
    [SerializeField] private Transform targetRig;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    
    public void Map()
    {
        targetRig.position = targetVR.TransformPoint(positionOffset);
        targetRig.rotation = targetVR.rotation * Quaternion.Euler(rotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    [SerializeField] private Axis axis;
    [Space]
    [SerializeField] private VRMap headMap;
    [SerializeField] private VRMap leftControllerMap;
    [SerializeField] private VRMap rightControllerMap;
    [Space]
    [SerializeField] private Transform headConstraint;
    [SerializeField] private Vector3 customOffset;
    
    private Vector3 headBodyOffset;
    private Vector3 rotationOffset;

    private enum Axis
    {
        forward,
        backward,
        up,
        down,
        right,
        left,
    }

    private void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
        rotationOffset = headConstraint.rotation.eulerAngles - transform.rotation.eulerAngles;
    }

    private void Update()
    {
        headMap.Map();
        leftControllerMap.Map();
        rightControllerMap.Map();
        
        transform.position = headConstraint.position + headBodyOffset + customOffset;
        transform.forward = axis switch
        {
            Axis.forward => Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up),
            Axis.backward => Vector3.ProjectOnPlane(-headConstraint.forward, Vector3.up),
            Axis.up => Vector3.ProjectOnPlane(headConstraint.up, Vector3.up),
            Axis.down => Vector3.ProjectOnPlane(-headConstraint.up, Vector3.up),
            Axis.right => Vector3.ProjectOnPlane(headConstraint.right, Vector3.up),
            Axis.left => Vector3.ProjectOnPlane(-headConstraint.right, Vector3.up),
            _ => throw new ArgumentOutOfRangeException()
        };
        transform.rotation *= Quaternion.Euler(rotationOffset);
    }
}
