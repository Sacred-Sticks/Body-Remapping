using System;
using UnityEngine;
using InputManagement;
using ReferenceVariables;
using UnityEngine.Serialization;

public class PlayerAssignment : MonoBehaviour
{
    [SerializeField] private Transform headset, leftController, rightController, character;
    [SerializeField] private Vector3Reference hmdPosition;
    [SerializeField] private QuaternionReference hmdRotation;
    [SerializeField] private Vector3Reference leftControllerPosition;
    [SerializeField] private QuaternionReference leftControllerRotation;
    [SerializeField] private Vector3Reference rightControllerPosition;
    [SerializeField] private QuaternionReference rightControllerRotation;

    private void Update()
    {
        SetPosition(headset, hmdPosition.Value + transform.position);
        SetPosition(leftController, leftControllerPosition.Value);
        SetPosition(rightController, rightControllerPosition.Value);
        
        SetRotation(headset, hmdRotation.Value);
        SetRotation(leftController, leftControllerRotation.Value);
        SetRotation(rightController, rightControllerRotation.Value);
    }

    private void SetPosition(Transform objectTransform, Vector3 objectPosition)
    {
        var position = objectPosition.x * character.right + objectPosition.y * character.up + objectPosition.z * character.forward;
        objectTransform.position = position;
    }

    private void SetRotation(Transform objectTransform, Quaternion objectRotation)
    {
        objectTransform.rotation = objectRotation * character.rotation;
    }

}
