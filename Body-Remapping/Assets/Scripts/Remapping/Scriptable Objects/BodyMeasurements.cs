using ReferenceVariables;
using UnityEngine;
[CreateAssetMenu(fileName = "Body Proportions", menuName = "Remapping/Body Proportions")]
public class BodyMeasurements : ScriptableObject
{
    public FloatReference leftArmLength;
    public FloatReference rightArmLength;
}
