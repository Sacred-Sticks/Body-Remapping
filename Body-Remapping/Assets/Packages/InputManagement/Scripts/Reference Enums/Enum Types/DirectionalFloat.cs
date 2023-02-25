using ReferenceVariables;
using UnityEngine;
namespace InputManagement
{
    [CreateAssetMenu(fileName = "Directional Float", menuName = "Variable Groups/Directional Float")]
    public class DirectionalFloat : ScriptableObject
    {
        [SerializeField] private FloatReference left;
        [SerializeField] private FloatReference right;

        public FloatReference Left
        {
            get
            {
                return left;
            }
        }
        public FloatReference Right
        {
            get
            {
                return right;
            }
        }
    }
}
