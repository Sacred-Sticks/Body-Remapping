using UnityEngine;
public class IKFootSolver : MonoBehaviour
{

    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private IKFootSolver otherFoot;
    [SerializeField] private float speed, stepDistance, stepLength, stepHeight;
    [SerializeField] private Vector3 positionOffset, rotationOffset;
    private bool afterFirstMove;

    private float footSpacing, lerp;
    private Vector3 oldNormal, currentNormal, newNormal;
    private Vector3 oldPos, currentPos, newPos;

    private void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPos = newPos = oldPos = transform.position;
        currentNormal = newNormal = oldNormal = transform.up;
        lerp = 1;
        afterFirstMove = false;
    }

    private void Update()
    {
        if (afterFirstMove) {
            transform.position = currentPos + positionOffset;
            transform.rotation = Quaternion.LookRotation(currentNormal) * Quaternion.Euler(rotationOffset);
        }

        CheckMovement();

        if (IsMoving()) {
            var tempPos = Vector3.Lerp(oldPos, newPos, lerp);
            tempPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPos = tempPos;
            currentNormal = Vector3.Lerp(oldNormal, newNormal, lerp);
            lerp += Time.deltaTime * speed;
            afterFirstMove = true;
            return;
        }
        oldPos = newPos;
        oldNormal = newNormal;
    }

    private void CheckMovement()
    {
        Ray ray = new Ray(characterTransform.position + characterTransform.right * footSpacing + Vector3.up * 2, Vector3.down);
        Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayers);

        if (Vector3.SqrMagnitude(newPos - hit.point) > Mathf.Pow(stepDistance, 2) && !otherFoot.IsMoving() && !IsMoving()) {
            lerp = 0;
            int direction = characterTransform.InverseTransformPoint(hit.point).z > characterTransform.InverseTransformPoint(newPos).z ? 1 : -1;
            newPos = hit.point + characterTransform.forward * direction * stepLength;
            newNormal = hit.normal;
        }
    }

    private bool IsMoving()
    {
        return lerp < 1;
    }

}
