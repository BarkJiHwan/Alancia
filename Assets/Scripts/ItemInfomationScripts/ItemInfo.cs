using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ItemType
{
    EquipItem,
    ConsumItem,
    MiscItem,
    QuestItem
}
public enum ItemGrade
{
    Normal,
    Epic,
    Legendary,
    Mythical
}
public enum JobType
{
    AllClass,
    Warrior,
    Archer,
    Mage
}
[System.Serializable]
public class ItemInfo : ScriptableObject
{
    public GameObject itemPrefab;
    public ItemType itemType;
    public ItemGrade itemGrade;
    //public string name;
    public string itemDescription;
    public int price;
    public int count;
    public int requiredLevel;
    public JobType requiredClass;
    public int requiredStrength;
    public int requiredDexterity;
    public int requiredIntelligence;
    public int requiredLuck;
    public float dropRate;

    public Sprite itemSprite;
    public virtual void ShowMyInfo(StringBuilder stateBuilder)
    {
        //stateBuilder.AppendLine($"Name: {name}");
        //stateBuilder.AppendLine($"Type: {itemType}");
        //stateBuilder.AppendLine($"Description: {itemDescription}");
        //stateBuilder.AppendLine($"Price: {price}");
    }
    public void ItemRequired(StringBuilder stateBuilder)
    {
        stateBuilder.AppendLine($"Required");
        stateBuilder.AppendLine($"Level: {requiredLevel}");
        stateBuilder.AppendLine($"Class: {requiredClass}");
        stateBuilder.Append($"Str: {requiredStrength} \t");
        stateBuilder.Append($"Dex: {requiredDexterity} \t");
        stateBuilder.Append($"Int: {requiredIntelligence} \t");
        stateBuilder.Append($"Luck: {requiredLuck} \t");
    }
}

