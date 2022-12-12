using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPanel : MonoBehaviour
{
    [Header("Map panel")]
    public GameObject Panel;
    [Header("Room Panel")]
    public GameObject roomPanel;
    [Header("Array of Map")]
    public GameObject[] maps;
    public int selectedMap = 0;
    public void OpenPanel()
    {
        Panel.SetActive(true);
    }
    public void closePanel()
    {
        Panel.SetActive(false);
    }
    public void openRoomPanel()
    {
        roomPanel.SetActive(true);
    }
    public void NextMap()
    {
        maps[selectedMap].SetActive(false);
        selectedMap = (selectedMap + 1) % maps.Length;
        maps[selectedMap].SetActive(true);
    }
    public void PreviousMap()
    {
        maps[selectedMap].SetActive(false);
        selectedMap--;
        if (selectedMap < 0)
        {
            selectedMap += maps.Length;
        }
        maps[selectedMap].SetActive(true);
    }
}
