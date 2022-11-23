using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scripts.PlayerScripts;

public class GameManager : MonoBehaviour
{

    bool statred;
    int targetNum = 20;
    public Team FirstTeam;
    public Team SecondTeam;

    [SerializeField]
    Transform [] SpwanPoints;

    GameObject newPlayer;

    public static event Action WinEvent;
    public static event Action LoseEvent;

    void Start()
    {
        

        FollowCamera cam =GameObject.Find("Follow Camera").GetComponent<FollowCamera>();

        string playerName = PlayerPrefs.GetString("Player","Archer");
        /*
        switch (playerName)
        {
            case "Archer":
                 newPlayer = (GameObject)Instantiate(Resources.Load("PlayersPrefs/Archer"),SpwanPoints[0].position,Quaternion.identity);
                break;

            case "Wizard":
                  newPlayer = (GameObject)Instantiate(Resources.Load("PlayersPrefs/Wizard"),SpwanPoints[1].position,Quaternion.identity);
                break;

            case "Warrior":
                  newPlayer = (GameObject)Instantiate(Resources.Load("PlayersPrefs/Warrior"),SpwanPoints[2].position,Quaternion.identity);
                 
                break;
        }
                     
            cam.setTarget(newPlayer.transform);
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void checkWin(){
	    
        if(FirstTeam.currentScore >=targetNum )
            // WinEvent?.Invoke(t_1)
            // LoseEvent?.Invoke(t_2)
        else if(SecondTeam.currentScore >=targetNum)
            // WinEvent?.Invoke(t_2)
            // LoseEvent?.Invoke(t_1)
        
    }       
*/


}
