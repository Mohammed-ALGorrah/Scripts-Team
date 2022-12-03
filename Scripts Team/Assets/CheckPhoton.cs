using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPhoton : MonoBehaviourPunCallbacks 
{
    public int team;
    public int playerPos;
    public Text test;
    public TeamSyncAll tt;
    private void Awake()
    {
        tt = GameObject.FindObjectOfType<TeamSyncAll>();
        test = GameObject.Find("TextTest").GetComponent<Text>();
    }
    private void Start()
    {
        if (team == 1)
        {
            transform.position = tt.SpwanPoints[playerPos].position;
            transform.SetParent(GameObject.Find("FirstTeam").transform);
            test.text = "Team :"+ team;
        }
        else if (team == 2)
        {
            transform.position = tt.SpwanPoints[playerPos].position;
            transform.SetParent(GameObject.Find("SecondTeam").transform);
            test.text = "Team :" + team;
        }
    }


    public bool CheckFriend2(int playerTeam)
    {
        if (team == playerTeam)
        {
            Debug.Log("Friend ^^ ");
            return true;
        }
        else
            return false;
    }

    
}
