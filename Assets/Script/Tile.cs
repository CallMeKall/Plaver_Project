using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform plantPoint;
    public GameObject plantPrefab;  //SATU PREFAB UNIVERSAL (berisi script Plant)

    private GameObject currentPlant;
    private bool isPlanted = false;

    public void OnSelect()
    {
        Debug.Log(name + " terseleksi");
    }

    public void OnDeselect()
    {
        Debug.Log(name + " tidak terseleksi");
    }

    public void Interact()
    {
        // JIKA TANAH KOSONG → TANAM
        if (!isPlanted)
        {
            PlantData selectedData = PlantManager.Instance.GetSelectedPlantData();
            if (selectedData == null) return;

            Vector3 spawnPos = plantPoint != null ? plantPoint.position : transform.position;

            currentPlant = Instantiate(plantPrefab, spawnPos, Quaternion.identity, transform);

            // KIRIM DATA TANAMAN KE SCRIPT PLANT
            currentPlant.GetComponent<Plant>().SetPlantData(selectedData);

            isPlanted = true;
            return;
        }

        // JIKA SUDAH ADA TANAMAN → PANEN / CABUT
        Plant plant = currentPlant.GetComponent<Plant>();

        if (plant == null)
        {
            currentPlant = null;
            isPlanted = false;
            return;
        }

        // JIKA SUDAH MATANG → PANEN
        if (plant.IsFullyGrown())
        {
            Debug.Log("Panen berhasil!");

            HarvestManager.Instance.AddHarvest(plant.data, 1);

            Destroy(currentPlant);
            currentPlant = null;
            isPlanted = false;
        }
        // JIKA BELUM MATANG → CABUT
        else
        {
            Debug.Log("Tanaman belum matang, dicabut.");

            Destroy(currentPlant);
            currentPlant = null;
            isPlanted = false;
        }
    }
}
