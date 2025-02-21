using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemInfo itemData;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(itemData);
            Destroy(gameObject);
        }
    }
    //private void AddItemToInventory()
    //{//사용되지 않는다면 지워도 상관없지만
    //팩토리 패턴으로 구현 한다면 사용 될 수도 있음.
    //    InventoryManager.Instance.AddItem(itemData);
    //}
}
