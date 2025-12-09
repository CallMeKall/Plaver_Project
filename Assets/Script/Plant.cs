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

        if (data != null && data.growthStages.Length > 0)
            sr.sprite = data.growthStages[0];
    }

    void Update()
    {
        if (data == null) return;

        timer += Time.deltaTime;

        if (timer >= data.timePerStage && stage < data.growthStages.Length - 1)
        {
            stage++;
            sr.sprite = data.growthStages[stage];
            timer = 0f;
        }
    }

    // ✅ INI YANG DIPANGGIL TILE SAAT MAU PANEN
    public bool IsFullyGrown()
    {
        return stage >= data.growthStages.Length - 1;
    }
    public void SetPlantData(PlantData newData)
    {
        data = newData;
        stage = 0;
        timer = 0f;

        if (sr == null)
            sr = GetComponent<SpriteRenderer>();

        sr.sprite = data.growthStages[0];
    }
    public void Harvest()
    {
        HarvestManager.Instance.AddHarvest(data, 1);
        Destroy(gameObject);
    }


}
