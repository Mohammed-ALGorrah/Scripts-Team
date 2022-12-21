using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using ExitGames.Client.Photon;
using Photon.Pun;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    #region Variables

    #region Connection Variables
    [Header("Connection Variables")]

    public GameObject joinChatBtn;
    ChatClient chatClient;
    bool isConnected;

    public string userName;
    #endregion

    #region UI Variables

    [Header("UI Variables")]
    public GameObject lobbyPanel;
    public GameObject chatPanel;
    string privateReceiver;
    string currentChat;
    public InputField chatField,receiverField;
    public GameObject chatDisplay;
    public GameObject parentList;
    #endregion

    #endregion

    #region Unity Methods
    void Start()
    {
        DontDestroyOnLoad(gameObject);

    }
    void Update()
    {
        if (this.chatClient != null)
        {
            chatClient.Service();
        } 

        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
            SubmitPrivateChatOnClick();
        }
        
    }

    #endregion

    #region Chat Client Listener
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    public void OnChatStateChange(ChatState state)
    {
    }

    public void OnConnected()
    {
        isConnected = true;
        Debug.Log("Connected");
        //joinChatBtn.SetActive(true);
        chatClient.Subscribe(new string[] {"RegionChannel"});
    }

    public void OnDisconnected()
    {
        
        //foreach (var item in parentList.GetComponentsInChildren<Transform>())
        //{

        //        Destroy(item.gameObject);
            
        //}
        receiverField.text = "";
        chatField.text = "";
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            GameObject msg = Instantiate(chatDisplay,parentList.transform);
            msg.GetComponent<Text>().text = senders[i]+" : " +messages[i];
            /*
            msgs = string.Format("{0}:{1}", senders[i], messages[i]);
            chatDisplay.text += msgs + "\n";
            Debug.Log(msgs);
            */
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        GameObject msg = Instantiate(chatDisplay, parentList.transform);
        msg.GetComponent<Text>().text = sender + " : " + message;
        /*
        string msg = "";
        msg = string.Format("{0}:{1}", sender, message);
        chatDisplay.text += "\n" + msg;
   */
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }
    #endregion

    #region Public Methods
    #region Connection Methods
    public void ChatConnectOnClick() {
        userName = PhotonNetwork.LocalPlayer.NickName;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(userName));
        Debug.Log("Connecting");
    }
    public void ChatDisconnectOnClick()
    {
        chatClient.Disconnect();
    }
    #endregion
    #region Send Message Methods
    public void SubmitPublicChatOnClick()
    {
        if (privateReceiver == null)
        {
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
            Debug.Log("Public msg");
        }
    }
    public void SubmitPrivateChatOnClick()
    {
        if (privateReceiver != null)
        {
            chatClient.SendPrivateMessage(privateReceiver, currentChat);
            chatField.text = "";
            currentChat = "";
        }
    }
    #endregion
    #region Input Field Methods
    public void TypeChatOnValueChange(string valueIn)
    {
        currentChat = valueIn;
    }
    public void ReceiverOnValueChange(string valueIn)
    {
        privateReceiver = valueIn;
    }
    #endregion
    #endregion

}
