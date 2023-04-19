using System;
using UnityEngine;

public class PressDetection : MonoBehaviour
{
    [SerializeField] private string buttonReleasedKey;
    [SerializeField] private string buttonPressedKey;
    [SerializeField] private float activationDistance;
    [SerializeField] private bool canActivateConsecutively;

    private Vector3 initialPosition;
    private bool isActivated;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.SqrMagnitude(transform.position - initialPosition) < Mathf.Pow(activationDistance, 2))
        {
            DeActivation();
            return;
        }
        Activation();
    }

    private void DeActivation()
    {
        if (!isActivated)
            return;
        isActivated = false;
        EventManager.Trigger(buttonReleasedKey, new PressDetectedEventArgs(this));
    }

    private void Activation()
    {
        if (!canActivateConsecutively)
        {
            if (isActivated)
                return;
        }
        EventManager.Trigger(buttonPressedKey, new PressDetectedEventArgs(this));
        isActivated = true;
    }
}

public class PressDetectedEventArgs : GameEvent
{
    public PressDetectedEventArgs(object sender) : base(sender)
    {
    }
}
