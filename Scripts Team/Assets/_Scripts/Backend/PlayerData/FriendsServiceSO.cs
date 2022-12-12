using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.Backend.PlayerData
{
    [CreateAssetMenu(menuName = "PlayFab/Services/Friends Service")]
    public class FriendsServiceSO : ScriptableObject
    {
        public event Action<List<FriendInfo>> OnGetFriendListSuccess;
        public event Action<string> OnGetFriendListError;

        public event Action OnAddFriendSuccess;
        public event Action<string> OnAddFriendError;

        public event Action OnRemoveFriendSuccess;
        public event Action<string> OnRemoveFriendError;
        public void GetPlayerFriendList()
        {
            PlayFabClientAPI.GetFriendsList(new GetFriendsListRequest
            {
                ExternalPlatformFriends = ExternalFriendSources.All,

            }, (result) =>
            {
                Debug.Log("Friend list received successfully");
                OnGetFriendListSuccess?.Invoke(result.Friends);
            }, (error) =>
            {
                OnGetFriendListError?.Invoke(error.GenerateErrorReport());

            });
        }


        public void AddFriend(string playerUsername)
        {
            var request = new AddFriendRequest
            {
                FriendUsername = playerUsername,

            };

            PlayFabClientAPI.AddFriend(request, (res) =>
            {
                if (res.Created)
                {
                    OnAddFriendSuccess?.Invoke();
                }

            }, (error) =>
            {
                OnAddFriendError?.Invoke(error.GenerateErrorReport());
            });
        }


        public void RemoveFriend(string playerId)
        {
            var request = new RemoveFriendRequest
            {
                FriendPlayFabId = playerId,

            };

            PlayFabClientAPI.RemoveFriend(request, (result) =>
            {
                OnRemoveFriendSuccess?.Invoke();

            }, (error) =>
            {
                OnRemoveFriendError?.Invoke(error.GenerateErrorReport());
            });

        }
    }

}