using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ArmorType
{
    Helmet, Armor, Gloves, Boots, Shield, Ring, Necklace
}
public class Armor : EquipItem
{
    public ArmorType armorType;
    public int defense;
    public int moveSpeed;
    public int resistance;
    public int Hp;
    public int Mp;
    public override void ShowMyInfo(StringBuilder stateBuilder)
    {
        base.ShowMyInfo(stateBuilder);
        stateBuilder.AppendLine($"Armor Type: {armorType}");
        stateBuilder.AppendLine($"Defense: {defense}");
        stateBuilder.AppendLine($"Move Speed: {moveSpeed}");
        stateBuilder.AppendLine($"Resistance: {resistance}");
        stateBuilder.AppendLine($"HP: {Hp}");
        stateBuilder.AppendLine($"MP: {Mp}");
    }
}