using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum ConsumTyep
{ HealthPotion, ManaPotion, Elixir }
[CreateAssetMenu(fileName = "New ConsumItem", menuName = "Items/2.Consum", order = 2)]
public class ConsumItem : ItemInfo
{
    public int hpRestored;
    public int manaRestores;
    public override void ShowMyInfo(StringBuilder stateBuilder)
    {
        stateBuilder.AppendLine($"HP Restored: {hpRestored}");
        stateBuilder.AppendLine($"Mana Restored: {manaRestores}");
    }
}
