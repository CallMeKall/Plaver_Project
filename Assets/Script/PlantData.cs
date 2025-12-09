using UnityEngine;

[CreateAssetMenu(fileName = "PlantData", menuName = "Farming/New Plant")]
public class PlantData : ScriptableObject
{
    public string plantName;
    public Sprite[] growthStages;  // 0=seed, 1=small, etc
    public float timePerStage = 3f;
}