using System;
using ReferenceVariables;
using UnityEngine;

namespace Debugging
{
    public class TransformGizmo : MonoBehaviour
    {
        [SerializeField] private FloatReference radius;
        [SerializeField] private Colors color;
        private enum Colors
        {
            Red,
            Yellow,
            Green,
            Blue,
            Magenta,
            Cyan,
            Black,
            White,
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = color switch
            {
                Colors.Red => Color.red,
                Colors.Yellow => Color.yellow,
                Colors.Green => Color.green,
                Colors.Blue => Color.blue,
                Colors.Magenta => Color.magenta,
                Colors.Cyan => Color.cyan,
                Colors.Black => Color.black,
                Colors.White => Color.white,
                _ => throw new ArgumentOutOfRangeException()
            };
            Gizmos.DrawSphere(transform.position, radius.Value);
        }
    }
}
