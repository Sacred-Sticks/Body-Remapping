using ReferenceVariables;
using UnityEngine;
namespace InputManagement
{
    [CreateAssetMenu(fileName = "Bidirectional Float", menuName = "Variable Groups/Bidirectional Float")]
    public class BidirectionalFloat : ScriptableObject
    {

        [SerializeField] private FloatReference up;
        [SerializeField] private FloatReference down;
        [SerializeField] private FloatReference left;
        [SerializeField] private FloatReference right;

        public FloatReference Up
        {
            get
            {
                return up;
            }
        }
        public FloatReference Down
        {
            get
            {
                return down;
            }
        }
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
