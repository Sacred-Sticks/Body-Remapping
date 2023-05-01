using System;
using UnityEngine;

public class PressDetection : MonoBehaviour
{
    [SerializeField] private string buttonReleasedKey;
    [SerializeField] private string buttonPressedKey;
    [SerializeField] private float activationDistance;
    [SerializeField] private bool canActivateConsecutively;
    [SerializeField] private Direction direction;

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isActivated;

    private const double TOLERANCE = 0.00001;

    private enum Direction
    {
        PositiveX,
        NegativeX,
        PositiveY,
        NegativeY,
        PositiveZ,
        NegativeZ,
    }

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = direction switch
        {

            Direction.PositiveX => initialPosition + transform.right * activationDistance,
            Direction.NegativeX => initialPosition - transform.right * activationDistance,
            Direction.PositiveY => initialPosition + transform.up * activationDistance,
            Direction.NegativeY => initialPosition - transform.up * activationDistance,
            Direction.PositiveZ => initialPosition + transform.forward * activationDistance,
            Direction.NegativeZ => initialPosition - transform.forward * activationDistance,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void Update()
    {
        if (!isActivated)
        {
            if (Vector3.SqrMagnitude(transform.position - targetPosition) < TOLERANCE)
                Activation();
        }
        else
        {
            if (Vector3.SqrMagnitude(transform.position - initialPosition) < TOLERANCE)
                DeActivation();
        }
    }

    private void DeActivation()
    {
        isActivated = false;
        EventManager.Trigger(buttonReleasedKey, new PressDetectedEvent(this));
    }

    private void Activation()
    {
        if (!canActivateConsecutively)
        {
            if (isActivated)
                return;
        }
        EventManager.Trigger(buttonPressedKey, new PressDetectedEvent(this));
        isActivated = true;
    }
}

public class PressDetectedEvent : GameEvent
{
    public PressDetectedEvent(object sender) : base(sender)
    {
    }
}
