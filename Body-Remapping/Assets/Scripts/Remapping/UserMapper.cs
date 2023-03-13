using System;
using ReferenceVariables;
using UnityEngine;
namespace Remapping
{
    public class UserMapper : MonoBehaviour
    {
        [SerializeField] private FloatVariable userInput;
        [SerializeField] private FloatReference armLength;
        [SerializeField] private Transform shoulderTransform;
        [SerializeField] private Transform hipTransform;

        private Vector3 handPosition;
        
        private enum MappingStatus
        {
            NonePlaced,
            ShoulderPlaced,
            AllJointsPlaced,
        }

        private MappingStatus activeStatus = MappingStatus.NonePlaced;

        private void Awake()
        {
            userInput.ValueChanged += OnUserInputValueChanged;
        }
        private void OnUserInputValueChanged(object sender, EventArgs e)
        {
            switch (userInput.Value)
            {
                case 0:
                    break;
                case 1:
                    StatusStateMachine();
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(userInput));
            }
        }

        private void StatusStateMachine()
        {
            switch (activeStatus)
            {
                case MappingStatus.NonePlaced:
                    SetPlacement(shoulderTransform);
                    activeStatus = MappingStatus.ShoulderPlaced;
                    break;
                case MappingStatus.ShoulderPlaced:
                    SetPlacement(hipTransform);
                    activeStatus = MappingStatus.AllJointsPlaced;
                    break;
                case MappingStatus.AllJointsPlaced:
                    armLength.Value = Vector3.Distance(transform.position, shoulderTransform.position);
                    handPosition = transform.position;
                    enabled = false;
                    break;
            }
        }
        private void SetPlacement(Transform objectTransform)
        {
            objectTransform.position = transform.position;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(shoulderTransform.position, 0.01f);
            Gizmos.DrawSphere(hipTransform.position, 0.01f);
            Gizmos.DrawSphere(handPosition, 0.01f);
        }
    }
}
