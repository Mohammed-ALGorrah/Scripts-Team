using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Events
{
    [CreateAssetMenu(menuName = "Events/Score Events")]
    public class ScoreEventSO : ScriptableObject
    {
        public event Action<int> OnScoreUpdated;

        public void RaiseEvent(int score)
        {
            if (OnScoreUpdated != null)
            {
                OnScoreUpdated.Invoke(score);
            }
            else
            {
                Debug.Log("Score event was invoked but no one requested it ");
            }
        }
    }

}