using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance {  get; private set; }
    public GameObject SlotImages;
    public GameObject inventorySlotPrefab;
    private int slotCount = 42; //½½·Ô °¹¼ö
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        CreateInventorySlot();
    }

    void CreateInventorySlot()
    {
        
        for (int i = 0; i< slotCount; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, SlotImages.transform);        
        }
    }
}
