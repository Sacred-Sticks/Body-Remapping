using System;
using UnityEngine;
using UnityEngine.Serialization;
namespace ReferenceVariables
{
    [Serializable]
    public class QuaternionReference
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private Quaternion constantValue;
        [SerializeField] private QuaternionVariable variable;

        public Quaternion Value
        {
            get
            {
                return useConstant ? constantValue : variable.Value;
            }
            set
            {
                if (useConstant)
                {
                    constantValue = value;
                    return;
                }
                variable.Value = value;
            }
        }

    }
}
