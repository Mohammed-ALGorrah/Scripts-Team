using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Skill", order = 2)]

public class SkillData : ScriptableObject
{
    public Sprite skillImage; 
    public string skillName;
    public string skillDescription;
    public SkillType skillType;
    public int skillDmg;
    public int skillRange;
    public int skillWidth;
    public int skillCooldown;
    public bool hasProjectile;
    public GameObject skillIndicator;
    public GameObject skillProjectile;
    public float ProjectileLifeTime;
    public float ProjectiSpeed;
}

public enum SkillType{
    NORMAL,
    SPECIAl,
}