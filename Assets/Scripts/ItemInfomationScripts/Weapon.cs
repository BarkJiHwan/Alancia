using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum WeaponType
{
    Sword, Bow, Staff
}
public enum HandType
{
    Onehanded, Twohanded
}
public class Weapon : EquipItem
{
    public WeaponType weaponType;
    public HandType handType;
    public int range;
    public int damage;

    public override void ShowMyInfo(StringBuilder stateBuilder)
    {
        base.ShowMyInfo(stateBuilder);
        stateBuilder.AppendLine($"WeaponType: {weaponType}");
        stateBuilder.AppendLine($"HandType: {handType}");
        stateBuilder.AppendLine($"Range: {range}");
        stateBuilder.AppendLine($"Damage: {damage}");
    }
}
