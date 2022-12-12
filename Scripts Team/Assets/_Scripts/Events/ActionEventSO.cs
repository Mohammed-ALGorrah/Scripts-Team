using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Events
{
    [CreateAssetMenu(menuName = "Events/Action Event")]

    public class ActionEventSO : ScriptableObject
    {

        public event Action onActionEvent;

        public void RaiseEvent()
        {
            if (onActionEvent != null)
            {
                onActionEvent.Invoke();
            }
            else
            {
                Debug.Log("onActionEvent event was invoked but no one requested it ");
            }
        }
    }

}