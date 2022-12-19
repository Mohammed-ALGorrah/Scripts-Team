using Heros.Backend.PlayerData;
using Heros.ScritpableObjects.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Heros.UI.Components
{
    public class Navbar : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] Text gemsText;
        [SerializeField] Text coinsText;
        [SerializeField] Text rankText;
        [SerializeField] Text lvlText;
        [SerializeField] MessageBox callbackMessage;



        [Header("Services")]
        [SerializeField] PlayerCurrencyServiceSO _playerCurrencyService;
        [SerializeField] PlayerDataServiceSO _playerDataServiceSO;

        [Header("Vairables")]
        [SerializeField] IntegerVariable coins, gems;


        private void Start()
        {
                _playerCurrencyService.GetPlayerCurrency();
                      
        }

       IEnumerator WaitForData()
        {
            yield return new WaitForSeconds(5f);
            _playerDataServiceSO.GetAllPlayerData();
        }
        private void OnEnable()
        {
            StartCoroutine(WaitForData());
            _playerCurrencyService.OnAddCurrencyError += _playerCurrencyService_OnAddCurrencyError;
            _playerCurrencyService.OnSubtractCurrencyError += _playerCurrencyService_OnSubtractCurrencyError;
            _playerDataServiceSO.OnGetPlayerDataSuccess += _playerDataServiceSO_OnGetPlayerDataSuccess;
        }

        public GameObject loadPlayfabDataPanel;

        private void _playerDataServiceSO_OnGetPlayerDataSuccess(PlayerDataInfo info)
        {
            if (info.rank != null)
            {
                rankText.text = info.rank;
                lvlText.text = info.level;
                loadPlayfabDataPanel.SetActive(false);
            }
            else
            {
                Debug.Log("Wait fooooooooooor data");
                StartCoroutine(WaitForData());
            }
            
        }

        private void OnDisable()
        {
            _playerCurrencyService.OnAddCurrencyError -= _playerCurrencyService_OnAddCurrencyError;
            _playerCurrencyService.OnSubtractCurrencyError -= _playerCurrencyService_OnSubtractCurrencyError;
            _playerDataServiceSO.OnGetPlayerDataSuccess -= _playerDataServiceSO_OnGetPlayerDataSuccess;


        }


        private void _playerCurrencyService_OnSubtractCurrencyError(string obj)
        {

            callbackMessage.SetMessageText(obj);
            callbackMessage.gameObject.SetActive(true);
        }

        private void _playerCurrencyService_OnAddCurrencyError(string obj)
        {
            callbackMessage.SetMessageText(obj);
            callbackMessage.gameObject.SetActive(true);


        }

        private void Update()
        {

            coinsText.text = coins.value.ToString();
            gemsText.text = gems.value.ToString();


        }
    }

}