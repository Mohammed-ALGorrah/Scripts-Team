using Heros.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Team : MonoBehaviour
{
    public List <GameObject> playersRed,playersBlue;
    public Text txtCounterRed,txtCounterBlue;
    public int currentScoreRed,currentScoreBlue;  

    public GameObject PlayersDataPanel,playersDataParent,playerDataPref,winMenu;
    List<GameObject> PlayersData;

    public int target = 3;

    private void Awake() {
        PlayersData = new List<GameObject>();
    }

    void LateUpdate()
    {
        
        if (txtCounterRed != null)
        {
            currentScoreRed = 0;
            for (int i = 0; i < playersRed.Count; i++)
            {
                if (playersRed[i].GetComponent<Player>() != null)
                {
                    currentScoreRed += playersRed[i].GetComponent<Player>().numOfKills;
                }
            }
            txtCounterRed.text = currentScoreRed + "";
            if (currentScoreRed == target)
            {
                GetComponent<PhotonView>().RPC("Win",RpcTarget.AllBuffered,"Red Team Win");
            }

        }

        if (txtCounterBlue != null)
        {
            currentScoreBlue = 0;
            for (int i = 0; i < playersBlue.Count; i++)
            {
                if (playersBlue[i].GetComponent<Player>()!= null)
                {
                    currentScoreBlue += playersBlue[i].GetComponent<Player>().numOfKills;
                }
            }
            txtCounterBlue.text = currentScoreBlue + "";
            if (currentScoreBlue == target)
            {
                GetComponent<PhotonView>().RPC("Win",RpcTarget.AllBuffered,"Blue Team Win");
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PlayersDataPanel.SetActive(true);
            for (int i = 0; i < playersRed.Count; i++)
            {
                GameObject playerData = Instantiate(playerDataPref);
                playerData.transform.SetParent(playersDataParent.transform);
                playerData.transform.localScale = Vector3.one;
                playerData.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 1f);
                playerData.transform.GetChild(0).gameObject.GetComponent<Text>().text = playersRed[i].GetComponent<PhotonView>().Owner.NickName;
                playerData.transform.GetChild(1).gameObject.GetComponent<Text>().text = playersRed[i].GetComponent<Player>().numOfKills+"";
                playerData.transform.GetChild(2).gameObject.GetComponent<Text>().text = playersRed[i].GetComponent<Player>().numOfDead+"";
                PlayersData.Add(playerData);
            }

            for (int i = 0; i < playersBlue.Count; i++)
            {
                GameObject playerData = Instantiate(playerDataPref);
                playerData.transform.SetParent(playersDataParent.transform);
                playerData.transform.localScale = Vector3.one;
                playerData.transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 1f);
                playerData.transform.GetChild(0).gameObject.GetComponent<Text>().text = playersBlue[i].GetComponent<PhotonView>().Owner.NickName;
                playerData.transform.GetChild(1).gameObject.GetComponent<Text>().text = playersBlue[i].GetComponent<Player>().numOfKills+"";
                playerData.transform.GetChild(2).gameObject.GetComponent<Text>().text = playersBlue[i].GetComponent<Player>().numOfDead+"";
                PlayersData.Add(playerData);
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
    public void Win(string txt){
        Invoke("Winn",1.5f);
            
    }

    void Winn(string txt){
            winMenu.SetActive(true);
            winMenu.transform.GetChild(0).gameObject.GetComponent<Text>().text = txt;
            Time.timeScale = 0;
    }
}
