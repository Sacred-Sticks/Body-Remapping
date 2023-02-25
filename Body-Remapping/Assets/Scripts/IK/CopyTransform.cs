using System.Collections;
using UnityEngine;
public class CopyTransform : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private bool position;
    [SerializeField] private Vector3 posOffset;
    [SerializeField] private bool rotation;
    [SerializeField] private Vector3 rotOffset;
    [SerializeField] private bool scale;
    [SerializeField] private Vector3 scaleOffset;

    private void Start()
    {
        if (position)
            StartCoroutine(CopyPosition());
        if (rotation)
            StartCoroutine(CopyRotation());
        if (scale)
            StartCoroutine(CopyScale());
    }

    private IEnumerator CopyPosition()
    {
        while (true) {
            transform.position = target.position + (posOffset.x * transform.right + posOffset.y * transform.up + posOffset.z * transform.forward);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CopyRotation()
    {
        while (true) {
            transform.rotation = target.rotation * Quaternion.Euler(rotOffset);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator CopyScale()
    {
        while (true) {
            transform.localScale = target.localScale + scaleOffset;
            yield return new WaitForEndOfFrame();
        }
    }

}
