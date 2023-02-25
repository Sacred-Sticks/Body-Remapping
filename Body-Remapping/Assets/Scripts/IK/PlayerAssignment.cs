using UnityEngine;
using InputManagement;
using ReferenceVariables;

public class PlayerAssignment : MonoBehaviour
{
    [SerializeField] private Transform headset, leftController, rightController, character;
    [SerializeField] private Vector3Reference position;
    //[SerializeField] private QuaternionReference rotation;
    
    private void SetPosition()
    {
        transform.position = position.Value.x * character.right + position.Value.y * character.up + position.Value.z * character.forward;
    }

    private void SetRotation()
    {
        //transform.rotation = rotation * character.rotation;
    }

}
