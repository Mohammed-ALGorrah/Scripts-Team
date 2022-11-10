using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    bool statred;
    int targetNum = 20;
    // Team FirstTeam;
    // Team SecondTeam;
    public static event Action WinEvent;
    public static event Action LoseEvent;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void checkWin(){
	    /*
        if(t_1.currentScore >=targetNum )
            WinEvent?.Invoke(t_1)
            LoseEvent?.Invoke(t_2)
        else if(t_2.currentScore >=targetNum)
            WinEvent?.Invoke(t_2)
            LoseEvent?.Invoke(t_1)
        */
    }       



}
