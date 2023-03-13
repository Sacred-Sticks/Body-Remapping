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
                    Debug.Log($"Length set to {armLength.Value}");
                    enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void SetPlacement(Transform objectTransform)
        {
            objectTransform.position = transform.position;
            Debug.Log($"Set point at {transform.position}");
        }
    }
}
