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
    [SerializeField] private VRMap headMap;
    [SerializeField] private VRMap leftControllerMap;
    [SerializeField] private VRMap rightControllerMap;
    //[SerializeField] private VRMap rootMap;
    
    [SerializeField] private Transform headConstraint;
    [SerializeField] private Vector3 customOffset;
    
    private Vector3 headBodyOffset;

    private void Start()
    {
        headBodyOffset = transform.position - headConstraint.position;
    }

    private void Update()
    {
        headMap.Map();
        leftControllerMap.Map();
        rightControllerMap.Map();
        //rootMap.Map();
        
        transform.position = headConstraint.position + headBodyOffset + customOffset;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up);
    }
}
