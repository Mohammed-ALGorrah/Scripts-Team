using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Player", order = 1)]

public class PlayerData : ScriptableObject
{    
    public string prefabName;
    public Image image;
    public string bio;
    public PlayerType playerType;
    public Animator animator;
    public int maxHealth;
    public int maxCharge;
}
