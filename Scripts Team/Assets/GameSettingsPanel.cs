using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSettingsPanel : MonoBehaviour
{

    public GameObject settingPanel;
    public void BackToHomeBtn()
    {
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("team");
        PhotonNetwork.LocalPlayer.CustomProperties.Remove("postion");
        PhotonNetwork.Disconnect();
        Destroy(GameObject.Find("Room"));
        Destroy(GameObject.Find("SaveDataDontDestroy"));
        StartCoroutine(WaitLoad());
    }

    IEnumerator WaitLoad()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }

    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
    }

    public void OpenSettingPanel()
    {

        settingPanel.SetActive(true);
    }
}
