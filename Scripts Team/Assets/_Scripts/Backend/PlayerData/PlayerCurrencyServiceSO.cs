using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using Heros.Backend.PlayfabData;
using Heros.ScritpableObjects.Variables;

namespace Heros.Backend.PlayerData
{
    [CreateAssetMenu(menuName = "PlayFab/Services/Player Currency Service")]
    public class PlayerCurrencyServiceSO : ScriptableObject
    {
        [SerializeField] PlayFabConstants _playFabConstants;
        [SerializeField] private IntegerVariable coins, gems;

        [SerializeField] string coninsKey = "CN";
        [SerializeField] string gemsKey = "GM";


        public event Action<string> OnAddCurrencyError;
        public event Action<string> OnSubtractCurrencyError;



        public void GetPlayerCurrency()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), (res) =>
            {
                coins.value = res.VirtualCurrency["CN"];
                gems.value = res.VirtualCurrency["GM"];
                Debug.Log("User Currency are updated");

            }, (err) =>
            {
                Debug.Log(err.GenerateErrorReport());
            });
        }


        public void IncrementCoins(int amount)
        {
            if (amount < 0)
            {
                OnAddCurrencyError?.Invoke("Amount is not a valid number");
                return;
            }

            var request = new AddUserVirtualCurrencyRequest
            {
                Amount = amount,
                VirtualCurrency = coninsKey
            };


            PlayFabClientAPI.AddUserVirtualCurrency(request, (res) => {
                coins.value = res.Balance;
                Debug.Log("Coins are updated successfully");

            }, (err) => {

                Debug.Log(err.GenerateErrorReport());
            });


        }


        public void DecremntCoins(int amount)
        {
            if (amount < 0 | coins.value - amount < 0)
            {
                OnSubtractCurrencyError?.Invoke("Amount is not a valid number");
                return;
            }

            var request = new SubtractUserVirtualCurrencyRequest
            {
                Amount = amount,
                VirtualCurrency = coninsKey
            };


            PlayFabClientAPI.SubtractUserVirtualCurrency(request, (res) => {
                coins.value = res.Balance;
                Debug.Log("Coins are updated successfully");

            }, (err) => {

                Debug.Log(err.GenerateErrorReport());
            });


        }


        public void IncrementGems(int amount)
        {
            if (amount < 0)
            {
                OnAddCurrencyError?.Invoke("Amount is not a valid number");
                return;
            }

            var request = new AddUserVirtualCurrencyRequest
            {
                Amount = amount,
                VirtualCurrency = gemsKey
            };


            PlayFabClientAPI.AddUserVirtualCurrency(request, (res) => {
                gems.value = res.Balance;
                Debug.Log("gems are updated successfully");

            }, (err) => {

                Debug.Log(err.GenerateErrorReport());
            });
        }


        public void DecremntGems(int amount)
        {
            if (amount < 0 || gems.value - amount < 0)
            {
                OnSubtractCurrencyError?.Invoke("Amount is not a valid number");
                return;
            }

            var request = new SubtractUserVirtualCurrencyRequest
            {
                Amount = amount,
                VirtualCurrency = gemsKey
            };


            PlayFabClientAPI.SubtractUserVirtualCurrency(request, (res) => {
                gems.value = res.Balance;
                Debug.Log("Gems are updated successfully");

            }, (err) => {

                Debug.Log(err.GenerateErrorReport());
            });


        }

    }

}