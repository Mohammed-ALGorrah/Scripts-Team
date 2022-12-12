using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Heros.Events;

namespace Heros.UI.Managers
{
    public class ScoreUIManager : MonoBehaviour
    {
        public TMP_Text scoreText;
        public TMP_InputField scoreInput;
        public Button scoreButton;

        [Header("Assign Events Here")]
        [SerializeField] private ScoreEventSO scoreUpdatedEvent;


        private void OnEnable()
        {
            scoreUpdatedEvent.OnScoreUpdated += HandleScoreUpdated;
        }

        private void OnDisable()
        {
            scoreUpdatedEvent.OnScoreUpdated -= HandleScoreUpdated;
            scoreButton.onClick.RemoveAllListeners();

        }

        private void Start()
        {
            scoreButton.onClick.AddListener(() =>
            {
                OnClickButton();
            });
        }

        private void HandleScoreUpdated(int val)
        {
            scoreText.text = val.ToString();
        }

        public void OnClickButton()
        {
            scoreUpdatedEvent.RaiseEvent(int.Parse(scoreInput.text));
        }
    }

}