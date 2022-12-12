using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Backend.PlayerData
{
    [CreateAssetMenu(menuName = "PlayFab/Services/Player Data Service")]

    public class PlayerDataServiceSO : ScriptableObject
    {
        public event Action<PlayerDataInfo> OnGetPlayerDataSuccess; 
        public event Action<string> OnGetPlayerDataError;
        
        public event Action OnPlayerDataUpdatedSuccess; 
        public event Action<string> OnPlayerDataUpdatedError; 


        public void GetAllPlayerData()
        {
            Debug.Log("@@@@@@@@@@@@@@@@@@@@@");
            PlayFabClientAPI.GetUserData(new GetUserDataRequest(), (res) =>
            {
                PlayerDataInfo info = new PlayerDataInfo();
                info.selectedCharacter = res.Data["selected_character"].Value;
                info.rank = res.Data["rank"].Value;
                Debug.Log(res.ToJson());
                OnGetPlayerDataSuccess?.Invoke(info);

            }, (error) =>
            {
                Debug.Log(error.GenerateErrorReport());
                OnGetPlayerDataError?.Invoke(error.ErrorMessage);
            });
        }

        public void UpdatePlayerData(string key , string value)
        {
            var request = new UpdateUserDataRequest {
                Data = new Dictionary<string, string> { {key,value } }
                
            };

            PlayFabClientAPI.UpdateUserData(request, (res) =>
            {
                Debug.Log("success updated");
                Debug.Log(res.ToJson());
                OnPlayerDataUpdatedSuccess?.Invoke();

            }, (err) =>
            {
                Debug.Log(err.GenerateErrorReport());
                OnPlayerDataUpdatedError?.Invoke(err.ErrorMessage);

            });

        }
    }



    public struct PlayerDataInfo
    {
        public string rank;
        public string selectedCharacter;

    }
}
