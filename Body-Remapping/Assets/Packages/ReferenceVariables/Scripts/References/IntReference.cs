using System;
using UnityEngine;
using UnityEngine.Serialization;
namespace ReferenceVariables
{
    [Serializable]
    public class IntReference
    {
        [SerializeField] private bool useConstant = true;
        [SerializeField] private int constantValue;
        [SerializeField] private IntVariable variable;

        public int Value
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
