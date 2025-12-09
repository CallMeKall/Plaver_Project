using UnityEngine;
using UnityEngine.InputSystem;

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;

    [Header("Plant Data List")]
    public PlantData[] plantDatas;   //PILIH TANAMAN DARI PLANT DATA
    public int selectedIndex = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            selectedIndex = 0;

        if (Keyboard.current.digit2Key.wasPressedThisFrame && plantDatas.Length > 1)
            selectedIndex = 1;

        if (Keyboard.current.digit3Key.wasPressedThisFrame && plantDatas.Length > 2)
            selectedIndex = 2;
    }

    public PlantData GetSelectedPlantData()
    {
        if (plantDatas.Length == 0) return null;

        selectedIndex = Mathf.Clamp(selectedIndex, 0, plantDatas.Length - 1);
        return plantDatas[selectedIndex];
    }
}
