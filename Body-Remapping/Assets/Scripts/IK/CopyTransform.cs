using System;
using System.Collections;
using ReferenceVariables;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CopyTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool copyPosition;
    [ConditionalHide(nameof(copyPosition), true)]
    [SerializeField] private bool3 positionAxis;
    [ConditionalHide(nameof(copyPosition), true)]
    [SerializeField] private Vector3 posOffset;
    [ConditionalHide(nameof(copyPosition), true)]
    [SerializeField] private float positionMultiplier = 1;
    [SerializeField] private bool copyRotation;
    [ConditionalHide(nameof(copyRotation), true)]
    [SerializeField] private bool3 rotationAxis;
    [ConditionalHide(nameof(copyRotation), true)]
    [SerializeField] private Vector3 rotOffset;
    [ConditionalHide(nameof(copyRotation), true)]
    [SerializeField] private float rotationMultiplier = 1;
    [SerializeField] private bool copyScale;
    [ConditionalHide(nameof(copyScale), true)]
    [SerializeField] private bool3 scaleAxis;
    [ConditionalHide(nameof(copyScale), true)]
    [SerializeField] private Vector3 scaleOffset;
    [ConditionalHide(nameof(copyScale), true)]
    [SerializeField] private float scaleMultiplier = 1;

    private void OnEnable()
    {
        if (copyPosition)
            StartCoroutine(CopyPosition());
        if (copyRotation)
            StartCoroutine(CopyRotation());
        if (copyScale)
            StartCoroutine(CopyScale());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator CopyPosition()
    {
        while (true)
        {
            var position = new Vector3()
            {
                x = positionAxis.x ? target.position.x : transform.position.x,
                y = positionAxis.y ? target.position.y : transform.position.y,
                z = positionAxis.z ? target.position.z : transform.position.z,
            };
            position *= positionMultiplier;
            transform.position = position + (posOffset.x * transform.right + posOffset.y * transform.up + posOffset.z * transform.forward);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CopyRotation()
    {
        while (true)
        {
            var rotation = new Vector3()
            {
                x = rotationAxis.x ? target.rotation.eulerAngles.x : transform.rotation.eulerAngles.x,
                y = rotationAxis.y ? target.rotation.eulerAngles.y : transform.rotation.eulerAngles.y,
                z = rotationAxis.z ? target.rotation.eulerAngles.z : transform.rotation.eulerAngles.z,
            };
            rotation *= rotationMultiplier;
            transform.rotation = Quaternion.Euler(rotation) * Quaternion.Euler(rotOffset);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CopyScale()
    {
        while (true)
        {
            var scale = new Vector3()
            {
                x = scaleAxis.x ? target.localScale.x : transform.localScale.x,
                y = scaleAxis.y ? target.localScale.y : transform.localScale.y,
                z = scaleAxis.z ? target.localScale.z : transform.localScale.z,
            };
            scale *= scaleMultiplier;
            transform.localScale = scale + scaleOffset;
            yield return new WaitForEndOfFrame();
        }
    }

}
