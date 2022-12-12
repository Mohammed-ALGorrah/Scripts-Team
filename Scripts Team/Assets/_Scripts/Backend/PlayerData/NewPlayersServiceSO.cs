using Heros.Backend.PlayfabData;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Backend.PlayerData
{
    [CreateAssetMenu(menuName = "PlayFab/Services/NewPlayersService")]
    public class NewPlayersServiceSO : ScriptableObject
    {

        [SerializeField] private PlayFabConstants playFabConstants;


        public void GetNewPlayerGifts()
        {

        }


    }
}
