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
    public bool HelaingSkill;
    public Player player;
    public GameObject hitEffect;
    public GameObject skillIndicator;
    public GameObject skillProjectile;
    public float ProjectileLifeTime;
    public float ProjectiSpeed;
    public Vector3 skillRotation = new Vector3(0,0,0);
}

public enum SkillType{
    NORMAL,
    SPECIAl,
}