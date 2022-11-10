using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Player", order = 1)]

public class PlayerData : ScriptableObject
{
    


    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;


}
