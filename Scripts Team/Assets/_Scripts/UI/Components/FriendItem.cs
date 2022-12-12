using Heros.Backend.PlayerData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heros.UI.Components
{
    public class FriendItem : MonoBehaviour
    {
        [SerializeField] private Text friendName;
        [SerializeField] private Button deleteFriendButton;

        private string friendId;

        [SerializeField] FriendsServiceSO _friendsServiceSO;


        private void OnEnable()
        {
            deleteFriendButton.onClick.AddListener(OnClickRemoveFriend);
        }

        private void OnDisable()
        {
            deleteFriendButton.onClick.RemoveListener(OnClickRemoveFriend);
        }

        public void OnClickRemoveFriend()
        {
            _friendsServiceSO.RemoveFriend(friendId);
        }

        public void SetFriendItem(string name, string id)
        {
            friendName.text = name;
            friendId = id;
        }

    }

}