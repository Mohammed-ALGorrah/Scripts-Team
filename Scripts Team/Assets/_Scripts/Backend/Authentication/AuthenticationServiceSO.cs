using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.IO;
using Heros.Backend.PlayfabData;
using Photon.Pun;

namespace Heros.Backend.Authentication
{
[CreateAssetMenu(menuName = "PlayFab/Services/Authentication Service")]
    public class AuthenticationServiceSO : ScriptableObject
    {
        [SerializeField] private PlayFabConstants _playFabConstants;

        //#region SO Events
        //public PlayFabAuthEvent OnRegisterSuccessEvent;
        //public StringEventSO OnRegisterFailedEvent;
        //public ActionEventSO OnLogoutEvent;

        //public PlayFabAuthEvent OnLoginSuccessEvent;
        //public StringEventSO OnLoginFailedEvent;

        //#endregion

        #region Events 
        public event Action<string, string> OnRegisterSuccessEvent;
        public event Action<string> OnRegisterFailedEvent;
        public event Action OnLogoutEvent;
        public event Action<string, string> OnLoginSuccessEvent;
        public event Action<string> OnLoginFailedEvent;

        #endregion
        public void RegisterWithEmail(RegisterParams registerParams)
        {
            if (!ValidateRegisterInput(registerParams))
            {
                OnRegisterFailedEvent?.Invoke("Fill the inputs correctly");
                return;
            }

            var request = new RegisterPlayFabUserRequest
            {
                Username = registerParams.Username,
                Email = registerParams.Email,
                Password = registerParams.Password,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = true,
                    GetUserVirtualCurrency = true,
                },
                TitleId = _playFabConstants.titleId

            };

            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailed);
        


        }

        private void OnRegisterFailed(PlayFabError error)
        {
            Debug.Log($"{error.Error} - {error.ErrorMessage }");
            OnRegisterFailedEvent?.Invoke(error.ErrorMessage);
        }

        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            Debug.Log($"Registerd success for {result.PlayFabId} -{result.Username}");
            OnRegisterSuccessEvent?.Invoke(result.PlayFabId, result.Username);

            var dataToJson = new PlayFabConstantsStruct
            {
                playerId = result.PlayFabId,
                savedUsername = result.Username,
                sessionTicket = Guid.NewGuid().ToString()
            };

            StoreSessionData(dataToJson);
            LinkCustomIDForUser(dataToJson);

            //PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), (res) =>
            //{
            //    Debug.Log(res.Data["new-players-gifts"]);

            //}, (error) =>
            //{
            //    Debug.Log(error.GenerateErrorReport());

            //});
           // PhotonNetwork.ConnectUsingSettings();
           // PhotonNetwork.LocalPlayer.NickName = result.Username;
           // PhotonNetwork.AutomaticallySyncScene = true;

        }

        private void LinkCustomIDForUser(PlayFabConstantsStruct dataToJson)
        {
            // Fire and forget, but link a custom ID to this PlayFab Account.
            PlayFabClientAPI.LinkCustomID(
                new LinkCustomIDRequest
                {
                    CustomId = dataToJson.sessionTicket,
                    ForceLink = false
                },
                (res) => { Debug.Log("succsess"); },   // Success callback
                (error) =>
                {

                    Debug.Log($"{error.Error} - {error.ErrorMessage } - custom link");

                }    // Failure callback
                );
        }


        private bool ValidateRegisterInput(RegisterParams registerParams)
        {
            if (string.IsNullOrEmpty(registerParams.Email) | string.IsNullOrEmpty(registerParams.Username) | string.IsNullOrEmpty(registerParams.Password))
            {
                return false;
            }

            return true;
        }


        public void Logout()
        {
            PlayFabClientAPI.ForgetAllCredentials();


            File.Delete(Application.dataPath + "/Playfab-constants.json");
            _playFabConstants.savedUsername = "";
            _playFabConstants.sessionTicket = "";
            _playFabConstants.playerId = "";
            OnLogoutEvent?.Invoke();
        }

        public void CheckIfLoggedIn()
        {
            if (string.IsNullOrEmpty(_playFabConstants.sessionTicket) | string.IsNullOrEmpty(_playFabConstants.playerId))
            {
                return;
            }


            PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest
            {
                CustomId = _playFabConstants.sessionTicket,
                TitleId = _playFabConstants.titleId,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = true,
                    GetUserVirtualCurrency = true,
                },

            }, (result) =>
            {
                Debug.Log($"{result.PlayFabId} has logged in using custom id");

                OnLoginSuccessEvent?.Invoke(result.PlayFabId, result.InfoResultPayload.AccountInfo.Username);

                //PhotonNetwork.ConnectUsingSettings();
                //PhotonNetwork.LocalPlayer.NickName = result.InfoResultPayload.AccountInfo.Username;
                //PhotonNetwork.AutomaticallySyncScene = true;

            }, (err) =>
            {
                OnLoginFailedEvent?.Invoke(err.ErrorMessage);

            });




        }


        #region Login methods

        public void LoginWithEmail(LoginParams loginParams)
        {
            if (!ValidateLoginInput(loginParams))
            {
                OnLoginFailedEvent?.Invoke("Fill inputs correctly in login scene");
                return;
            }

            PlayFabConstantsStruct dataToJson = new PlayFabConstantsStruct();


            var request = new LoginWithEmailAddressRequest
            {
                Email = loginParams.Email,
                Password = loginParams.Password,
                TitleId = _playFabConstants.titleId,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
                {
                    GetUserAccountInfo = true,
                    GetUserVirtualCurrency = true,
                },
            };



            PlayFabClientAPI.LoginWithEmailAddress(request, (result) =>
            {
                //_playFabConstants.playerId = result.PlayFabId;
                //_playFabConstants.savedUsername = result.InfoResultPayload.AccountInfo.Username;
                //_playFabConstants.sessionTicket = Guid.NewGuid().ToString();

                dataToJson.playerId = result.PlayFabId;
                dataToJson.savedUsername = result.InfoResultPayload.AccountInfo.Username;
                dataToJson.sessionTicket = Guid.NewGuid().ToString();

                StoreSessionData(dataToJson);



                // Fire and forget, but link a custom ID to this PlayFab Account.
                PlayFabClientAPI.LinkCustomID(
                    new LinkCustomIDRequest
                    {
                        CustomId = dataToJson.sessionTicket,
                        ForceLink = false
                    },
                    (res) =>
                    {
                        OnLoginSuccessEvent?.Invoke(dataToJson.playerId, dataToJson.savedUsername);

                    }, null
                    );

            }, (err) =>
            {
                OnLoginFailedEvent?.Invoke(err.ErrorMessage);
            });






        }


        private bool ValidateLoginInput(LoginParams loginParams)
        {
            if (string.IsNullOrEmpty(loginParams.Email) | string.IsNullOrEmpty(loginParams.Password))
            {
                return false;
            }

            return true;
        }
        #endregion



        private void StoreSessionData(PlayFabConstantsStruct dataToJson)
        {
            _playFabConstants.playerId = dataToJson.playerId;
            _playFabConstants.savedUsername = dataToJson.savedUsername;
            _playFabConstants.sessionTicket =dataToJson.sessionTicket;


            string json = JsonUtility.ToJson(dataToJson, true);

            File.WriteAllText(Application.dataPath + "/Playfab-constants.json", json);
        }

    }

}