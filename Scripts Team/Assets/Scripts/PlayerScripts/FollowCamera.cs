using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlayerScripts
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
         public Transform target;

        private void LateUpdate()
        {
            transform.position = target.position;
        }

    }
}