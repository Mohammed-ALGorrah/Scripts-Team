using Heros.Backend.PlayerData;
using Heros.ScritpableObjects.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heros.UI.Managers
{
    public class HomeUIManager : MonoBehaviour
    {

        [SerializeField] TMP_Text coinsText, gemsText;
        [SerializeField] IntegerVariable coins, gems;

        [SerializeField] PlayerCurrencyServiceSO _playerCurrencyServiceSO;


        [SerializeField] Button incButton;
        [SerializeField] Button decButton;

        [SerializeField] Button incGemButton;
        [SerializeField] Button decGemButton;


        [SerializeField] TMP_Text callbackMessage;


        private async Task Start()
        {
            _playerCurrencyServiceSO.GetPlayerCurrency();
            await Task.Delay(2000);

            incButton.onClick.AddListener(OnClickIncrementButton);
            decButton.onClick.AddListener(OnClickDecButton);

            incGemButton.onClick.AddListener(OnClickIncrementGemsButton);
            decGemButton.onClick.AddListener(OnClickDecGemsButton);



        }

        private void OnEnable()
        {
            _playerCurrencyServiceSO.OnAddCurrencyError += OnAddCurrencyError_OnStringEvent;
            _playerCurrencyServiceSO.OnSubtractCurrencyError += OnSubtractCurrencyError_OnStringEvent;
        }


        private void OnDisable()
        {
            _playerCurrencyServiceSO.OnAddCurrencyError -= OnAddCurrencyError_OnStringEvent;
            _playerCurrencyServiceSO.OnSubtractCurrencyError -= OnSubtractCurrencyError_OnStringEvent;
        }

        private void OnSubtractCurrencyError_OnStringEvent(string errorMessage)
        {
            callbackMessage.text = errorMessage;
        }

        private void OnAddCurrencyError_OnStringEvent(string errorMessage)
        {
            callbackMessage.text = errorMessage;

        }

        private void OnClickDecButton()
        {
            Debug.Log("dec coins button clicked");
            _playerCurrencyServiceSO.DecremntCoins(10);
        }

        private void OnClickIncrementButton()
        {
            Debug.Log("inc coins button clicked");
            _playerCurrencyServiceSO.IncrementCoins(10);
        }


        private void OnClickDecGemsButton()
        {
            Debug.Log("dec gems button clicked");
            _playerCurrencyServiceSO.DecremntGems(10);
        }

        private void OnClickIncrementGemsButton()
        {
            Debug.Log("Increment Gems button clicked");
            _playerCurrencyServiceSO.IncrementGems(10);
        }




        private void Update()
        {
            coinsText.text = coins.value.ToString();
            gemsText.text = gems.value.ToString();


        }


    }

}