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
        gold = 0;
    }

    public void AddItem(ItemInfo item)
    {
        if (itemLists.ContainsKey(item.itemType))
        {
            itemLists[item.itemType].Add(item);
            UpdateInventorySlots();
        }
        else
        {
            Debug.Log("타입을 설정하자.");
        }
    }

    public void UpdateInventorySlots()
    {
        foreach (var entry in itemLists)
        {
            ItemType itemType = entry.Key;
            List<ItemInfo> items = entry.Value;
            GameObject panel = GetPanelByItemType(itemType);
            for (int i = 0; i < items.Count; i++)
            {
                Slot slotComponent = panel.transform.GetChild(i).GetComponent<Slot>();
                slotComponent.SetItem(items[i]);
            }
        }
        foreach (Transform child in EquipItemPanel.transform)
        {
            child.GetComponent<Slot>().ClearItem();
        }
        foreach (Transform child in ConsumItemPanel.transform)
        {
            child.GetComponent<Slot>().ClearItem();
        }
        foreach (Transform child in MiscItemPanel.transform)
        {
            child.GetComponent<Slot>().ClearItem();
        }
        foreach (Transform child in QuestItemPanel.transform)
        {
            child.GetComponent<Slot>().ClearItem();
        }

        foreach (var entry in itemLists)
        {
            ItemType itemType = entry.Key;
            List<ItemInfo> items = entry.Value;
            GameObject panel = GetPanelByItemType(itemType);
            for (int i = 0; i < items.Count; i++)
            {
                Slot slotComponent = panel.transform.GetChild(i).GetComponent<Slot>();
                slotComponent.SetItem(items[i]);
            }
        }
    }

    private void InventorySlotSetting()
    {
        foreach (var entry in itemLists)
        {
            ItemType itemType = entry.Key;
            GameObject panel = GetPanelByItemType(itemType);
            for (int i = 0; i < 42; i++)
            {
                GameObject slotObj = Instantiate(ItemSlotPrefab, panel.transform);
                Slot slotComponent = slotObj.GetComponent<Slot>();
                slotComponent.SetSlotIndex(i);
                slotComponent.panel = (RectTransform)panel.transform;
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
        GetGold();
    }

    public int GetGold()
    {
        return gold;
    }
}
