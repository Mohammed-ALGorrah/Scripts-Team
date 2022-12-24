using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Heros.Backend.PlayfabData;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region Variables
    #region Input Fields
    [Header("Input Fields")]
    public InputField userNameText;
    public TMP_InputField roomNameText;
    public InputField maxPlayerNumberText;
    #endregion
    #region Ui Panels
    [Header("Ui Panels")]
    public GameObject PlayerNamePanel;
    public GameObject LobbyPanel;
    public GameObject RoomCreatePanel;
    public GameObject ConnectingPanel;
    public GameObject RoomListPanel;
    public GameObject InsideRoomPanel;
    #endregion
    #region Ui Buttons
    [Header("Ui Buttons")]
    public GameObject PlayButton;
    public GameObject springButton;
    public GameObject snowButton;
    #endregion
    #region Room List Variables
    [Header("Room List Variables")]
    public GameObject roomListPrefab;
    public GameObject roomListParent;
    private Dictionary<string, RoomInfo> roomListData;
    private Dictionary<string, GameObject> roomListGameobject;
    #endregion
    #region Players List Variables
    [Header("Players List Variables")]
    public GameObject playerListItemPrefab1;
    public GameObject playerListItemPrefab2;
    public GameObject playerListItemPrefab3;
    public GameObject PlayerListItemParentTeam1;
    public GameObject PlayerListItemParentTeam2;
    private Dictionary<int, GameObject> playersList;
    private int avatar = 0;
    public TMP_Text roomName;
    #endregion
    private int redCount = 0;
    private int BlueCount = 3;

    public PlayFabConstants userPlayfab;
    #endregion

    #region UnityMethods

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("lvvvvvvvvvvvvvvl = " + level);
        if (level == 0)
        {
            PhotonNetwork.Disconnect();
            Destroy(this.gameObject);
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("exiiiiiiiiiiiiiiiiiiiiiiiiiiiiit");
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("ConnectUsingSettings");
        PhotonNetwork.LocalPlayer.NickName = userPlayfab.savedUsername;
        PhotonNetwork.AutomaticallySyncScene = true;
        roomListData = new Dictionary<string, RoomInfo>();
        roomListGameobject = new Dictionary<string, GameObject>();
        playersList = new Dictionary<int, GameObject>();
    }
    #endregion

    #region UiMethods
    public float clickTimer = 1f; // Time in seconds between button clicks
    public float lastClickTime = 0f;
    public void OnLoginClick()
    {
       // if (Time.time - lastClickTime > clickTimer)
      //  {
          //  lastClickTime = Time.time;
            ActivateMyPanel(LobbyPanel.name);
       // }
    }

    public void OnRoomCreateClick()
    {
        if (Time.time - lastClickTime > clickTimer)
        {
            lastClickTime = Time.time;

            string roomName = roomNameText.text;
            if (string.IsNullOrEmpty(roomName))
            {
                roomName += Random.Range(0, 1000);
            }
            CreateRoom(roomName, 6);
        }
    }

    public void OnCancelClick()
    {
       // if (Time.time - lastClickTime > clickTimer)
        //{
        //    lastClickTime = Time.time;
            ActivateMyPanel(LobbyPanel.name);
       // }
    }

    public void OnClosePanel()
    {
       // if (Time.time - lastClickTime > clickTimer)
       // {
          //  lastClickTime = Time.time;
            LobbyPanel.SetActive(false);
      //  }
    }

    public void RoomListBtnClicked()
    {
        if (Time.time - lastClickTime > clickTimer)
        {
            lastClickTime = Time.time;
            if (!PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinLobby();
            }
            ActivateMyPanel(RoomListPanel.name);
        }
    }

    public void BackFromRoomList()
    {
       // if (Time.time - lastClickTime > clickTimer)
       // {
            //lastClickTime = Time.time;
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
           // }
            ActivateMyPanel(LobbyPanel.name);
        }
    }

    public void BackFromPlayersList()
    {
      //  if (Time.time - lastClickTime > clickTimer)
       // {
         //   lastClickTime = Time.time;
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.LeaveRoom();
            }
            ActivateMyPanel(LobbyPanel.name);
      //  }
    }

    public void OnClickPlayButton()
    {
        if (Time.time - lastClickTime > clickTimer)
        {
            lastClickTime = Time.time;
           // if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            //{
                PhotonNetwork.LoadLevel(3);
                PhotonNetwork.CurrentRoom.IsOpen = false;
            //}
        }
    }


    public void OnRandomBtnClick()
    {
        if (Time.time - lastClickTime > clickTimer)
        {
            lastClickTime = Time.time;
            PhotonNetwork.JoinRandomRoom();
        }
    }

    #endregion

    #region PHOTON_CALLBACKS

    public override void OnConnected()
    {
        Debug.Log("Connected to server");
    }

    public GameObject loadPanel;
    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectUsingSettings222222222222222");
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to photon...");
        loadPanel.SetActive(false);
    }

    public override void OnCreatedRoom()
    {
        /* int teamNum = 0;
         if (PhotonNetwork.CurrentRoom.PlayerCount <= (PhotonNetwork.CurrentRoom.MaxPlayers / 2))
         {
             teamNum = 1;
             if (PhotonNetwork.LocalPlayer.CustomProperties["team"] == null)
             {
                 var hash = PhotonNetwork.LocalPlayer.CustomProperties;
                 hash.Add("team", teamNum);
                 hash.Add("postion", 0);

                 PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

             }
         }
         else if (PhotonNetwork.CurrentRoom.PlayerCount > (PhotonNetwork.CurrentRoom.MaxPlayers / 2))
         {
             teamNum = 2;
             if (PhotonNetwork.LocalPlayer.CustomProperties["team"] == null)
             {
                 var hash = PhotonNetwork.LocalPlayer.CustomProperties;
                 hash.Add("team", teamNum);
                 hash.Add("postion", 1);
                 PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
             }
         }*/
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " room Is created with max players Number is " + PhotonNetwork.CurrentRoom.MaxPlayers);
        ActivateMyPanel(InsideRoomPanel.name);
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        
        

        if (PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(true);
            springButton.SetActive(true);
            snowButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
            springButton.SetActive(false);
            snowButton.SetActive(false);
        }



        int teamNum = 0;
        int xPos = 0;
        int zPos = 0;
        

        var hash2 = PhotonNetwork.LocalPlayer.CustomProperties;

        if (PhotonNetwork.CurrentRoom.PlayerCount <= (PhotonNetwork.CurrentRoom.MaxPlayers / 2))
        {
            xPos = 20;
            zPos = 10;
            teamNum = 1;
            int team1 = 0;
            foreach (KeyValuePair<int, Player> kvp in PhotonNetwork.CurrentRoom.Players)
            {
                if (kvp.Value.CustomProperties["team"] != null)
                {
                    Debug.Log("__ " + (int)kvp.Value.CustomProperties["team"]);
                    if ((int)kvp.Value.CustomProperties["team"] == 1)
                    {
                       
                        team1++;
                    }

                }

            }
            if (PhotonNetwork.LocalPlayer.CustomProperties["postion"] == null)
            {
                PhotonNetwork.LocalPlayer.CustomProperties["postion"] = team1;
            }
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount > (PhotonNetwork.CurrentRoom.MaxPlayers / 2))
        {
            int team1 = 0;
            int team2 = 0;
            foreach (KeyValuePair<int, Player> kvp in PhotonNetwork.CurrentRoom.Players)
            {
                if (kvp.Value.CustomProperties["team"] != null)
                {
                    Debug.Log("__ " + (int)kvp.Value.CustomProperties["team"]);
                    if ((int)kvp.Value.CustomProperties["team"] == 1)
                    {
                        //zPos = 10;
                        //xPos = 20;
                        team1++;
                    }
                    else if ((int)kvp.Value.CustomProperties["team"] == 2)
                    {
                        //zPos = -10;
                        //xPos = -20;
                        team2++;
                    }
                    
                }


            }

            if (team1 == (PhotonNetwork.CurrentRoom.MaxPlayers / 2))
            {
                teamNum = 2;
                xPos = -20;
                zPos = -10;
                if (PhotonNetwork.LocalPlayer.CustomProperties["postion"] == null)
                {

                    var hash = PhotonNetwork.LocalPlayer.CustomProperties;
                    hash.Add("postion", team2);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                }
            }
            else if (team2 == (PhotonNetwork.CurrentRoom.MaxPlayers / 2))
            {
                //zPos = 10;
                //xPos = 20;
                teamNum = 1;

                if (PhotonNetwork.LocalPlayer.CustomProperties["postion"] == null)
                {
                    var hash = PhotonNetwork.LocalPlayer.CustomProperties;
                    hash.Add("postion", team1);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
                }
            }


        }

        PhotonNetwork.LocalPlayer.SetCustomProperties(hash2);

        if (PhotonNetwork.LocalPlayer.CustomProperties["team"] == null)
        {
            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Add("xPos", xPos);
            hash.Add("zPos", zPos);
            hash.Add("team", teamNum);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        }
        StartCoroutine(addClientss());
    }

    IEnumerator addClientss()
    {
        yield return new WaitForSeconds(1f);

        foreach (Player playerItem in PhotonNetwork.PlayerList)
        {
            AddCientToRoom(playerItem);
            Debug.Log("Team" + PhotonNetwork.LocalPlayer.CustomProperties["team"]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        StartCoroutine(addClient(newPlayer));
    }

    IEnumerator addClient(Player newPlayer)
    {
        yield return new WaitForSeconds(1f);
        AddCientToRoom(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (otherPlayer.IsMasterClient)
        {
            PhotonNetwork.SetMasterClient(otherPlayer.GetNext());
        }

        otherPlayer.CustomProperties.Remove("team");
        otherPlayer.CustomProperties.Remove("zPos");
        otherPlayer.CustomProperties.Remove("xPos");
        otherPlayer.CustomProperties.Remove("postion");

        Destroy(playersList[otherPlayer.ActorNumber]);
        playersList.Remove(otherPlayer.ActorNumber);
        if (PhotonNetwork.IsMasterClient)
        {
            if (PlayButton != null)
            {
                PlayButton.SetActive(true);
                springButton.SetActive(true);
                snowButton.SetActive(true);
            }

        }
        else
        {
            if (PlayButton != null)
            {
                PlayButton.SetActive(false);
                springButton.SetActive(false);
                snowButton.SetActive(false);
            }

        }

    }

    public override void OnLeftRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            ActivateMyPanel(LobbyPanel.name);
        }


        foreach (GameObject obj in playersList.Values)
        {
            Destroy(obj);
        }
    }

    public override void OnLeftLobby()
    {
        ClearRoomList();
        roomListData.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomList();

        foreach (RoomInfo rooms in roomList)
        {
            Debug.Log("Room Name :" + rooms.Name);
            if (!rooms.IsOpen || !rooms.IsVisible || rooms.RemovedFromList)
            {
                if (roomListData.ContainsKey(rooms.Name))
                {
                    roomListData.Remove(rooms.Name);
                }
            }
            else
            {
                if (roomListData.ContainsKey(rooms.Name))
                {
                    roomListData[rooms.Name] = rooms;
                }
                else
                {
                    roomListData.Add(rooms.Name, rooms);
                }
            }

        }

        // Generate Rooms List

        foreach (RoomInfo roomItem in roomListData.Values)
        {
            Debug.Log("Instantiate room");
            GameObject roomListItemObject = Instantiate(roomListPrefab, roomListParent.transform);
            roomListItemObject.SetActive(true);
            //roomListItemObject.transform.SetParent(roomListParent.transform);
            //roomListItemObject.GetComponent<RectTransform>().rect.Set(0,0,900,70);
            roomListItemObject.transform.localScale = Vector3.one;
            roomListItemObject.GetComponent<RectTransform>().localPosition.Set(0, 0, 1);
            //roomListItemObject.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 1f);
            Debug.Log("Room is " + roomItem.Name);
            roomListItemObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = roomItem.Name;
            roomListItemObject.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = roomItem.MaxPlayers + " / ";
            roomListItemObject.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = roomItem.PlayerCount.ToString();

            if (roomItem.PlayerCount == roomItem.MaxPlayers)
            {
                roomListItemObject.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                roomListItemObject.transform.GetChild(4).gameObject.SetActive(true);
                roomListItemObject.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    RoomJoinFromList(roomItem.Name);
                });
            }

            roomListGameobject.Add(roomItem.Name, roomListItemObject);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom("random" + Random.Range(0, 100000000) + Time.deltaTime, 4);

    }
    #endregion

    #region Public_Methods

    public void CreateRoom(string roomName, int maxPlayers)
    {
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)maxPlayers;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        
    }

    public void ActivateMyPanel(string panelName)
    {
        LobbyPanel.SetActive(panelName.Equals(LobbyPanel.name));
        //PlayerNamePanel.SetActive(panelName.Equals(PlayerNamePanel.name));
        RoomCreatePanel.SetActive(panelName.Equals(RoomCreatePanel.name));
        //ConnectingPanel.SetActive(panelName.Equals(ConnectingPanel.name));
        RoomListPanel.SetActive(panelName.Equals(RoomListPanel.name));
        InsideRoomPanel.SetActive(panelName.Equals(InsideRoomPanel.name));
    }

    public void RoomJoinFromList(string roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ClearRoomList()
    {
        if (roomListGameobject.Count > 0)
        {
            foreach (var item in roomListGameobject.Values)
            {
                Destroy(item);
            }
            roomListGameobject.Clear();
        }

    }
    public void AddCientToRoom(Player player)
    {
        Debug.Log("tttttttttttttttttt " + PhotonNetwork.LocalPlayer.CustomProperties["xPos"]);
        Debug.Log("tttttttttttttttttt " + PhotonNetwork.LocalPlayer.CustomProperties["team"]);
        if ((int)player.CustomProperties["team"] == 1)
        {
            
            

            Debug.Log("xxxxxxxxxxxxxx " + PhotonNetwork.LocalPlayer.CustomProperties["xPos"]);

            GameObject playerListItemObject;
            playerListItemObject = Instantiate(playerListItemPrefab2, PlayerListItemParentTeam1.transform);
            playerListItemObject.transform.localScale = Vector3.one;
            playerListItemObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = player.NickName + player.ActorNumber;

            if (playersList.ContainsKey(player.ActorNumber))
            {
                playersList[player.ActorNumber] = playerListItemObject;
            }
            else
            {
                playersList.Add(player.ActorNumber, playerListItemObject);
            }
        }
        else if ((int)player.CustomProperties["team"] == 2)
        {
            Debug.Log("xxxxxxxxxxxxxx " + PhotonNetwork.LocalPlayer.CustomProperties["xPos"]);
            GameObject playerListItemObject;
            playerListItemObject = Instantiate(playerListItemPrefab2, PlayerListItemParentTeam2.transform);
            playerListItemObject.transform.localScale = Vector3.one;
            playerListItemObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = player.NickName;

            if (playersList.ContainsKey(player.ActorNumber))
            {
                playersList[player.ActorNumber] = playerListItemObject;
            }
            else
            {
                playersList.Add(player.ActorNumber, playerListItemObject);
            }
        }

    }

    #endregion

}