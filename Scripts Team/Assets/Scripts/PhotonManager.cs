using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region Variables
    [Header("Input Fields")]
    public InputField userNameText;
    public InputField roomNameText;
    public InputField maxPlayerNumberText;

    [Header("Ui Panels")]
    public GameObject PlayerNamePanel;
    public GameObject LobbyPanel;
    public GameObject RoomCreatePanel;
    public GameObject ConnectingPanel;
    public GameObject RoomListPanel;
    public GameObject InsideRoomPanel;

    [Header("Ui Buttons")]
    public GameObject PlayButton;

    [Header("Room List Variables")]
    public GameObject roomListPrefab;
    public GameObject roomListParent;
    private Dictionary<string, RoomInfo> roomListData;
    private Dictionary<string, GameObject> roomListGameobject;

    [Header("Players List Variables")]
    public GameObject playerListItemPrefab1;
    public GameObject playerListItemPrefab2;
    public GameObject playerListItemPrefab3;
    public GameObject PlayerListItemParent;
    private Dictionary<int, GameObject> playersList;
    private int avatar = 0;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        ActivateMyPanel(PlayerNamePanel.name);
        roomListData = new Dictionary<string, RoomInfo>();
        roomListGameobject = new Dictionary<string, GameObject>();
        playersList = new Dictionary<int, GameObject>();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Network state : " + PhotonNetwork.NetworkClientState);
    }
    #endregion

    #region UiMethods

    public void OnLoginClick()
    {
        string name = userNameText.text;
        if (!string.IsNullOrEmpty(name))
        {
            PhotonNetwork.LocalPlayer.NickName = name;
            PhotonNetwork.ConnectUsingSettings();
            ActivateMyPanel(ConnectingPanel.name);
        }
        else
        {
            Debug.Log("Empty name");
        }
    }

    public void OnRoomCreateClick()
    {
        string roomName = roomNameText.text;
        if (string.IsNullOrEmpty(roomName))
        {
            roomName = roomName + Random.Range(0,1000);
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte) int.Parse(maxPlayerNumberText.text);
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void OnCancelClick()
    {
        ActivateMyPanel(LobbyPanel.name);
    }

    public void RoomListBtnClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        ActivateMyPanel(RoomListPanel.name);
    }

    public void BackFromRoomList()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivateMyPanel(LobbyPanel.name);
    }

    public void BackFromPlayersList()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        ActivateMyPanel(LobbyPanel.name);
    }

    public void OnClickPlayButton()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    #endregion

    #region PHOTON_CALLBACKS

    public override void OnConnected()
    {
        Debug.Log("Connected to server");
        // custome avatar
        avatar = Random.Range(1,3);
        var hash = PhotonNetwork.LocalPlayer.CustomProperties;
        hash.Add("avatar", avatar);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        Debug.Log(PhotonNetwork.LocalPlayer.ToStringFull());

        ActivateMyPanel(LobbyPanel.name);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is connected to photon...");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name +" room Is created with max players Number is " + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    public override void OnJoinedRoom()
    {
        
        ActivateMyPanel(InsideRoomPanel.name);

        if (PhotonNetwork.IsMasterClient)
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
        }

        

        foreach (Player playerItem in PhotonNetwork.PlayerList)
        {
            Debug.Log(playerItem.NickName + " Room Joined !");

            AddCientToRoom(playerItem);


        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        AddCientToRoom(newPlayer);

    }

     public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
     {
         Destroy(playersList[otherPlayer.ActorNumber]);
         playersList.Remove(otherPlayer.ActorNumber);
     }



    public override void OnLeftRoom()
    {
        ActivateMyPanel(LobbyPanel.name);

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
            Debug.Log("Room Name :"+ rooms.Name);
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
            GameObject roomListItemObject = Instantiate(roomListPrefab);
            roomListItemObject.transform.SetParent(roomListParent.transform);
            roomListItemObject.transform.localScale = Vector3.one;
            roomListItemObject.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 1f);
            Debug.Log("Room is "+roomItem.Name);
            roomListItemObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = roomItem.Name;
            roomListItemObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = roomItem.PlayerCount + " / " + roomItem.MaxPlayers;
            if (roomItem.PlayerCount == roomItem.MaxPlayers) {
                roomListItemObject.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                roomListItemObject.transform.GetChild(2).gameObject.SetActive(true);
                roomListItemObject.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    RoomJoinFromList(roomItem.Name);
                });
            }
            
            roomListGameobject.Add(roomItem.Name,roomListItemObject);
        }
    }
    #endregion

    #region Public_Methods

    public void ActivateMyPanel(string panelName)
    {
        LobbyPanel.SetActive(panelName.Equals(LobbyPanel.name));
        PlayerNamePanel.SetActive(panelName.Equals(PlayerNamePanel.name));
        RoomCreatePanel.SetActive(panelName.Equals(RoomCreatePanel.name));
        ConnectingPanel.SetActive(panelName.Equals(ConnectingPanel.name));
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
        GameObject playerListItemObject;
        int prefab = (int)player.CustomProperties["avatar"];
        switch (prefab)
        {
            case 1:
                playerListItemObject = Instantiate(playerListItemPrefab1);
                break;
            case 2:
                playerListItemObject = Instantiate(playerListItemPrefab2);
                break;
            case 3:
                playerListItemObject = Instantiate(playerListItemPrefab3);
                break;
            default:
                playerListItemObject = Instantiate(playerListItemPrefab1);
                break;
        }

        //playerListItemObject = Instantiate(playerListItemPrefab1);
        playerListItemObject.transform.SetParent(PlayerListItemParent.transform);
        playerListItemObject.transform.localScale = Vector3.one;
        //playerListItemObject.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 1f);
        playerListItemObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = player.NickName;

        if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            playerListItemObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            playerListItemObject.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (playersList.ContainsKey(player.ActorNumber))
        {
            playersList[player.ActorNumber] = playerListItemObject;
        }
        else
        {
            playersList.Add(player.ActorNumber, playerListItemObject);
        }
    }

    #endregion
}
