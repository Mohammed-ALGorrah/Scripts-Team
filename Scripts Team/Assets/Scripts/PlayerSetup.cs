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
    [Header("Test data")]
    public Text numText;
    int num = 0;

    #endregion

    #region UnityMethods
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
            //PlayerCanvas.SetActive(false);
            PlayerCamera.SetActive(false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // change value on all clients
        if (Input.GetKeyDown(KeyCode.Z))
        {
            num++;
            numText.text = "Number = " + num;
            photonView.RPC("changeText",RpcTarget.All, num);
        }
    }

    #endregion

    #region Public_Methods

    [PunRPC]
    public void changeText(int x)
    {
        num = x;
        numText.text = "Number = " + num;

    }

    #endregion
}
