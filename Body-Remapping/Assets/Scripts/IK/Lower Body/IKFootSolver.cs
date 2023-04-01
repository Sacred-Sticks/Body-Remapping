using UnityEngine;

public class IKFootSolver : MonoBehaviour
{

    [SerializeField] private IKFootSolver otherFoot;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 normalOffset;
    [SerializeField] private float steppingSpeed;
    [SerializeField] private float steppingDistance;
    [SerializeField] private float stepLength;
    [SerializeField] private float steppingHeight;
    [SerializeField] private float stepDetectionHeight;
    
    public bool IsMoving
    {
        get
        {
            return lerpPercentage < 1;
        }
    }

    private Vector3 previousPosition, currentPosition, nextPosition;
    private Vector3 previousNormal, currentNormal, nextNormal;
    private float footSpacing;
    private float lerpPercentage;

    private void Start()
    {
        footSpacing = transform.localPosition.x;
        previousPosition = currentPosition = nextPosition = transform.position;
        previousNormal = currentNormal = nextNormal = transform.up;
        lerpPercentage = 1;
    }

    private void Update()
    {
        transform.position = currentPosition + positionOffset;
        transform.rotation = Quaternion.Euler(currentNormal) * Quaternion.Euler(normalOffset);

        var ray = new Ray(target.position + (transform.right * footSpacing) + (Vector3.up * 2), Vector3.down);
        if (Physics.Raycast(ray, out var hit, stepDetectionHeight, groundLayers.value))
        {
            if (Vector3.SqrMagnitude(nextPosition - hit.point) > Mathf.Pow(steppingDistance, 2) && !otherFoot.IsMoving && !IsMoving)
            {
                lerpPercentage = 0;
                int direction = (target.InverseTransformPoint(hit.point) - target.InverseTransformPoint(nextPosition)).z > 0 ? 1 : -1;
                nextPosition = hit.point + (transform.forward * (direction * stepLength));
                nextNormal = hit.normal;
            }
        }

        if (IsMoving)
        {
            var tempPosition = Vector3.Lerp(previousPosition, nextPosition, lerpPercentage);
            tempPosition.y = Mathf.Sin(lerpPercentage * Mathf.PI) * steppingHeight;
            var tempNormal = Vector3.Lerp(previousNormal, nextNormal, lerpPercentage);

            currentPosition = tempPosition;
            currentNormal = tempNormal;
            lerpPercentage += Time.deltaTime * steppingSpeed;
        }
        else
        {
            previousPosition = nextPosition;
            previousNormal = nextNormal;
        }

    }
}
