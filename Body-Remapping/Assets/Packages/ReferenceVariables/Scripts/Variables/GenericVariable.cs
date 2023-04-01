using System;
using UnityEngine;

namespace ReferenceVariables
{
    public class GenericVariable<TDataType> : GenericVariable
    {
        [SerializeField] private TDataType value;
        [SerializeField] private bool resetValue;
        [ConditionalHide("resetValue", true)]
        [SerializeField] private TDataType initialValue;

        public delegate void VariableDelegate(object sender, EventArgs e);

        public event VariableDelegate ValueChanged;
        
        public TDataType Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnEnable()
        {
            if (resetValue)
                value = initialValue;
        }

    }

    public class GenericVariable : ScriptableObject
    {

    }
}