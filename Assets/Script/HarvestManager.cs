using System.Collections.Generic;
using UnityEngine;

public class HarvestManager : MonoBehaviour
{
    public static HarvestManager Instance;

    // Simpan jumlah panen per PlantData
    private Dictionary<PlantData, int> harvestCounts = new Dictionary<PlantData, int>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Tambah panen
    public void AddHarvest(PlantData data, int amount)
    {
        if (harvestCounts.ContainsKey(data))
            harvestCounts[data] += amount;
        else
            harvestCounts[data] = amount;

        Debug.Log($"[HARVEST] {data.plantName} total: {harvestCounts[data]}");
    }

    // Ambil jumlah panen per jenis tanaman
    public int GetHarvestCount(PlantData data)
    {
        if (harvestCounts.ContainsKey(data))
            return harvestCounts[data];

        return 0;
    }
}
