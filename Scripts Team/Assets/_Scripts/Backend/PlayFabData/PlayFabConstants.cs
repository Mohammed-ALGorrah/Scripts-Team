using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Heros.Backend.PlayfabData
{
    [CreateAssetMenu(menuName = "PlayFab/Config")]

    public class PlayFabConstants : ScriptableObject
    {
        public string titleId;
        public string playerId;
        public string savedUsername;
        public string sessionTicket;

        private void Awake()
        {
            Debug.Log("Awake method for playfab constants");
        }

        private void OnEnable()
        {
            if (!File.Exists(Application.dataPath + "/Playfab-constants.json"))
            {
                Debug.Log("Json file does not exist");
            }
            else
            {
                string json = File.ReadAllText(Application.dataPath + "/Playfab-constants.json");
                PlayFabConstantsStruct dataFromJson = JsonUtility.FromJson<PlayFabConstantsStruct>(json);
                playerId = dataFromJson.playerId;
                savedUsername = dataFromJson.savedUsername;
                sessionTicket = dataFromJson.sessionTicket;

            }


        }
    }


    [Serializable]
    public struct PlayFabConstantsStruct
    {
        public string playerId;
        public string savedUsername;
        public string sessionTicket;
    }


}