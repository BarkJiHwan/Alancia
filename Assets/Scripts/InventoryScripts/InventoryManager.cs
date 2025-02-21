using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    private Dictionary<ItemType, List<ItemInfo>> itemLists;
    private int gold;
    public RectTransform invenDeletArea;
    public GameObject EquipItemPanel;
    public GameObject ConsumItemPanel;
    public GameObject MiscItemPanel;
    public GameObject QuestItemPanel;
    public GameObject ItemSlotPrefab;

    private Dictionary<ItemType, List<ItemInfo>> capturedSlots;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
        InitializeInventory();
        InventorySlotSetting();
    }
    private void InitializeInventory()
    {
        itemLists = new Dictionary<ItemType, List<ItemInfo>>()
        {
            {ItemType.EquipItem, new List<ItemInfo>()},
            {ItemType.ConsumItem, new List<ItemInfo>()},
            {ItemType.MiscItem, new List<ItemInfo>()},
            {ItemType.QuestItem, new List<ItemInfo>()}
        };
        capturedSlots = new Dictionary<ItemType, List<ItemInfo>>();
        gold = 0;
    }
    public void AddItem(ItemInfo item)
    {
        if (itemLists.ContainsKey(item.itemType))
        {
            Debug.Log(item.itemType);
            itemLists[item.itemType].Add(item);
        }
        else
        {
            Debug.Log("타입을 설정하자.");
        }
        //InventorySlotUpdate(item);
        InventorySlotUpdate();
    }
    public void InventorySlotUpdate()
    {
        //ClearInventorySlots();        
        UpdateSlots(EquipItemPanel, itemLists[ItemType.EquipItem]);
        UpdateSlots(ConsumItemPanel, itemLists[ItemType.ConsumItem]);
        UpdateSlots(MiscItemPanel, itemLists[ItemType.MiscItem]);
        UpdateSlots(QuestItemPanel, itemLists[ItemType.QuestItem]);
    }
    //private void ClearInventorySlots()
    //{
    //    ClearSlots(EquipItemPanel);
    //    ClearSlots(ConsumItemPanel);
    //    ClearSlots(MiscItemPanel);
    //    ClearSlots(QuestItemPanel);
    //}
    //private void ClearSlots(GameObject panel)
    //{
    //    foreach (Transform child in panel.transform)
    //    {
    //        Slot slotGetComponent = child.GetComponent<Slot>();
    //        if (slotGetComponent != null)
    //        {
    //            Destroy(child.gameObject);
    //        }
    //    }
    //}
    private void UpdateSlots(GameObject panel, List<ItemInfo> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            Transform slotTransform = panel.transform.GetChild(i);
            Slot slotComponent = slotTransform.GetComponent<Slot>();
            slotComponent.SetItem(items[i]);
        }
    }
    private void InventorySlotSetting()
    {
        foreach (var entry in itemLists)
        {
            ItemType itemType = entry.Key;
            List<ItemInfo> items = entry.Value;
            GameObject panel = GetPanelByItemType((itemType));
            GameObject slotObj = Instantiate(ItemSlotPrefab, panel.transform);
            for (int i = 0; i < 42; i++)
            {
                Slot slotComponent = slotObj.GetComponent<Slot>();                
                slotComponent.SetSlotIndex(i);
            }
            
        }
    }
    private GameObject GetPanelByItemType(ItemType itemtype)
    {
        switch (itemtype)
        {
            case ItemType.EquipItem:
                return EquipItemPanel;
            case ItemType.ConsumItem:
                return ConsumItemPanel;
            case ItemType.MiscItem:
                return MiscItemPanel;
            case ItemType.QuestItem:
                return QuestItemPanel;
            default:
                return null;
        }
    }

    
    public void AddGold(int money)
    {
        gold += money;
        int galod = GetGold();
    }
    public int GetGold()
    {
        return gold;
    }
    public void GoldUIUpdate()
    {
        //골드 itme 골드는 ItemInfo에 추가해야함
    }
}
