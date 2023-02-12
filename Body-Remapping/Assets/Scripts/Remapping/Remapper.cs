using UnityEngine;

public class Remapper : MonoBehaviour
{
    [SerializeField] private BodyMeasurements userMeasurements;
    [SerializeField] private BodyMeasurements avatarMeasurements;
    [Space]
    [SerializeField] private Transform LeftController;
    [SerializeField] private Transform RightController;
    [Space]
    [SerializeField] private Transform LeftControllerSimulator;
    [SerializeField] private Transform RightControllerSimulator;

    private void Update()
    {
        
    }
}
