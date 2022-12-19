using Heros.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Team : MonoBehaviour
{
    [Header("Players Arrays")]
    public List<GameObject> playersRed, playersBlue;

    [Header("Counters texts")]
    public Text txtCounterRed, txtCounterBlue;
    public int currentScoreRed, currentScoreBlue;

    [Header("Players Data Panel")]
    public GameObject PlayersDataPanel, playersRedParent, playersBlueParent, playerDataPref;
    List<GameObject> PlayersData;

    [Header("End Game Panel")]
    [SerializeField]
    GameObject endGamePanel;

    [SerializeField]
    TextMeshProUGUI redLabel, blueLable, AllKillsRed, AllKillsBlue;

    [SerializeField]
    GameObject[] playersRedArr, playersBlueArr;


    public RedAndBlueTeamScore redAndBlueTeam;

    public int target = 5;

    private void Awake()
    {
        PlayersData = new List<GameObject>();
    }

    void LateUpdate()
    {
        txtCounterRed.text = redAndBlueTeam.redTeam + ""; 
        if (redAndBlueTeam.blueTeam == target)
        {
            GetComponent<PhotonView>().RPC("Win", RpcTarget.AllBuffered, true);
        }
        txtCounterBlue.text = redAndBlueTeam.blueTeam + "";
        if (redAndBlueTeam.redTeam == target)
        {
            GetComponent<PhotonView>().RPC("Win", RpcTarget.AllBuffered, false);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayersDataPanel.SetActive(true);

            for (int i = 0; i < playersRed.Count; i++)
            {
                if (playersRed[i].gameObject != null)
                {
                    GameObject playerData = Instantiate(playerDataPref);
                    playerData.transform.SetParent(playersRedParent.transform);
                    playerData.transform.localScale = Vector3.one;
                    playerData.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 1f);
                    playerData.GetComponent<TextMeshProUGUI>().text = playersRed[i].GetComponent<PhotonView>().Owner.NickName;
                    playerData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = playersRed[i].GetComponent<Player>().numOfKills + "";
                    playerData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = playersRed[i].GetComponent<Player>().numOfDead + "";
                    PlayersData.Add(playerData);
                }
            }

            for (int i = 0; i < playersBlue.Count; i++)
            {
                if (playersBlue[i].gameObject != null)
                {
                    GameObject playerData = Instantiate(playerDataPref);
                    playerData.transform.SetParent(playersBlueParent.transform);
                    playerData.transform.localScale = Vector3.one;
                    playerData.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 1f);
                    playerData.GetComponent<TextMeshProUGUI>().text = playersBlue[i].GetComponent<PhotonView>().Owner.NickName;
                    playerData.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = playersBlue[i].GetComponent<Player>().numOfKills + "";
                    playerData.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = playersBlue[i].GetComponent<Player>().numOfDead + "";
                    PlayersData.Add(playerData);
                }
            }

        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            PlayersDataPanel.SetActive(false);
            foreach (GameObject p in PlayersData)
            {
                Destroy(p);
            }
        }

    }


    [PunRPC]
    public void Win(bool isRed)
    {
        StartCoroutine(finishGame(isRed));
    }
    IEnumerator finishGame(bool isRed)
    {
        yield return new WaitForSeconds(2f);
        endGamePanel.SetActive(true);
        if (isRed)
        {
            redLabel.text = "Winner";
            blueLable.text = "Loooser";
        }
        else
        {

            redLabel.text = "Loooser";
            blueLable.text = "Winner";
        }

        for (int i = 0; i < playersBlue.Count; i++)
        {
            if (playersBlue[i].gameObject != null)
            {
                playersBlueArr[i].SetActive(true);
                playersBlueArr[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = playersBlue[i].GetComponent<PhotonView>().Owner.NickName;
                playersBlueArr[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = playersBlue[i].GetComponent<Player>().numOfKills + "";
            }
        }
        for (int i = 0; i < playersRed.Count; i++)
        {
            if (playersRed[i].gameObject != null)
            {
                playersRedArr[i].SetActive(true);
                playersRedArr[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = playersRed[i].GetComponent<PhotonView>().Owner.NickName;
                playersRedArr[i].transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = playersRed[i].GetComponent<Player>().numOfKills + "";
            }
        }

        AllKillsRed.text = redAndBlueTeam.blueTeam + "/" + target;
        AllKillsBlue.text = redAndBlueTeam.redTeam + "/" + target;
        // Time.timeScale = 0;
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("team");
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("postion");
        PhotonNetwork.Disconnect();
        Destroy(GameObject.Find("Room"));
        Destroy(GameObject.Find("SaveDataDontDestroy"));


        Destroy(this.gameObject);

    }


}
