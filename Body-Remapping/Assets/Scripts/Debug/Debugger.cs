using UnityEngine;

public class Debugger : MonoBehaviour
{
    [SerializeField] private Transform aTransform;
    [SerializeField] private Transform bTransform;
    [SerializeField] private Transform cTransform;
    [SerializeField] private Transform dTransform;

    private void Update()
    {
        var abDifference = aTransform.position - bTransform.position;
        var cdDifference = cTransform.position - dTransform.position;
        Debug.Log($"Difference at {Vector3.Distance(abDifference, cdDifference)}");
    }
}
