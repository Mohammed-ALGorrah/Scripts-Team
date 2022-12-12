using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Backend.PlayFabEvents
{
    [CreateAssetMenu(menuName = "Events/PlayFab Events/Auth Events", order = 1)]
    public class PlayFabAuthEvent : ScriptableObject
    {
        public event Action<string, string> OnAuthSuccess;

        public void RaiseEvent(string playerId, string playerUsername)
        {
            OnAuthSuccess?.Invoke(playerId, playerUsername);
        }
    }

}