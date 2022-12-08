using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Heros.Players;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    #region Variables

    public GameObject PlayerCanvas;
    public GameObject PlayerCamera;
    public GameObject Player;
    public GameObject PlayerAttack;
    public GameObject PlayerMove;
    public GameObject HealthSystem;
    public GameObject ChargeSystem;
    public GameObject FirePoint;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            Player.GetComponent<Player>().enabled = true;
            PlayerAttack.GetComponent<PlayerAttack>().enabled = true;
            PlayerMove.GetComponent<PlayerMove>().enabled = true;
            HealthSystem.GetComponent<HealthSystem>().enabled = true;
            ChargeSystem.GetComponent<ChargeSystem>().enabled = true;
            FirePoint.SetActive(true);

            // PlayerCanvas.SetActive(true);
            PlayerCamera.SetActive(true);
        }
        else
        {
            HealthSystem.GetComponent<HealthSystem>().maxHealth = Player.GetComponent<Player>().playerData.maxHealth;
            HealthSystem.GetComponent<HealthSystem>().currentHealth = Player.GetComponent<Player>().playerData.maxHealth;

            Player.GetComponent<Player>().enabled = false;
            PlayerAttack.GetComponent<PlayerAttack>().enabled = false;
            PlayerMove.GetComponent<PlayerMove>().enabled = false;
            HealthSystem.GetComponent<HealthSystem>().enabled = false;
            ChargeSystem.GetComponent<ChargeSystem>().enabled = false;
            FirePoint.SetActive(false);
            //PlayerCanvas.SetActive(false);
            PlayerCamera.SetActive(false);
            
        }
    }
    

}
