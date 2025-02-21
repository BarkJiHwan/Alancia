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
    //{//������ �ʴ´ٸ� ������ ���������
    //���丮 �������� ���� �Ѵٸ� ��� �� ���� ����.
    //    InventoryManager.Instance.AddItem(itemData);
    //}
}
