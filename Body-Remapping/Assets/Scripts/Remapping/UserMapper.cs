using System;
using ReferenceVariables;
using UnityEngine;
namespace Remapping
{
    public class UserMapper : MonoBehaviour
    {
        [SerializeField] private UserMapper otherMapper;
        [SerializeField] private EventBus loadAvatar;
        [SerializeField] private FloatVariable userInput;
        [SerializeField] private FloatReference armLength;
        [SerializeField] private Transform shoulderTransform;
        [SerializeField] private Transform hipTransform;
        [SerializeField] private Transform hmdTransform;
        
        public enum MappingStatus
        {
            NonePlaced,
            ShoulderPlaced,
            AllJointsPlaced,
        }

        public MappingStatus ActiveStatus { get; private set; } = MappingStatus.NonePlaced;

        private void Awake()
        {
            userInput.ValueChanged += OnActivation;
        }
        private void OnActivation(object sender, EventArgs e)
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
            switch (ActiveStatus)
            {
                case MappingStatus.NonePlaced:
                    SetPlacement(shoulderTransform);
                    ActiveStatus = MappingStatus.ShoulderPlaced;
                    break;
                case MappingStatus.ShoulderPlaced:
                    SetPlacement(hipTransform);
                    ActiveStatus = MappingStatus.AllJointsPlaced;
                    break;
                case MappingStatus.AllJointsPlaced:
                    armLength.Value = Vector3.Distance(transform.position, shoulderTransform.position);
                    if (!otherMapper)
                        loadAvatar.Trigger(this, EventArgs.Empty);
                    Destroy(this);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void SetPlacement(Transform objectTransform)
        {
            var position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            objectTransform.position = position;
        }

        private void OnDestroy()
        {
            userInput.ValueChanged -= OnActivation;
        }
    }
}
