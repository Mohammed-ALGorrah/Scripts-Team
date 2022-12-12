using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Events
{
    [CreateAssetMenu(menuName = "Events/String event")]
    public class StringEventSO : ScriptableObject
    {
        public event Action<string> OnStringEvent;

        public void RaiseEvent(string val)
        {
            if (OnStringEvent != null)
            {
                OnStringEvent.Invoke(val);
            }
            else
            {
                Debug.Log("StringEventSO event was invoked but no one requested it ");
            }
        }
    }

}