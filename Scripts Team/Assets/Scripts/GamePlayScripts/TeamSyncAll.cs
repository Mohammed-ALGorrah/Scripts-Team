using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamSyncAll : MonoBehaviourPunCallbacks
{
    public List<int> Team1Players;
    public List<int> Team2Players;

    public int playerNum ;

    public int playerNum2;

    public Transform[] SpwanPoints;

    private void Awake()
    {
        OnInstanPlayers();
    }

    private void Start()
    {
        GetComponent<PhotonView>().RPC("TestPlayerTeams", RpcTarget.AllBuffered);
        //GetComponent<PhotonView>().RPC("SetPlayerPos", RpcTarget.AllBuffered);

    }


    [PunRPC]
    private void SetPlayerPos()
    {
        GameObject thisPlayer = GameObject.FindObjectOfType<Heros.Players.Player>().gameObject;
        thisPlayer.transform.position = SpwanPoints[thisPlayer.GetComponentInParent<CheckPhoton>().playerPos].position;
    }


    [PunRPC]
    private void TestPlayerTeams()
    {      
        playerNum++;
        GameObject thisPlayer = GameObject.FindObjectOfType<Heros.Players.Player>().gameObject;
        Photon.Realtime.Player thisPlayerPun = thisPlayer.GetComponent<PhotonView>().Owner;
        if ((int)thisPlayerPun.CustomProperties["team"] == 1)
        {
            thisPlayer.GetComponentInParent<CheckPhoton>().team = 1;
            thisPlayer.GetComponentInParent<CheckPhoton>().playerPos = playerNum;
        }
        else if ((int)thisPlayerPun.CustomProperties["team"] == 2)
        {
            thisPlayer.GetComponentInParent<CheckPhoton>().team = 2;
            thisPlayer.GetComponentInParent<CheckPhoton>().playerPos =  playerNum;
        }
        
    }

    private void OnInstanPlayers()
    {
        Photon.Realtime.Player[] playersOnPun = PhotonNetwork.PlayerList;
        for (int i = 0; i < playersOnPun.Length; i++)
        {
            Debug.Log("Players Teams @@ " + (int)playersOnPun[i].CustomProperties["team"]);
            playerNum2++;
            if ((int)playersOnPun[i].CustomProperties["team"] == 1)
            {
                Team1Players.Add(playerNum2);
                //transform.SetParent(GameObject.Find("FirstTeam").transform);
            }
            else if ((int)playersOnPun[i].CustomProperties["team"] == 2)
            {
                Team2Players.Add(playerNum2);
                //transform.SetParent(GameObject.Find("SecondTeam").transform);
            }
        }
    }
    

}
