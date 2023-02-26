using System;
using InputManagement;
using ReferenceVariables;
using UnityEngine;
namespace Remapping
{
    public class UserMapper : MonoBehaviour
    {
        [SerializeField] private FloatReference userInput;
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
        private bool inputting = false;
        
        private void Update()
        {
            if (userInput.Value == 0)
            {
                inputting = false;
                return;
            }
            if (inputting) return;
            inputting = true;
            StatusStateMachine();
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
                    enabled = false;
                    break;
                default:
                    break;
            }
        }
        private void SetPlacement(Transform objectTransform)
        {
            objectTransform.position = transform.position;
        }
    }
}
