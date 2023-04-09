using System;
using UnityEngine;

public class Offset : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 posOffset;
    private Vector3 rotOffset;

    private void Start()
    {
        posOffset = target.position - transform.position;
        rotOffset = target.rotation.eulerAngles - transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        transform.position += posOffset;
        transform.rotation *= Quaternion.Euler(rotOffset);
        throw new NotImplementedException();
    }
}
