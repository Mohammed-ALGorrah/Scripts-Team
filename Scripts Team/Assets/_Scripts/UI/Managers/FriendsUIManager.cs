using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine.UI;
using System;
using Heros.UI.Components;
using Heros.Backend.PlayerData;

namespace Heros.UI.Managers
{
    public class FriendsUIManager : MonoBehaviour
    {

        [SerializeField] private FriendsServiceSO _friendsService;
        [Header("Friend Item Prefab")]
        [SerializeField] FriendItem FriendItemPrefab;
        private List<FriendInfo> friendList;

        [SerializeField] Transform friendListTransform;

        #region Ui Variables
        [Header("UI Variables")]
        [SerializeField] MessageBox callbackMessage;

        [SerializeField] TMP_InputField addFriendInput;
        [SerializeField] Button addFriendButton;


        #endregion

        [SerializeField]
        ChatManager chatManager;



        private void Start()
        {
            _friendsService.GetPlayerFriendList();

        }

        private void OnEnable()
        {
            _friendsService.OnGetFriendListSuccess += _friendsService_OnGetFriendListSuccess;
            _friendsService.OnAddFriendSuccess += _friendsService_OnAddFriendSuccess;
            _friendsService.OnAddFriendError += _friendsService_OnAddFriendError;
            _friendsService.OnGetFriendListError += _friendsService_OnGetFriendListError;
            _friendsService.OnRemoveFriendSuccess += _friendsService_OnRemoveFriendSuccess;
            _friendsService.OnRemoveFriendError += _friendsService_OnRemoveFriendError;

            addFriendButton.onClick.AddListener(OnClickAddFriendButton);
        }

        public void ClearFriendsList()
        {
            foreach (Transform transform in friendListTransform)
            {
                Destroy(transform.gameObject);
            }
        }



        private void OnClickAddFriendButton()
        {
            if (string.IsNullOrEmpty(addFriendInput.text))
            {
                callbackMessage.SetMessageText("Please Enter a username");
                callbackMessage.gameObject.SetActive(true);

                return;
            }
            _friendsService.AddFriend(addFriendInput.text);
        }



        private void OnDisable()
        {
            _friendsService.OnGetFriendListSuccess -= _friendsService_OnGetFriendListSuccess;
            _friendsService.OnAddFriendSuccess -= _friendsService_OnAddFriendSuccess;
            _friendsService.OnAddFriendError -= _friendsService_OnAddFriendError;
            _friendsService.OnGetFriendListError -= _friendsService_OnGetFriendListError;

            _friendsService.OnRemoveFriendSuccess -= _friendsService_OnRemoveFriendSuccess;
            _friendsService.OnRemoveFriendError -= _friendsService_OnRemoveFriendError;

            addFriendButton.onClick.RemoveListener(OnClickAddFriendButton);



        }
        private void _friendsService_OnRemoveFriendError(string obj)
        {
            callbackMessage.SetMessageText(obj);
            callbackMessage.gameObject.SetActive(true);

        }

        private void _friendsService_OnRemoveFriendSuccess()
        {
            Debug.Log("FriendUI MANAGER Friend removed");
            ClearFriendsList();
            _friendsService.GetPlayerFriendList();
        }



        private void _friendsService_OnGetFriendListError(string obj)
        {
            callbackMessage.SetMessageText(obj);
            callbackMessage.gameObject.SetActive(true);

        }

        private void _friendsService_OnAddFriendError(string obj)
        {
            callbackMessage.SetMessageText(obj);
            callbackMessage.gameObject.SetActive(true);
        }

        private void _friendsService_OnAddFriendSuccess()
        {

            ClearFriendsList();
            _friendsService.GetPlayerFriendList();
        }



        private void _friendsService_OnGetFriendListSuccess(List<FriendInfo> friends)
        {
            friendList = friends;

            foreach (FriendInfo item in friendList)
            {
                var friendItem = Instantiate(FriendItemPrefab, friendListTransform);
                friendItem.SetFriendItem(item.Username, item.FriendPlayFabId);
                friendItem.transform.GetChild(3).GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(()=> {
                    chatManager.chatPanel.SetActive(true);
                    chatManager.ChatConnectOnClick();
                    chatManager.receiverField.text = item.Username;
                });
            }



        }






    }

}