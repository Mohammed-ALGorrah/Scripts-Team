using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{

    #region Variables

    [Header("Player Resources")]
    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;

    internal static void LoadScene(string v)
    {
        throw new System.NotImplementedException();
    }

    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            int randomNumber1 = Random.Range(-10, 10);
            int randomNumber2 = Random.Range(-10,10);

            int prefab = (int)PhotonNetwork.LocalPlayer.CustomProperties["avatar"];
            switch (prefab)
            {
                case 1:
                    PhotonNetwork.Instantiate(playerPrefab1.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
                case 2:
                    PhotonNetwork.Instantiate(playerPrefab2.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
                case 3:
                    PhotonNetwork.Instantiate(playerPrefab3.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
                default:
                    PhotonNetwork.Instantiate(playerPrefab1.name, new Vector3(randomNumber1, 5f, randomNumber2), Quaternion.identity);
                    break;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

}
