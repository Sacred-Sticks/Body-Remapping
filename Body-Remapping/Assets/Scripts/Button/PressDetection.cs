using System;
using UnityEngine;

public class PressDetection : MonoBehaviour
{
    [SerializeField] private EventBus buttonPressed;
    [SerializeField] private EventBus buttonReleased;
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
        if (!buttonReleased)
            return;
        var releasedArgs = new PressDetectedEventArgs();
        buttonReleased.Trigger(this, releasedArgs);
    }

    private void Activation()
    {
        if (!canActivateConsecutively)
        {
            if (isActivated)
                return;
        }
        if (!buttonPressed)
            return;
        var pressedArgs = new PressDetectedEventArgs();
        buttonPressed.Trigger(this, pressedArgs);
        isActivated = true;
    }
}

public class PressDetectedEventArgs : EventArgs
{
    
}
