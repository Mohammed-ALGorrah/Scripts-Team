using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Player", order = 1)]

public class PlayerData : ScriptableObject
{
    
    public string prefabName;
    public int numberOfPrefabsToCreate;
    public string image;
    public string name;
    public string bio;
    public float attackRange;
    public int basicSkillDamage;
    public int speacialSkill;
    public PlayerType plarType;
    public Animator animator;
    public AnimatorOverrideController animatorOverrideController;
    public int maxHealth;
    public int maxCharge;




}
