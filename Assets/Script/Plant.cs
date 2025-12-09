using UnityEngine;

public class Plant : MonoBehaviour
{
    public PlantData data;

    private SpriteRenderer sr;
    private int stage = 0;
    private float timer = 0f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = data.growthStages[0];
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= data.timePerStage && stage < data.growthStages.Length - 1)
        {
            stage++;
            sr.sprite = data.growthStages[stage];
            timer = 0f;
        }
    }

    public bool IsFullyGrown()
    {
        return stage == data.growthStages.Length - 1;
    }
}