using System;
using Interfaces;
using UnityEngine;

namespace Utils
{
    public class BoxCheck : MonoBehaviour, ICheck
    {
        [SerializeField]
        private float width = 0.1f;

        [SerializeField]
        private float height = 0.1f;

        [SerializeField]
        private LayerMask collisionMask;

        public bool Check()
        {
            return Physics2D.OverlapBox(
                transform.position,
                new Vector2(width, height),
                0,
                collisionMask
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1));
        }
    }
}
