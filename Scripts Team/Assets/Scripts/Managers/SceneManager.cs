using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using Photon.Pun.UtilityScripts;
using TMPro;
namespace Heros.Manager
{
    public class SceneManager : MonoBehaviourPunCallbacks
    {

        #region Variables

        [Header("Player Resources")]
        public GameObject Wizard;
        public GameObject Archer;

        int playerNum = 0;
        [SerializeField]
        public Transform[] SpwanPointsTeam1;
        public Transform[] SpwanPointsTeam2;
        GameObject thisPlayer;
        Photon.Realtime.Player thisPlayerPun;
        public TextMeshProUGUI playerName;
        GameObject player;
        #endregion


        void Awake()
        {

            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["xPos"]);
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["team"]);

            int x = (int)PhotonNetwork.LocalPlayer.CustomProperties["xPos"];
            int z = 0;//(int)PhotonNetwork.LocalPlayer.CustomProperties["zPos"];

            if (PlayerPrefs.GetString("CH", "Wizard") == "Wizard")
            {
                player = (GameObject)PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/" + Wizard.name,
                    new Vector3(x, 0, z)
                    , Quaternion.identity);

            }
            else if (PlayerPrefs.GetString("CH", "Wizard") == "Archer")
            {
                player = (GameObject)PhotonNetwork.Instantiate("Prefab/PlayersPrefabs/" + Archer.name,
                    new Vector3(x, 0, z)
                    , Quaternion.identity);
            }


            thisPlayer = GameObject.FindObjectOfType<Heros.Players.Player>().gameObject;
            thisPlayerPun = thisPlayer.GetComponent<PhotonView>().Owner;

            playerName.text =thisPlayerPun.NickName + thisPlayerPun.CustomProperties["xPos"].ToString();

            if ((int)thisPlayerPun.CustomProperties["team"] == 1)
            {
                //player.transform.position = SpwanPointsTeam1[(int)thisPlayerPun.CustomProperties["postion"]].position;
                //thisPlayer.GetComponentInParent<CheckPhoton>().respawnPos = SpwanPointsTeam1[(int)thisPlayerPun.CustomProperties["postion"]].position;

                GetComponent<PhotonView>().RPC("addTOTeam", RpcTarget.AllBuffered, true);

            }
            else if ((int)thisPlayerPun.CustomProperties["team"] == 2)
            {
                //player.transform.position = SpwanPointsTeam2[(int)thisPlayerPun.CustomProperties["postion"]].position;
                //thisPlayer.GetComponentInParent<CheckPhoton>().respawnPos = SpwanPointsTeam2[(int)thisPlayerPun.CustomProperties["postion"]].position;
                GetComponent<PhotonView>().RPC("addTOTeam", RpcTarget.AllBuffered, false);
            }
        }

        [PunRPC]
        public void addTOTeam(bool first)
        {
            if (first)
            {
                GameObject.Find("Teams").GetComponent<Team>().playersBlue.Add(GameObject.FindObjectOfType<Heros.Players.Player>().gameObject);
            }
            else
            {
                GameObject.Find("Teams").GetComponent<Team>().playersRed.Add(GameObject.FindObjectOfType<Heros.Players.Player>().gameObject);
            }
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                foreach (KeyValuePair<int, Player> kvp in PhotonNetwork.CurrentRoom.Players)
                {
                    Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
                    Debug.Log("avatar = " + kvp.Value.CustomProperties["avatar"]);
                    Debug.Log("team = " + kvp.Value.CustomProperties["team"]);
                    Debug.Log("position = " + kvp.Value.CustomProperties["team"]);
                    Debug.Log("kills = " + kvp.Value.CustomProperties["kills"]);
                    Debug.Log("dead = " + kvp.Value.CustomProperties["dead"]);
                }
            }
        }


    }
}