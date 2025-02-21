using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Image itemImage;
    public Sprite slotImage;
    public Transform panel;
    public RectTransform rectTransform;
        
    private ItemInfo item;
    private int slotIndex;

    private Vector2 initialPos; //�������� �̵� ������ġ

    
    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }
    public bool HasItem()
    {
        return item != null;
    }
    public void SetItem(ItemInfo newItem)
    {
        item = newItem;
        itemImage.sprite = newItem.itemSprite;
        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1f);
    }
    public void ClearItem()
    {
        item = null;
        itemImage.sprite = slotImage;
        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //����ڵ� �������� ���ٸ� ����
        if (item == null)
            return;

        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0.6f);
        initialPos = rectTransform.anchoredPosition;
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        //����ڵ� �������� ���ٸ� ����
        if (item == null)
            return;
        //eventData.delta���콺�� �󸶳� ���������� �˷���
        //panel.lossyScale.x; �г��� �������� ����Ͽ� �巡�׸� ����
        //��, �������� 1���� ���� �������� ���� ���
        //�������� ���ڿ� ������ �ʵ��� ����

        Vector2 localPoint;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(panel as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        { 
            rectTransform.localPosition = localPoint;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //����ڵ� �������� ���ٸ� ����
        if (item == null)
            return;

        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1f);

        RectTransform dropTarget = InventoryManager.Instance.invenDeletArea;

        if (dropTarget != null)
        {
            Slot targetSlot = dropTarget.GetComponent<Slot>();
            if (targetSlot != null)
            {
                Debug.Log("�ȿ� ����");
                if (targetSlot.item == null)
                {
                    targetSlot.SetItem(item);
                    targetSlot.item = item;
                    ClearItem();
                }
                else
                {
                    Debug.Log("������ ����!");
                    ItemInfo tempItemInfo = targetSlot.item;
                    ItemInfo tempItem = targetSlot.item;

                    targetSlot.SetItem(item);
                    targetSlot.item = item;

                    SetItem(tempItemInfo);
                    item = tempItem;
                }
            }
            else
            {
                rectTransform.anchoredPosition = initialPos;
            }
        }
        else
        {//RectTransform���ο� �ִ��� Ȯ���ϴ� �޼ҵ�
         //InventoryManager.Instance.DeleteArea������ �ִٸ�
         //��, �κ��丮 ������ �����ٸ� true, �κ��丮 �ȿ��ִٸ� false
            if (RectTransformUtility.RectangleContainsScreenPoint(InventoryManager.Instance.invenDeletArea, eventData.position))
            {//�κ��丮 ������ �����ٸ� ��ư Ȱ��ȭ
                Debug.Log("������ ����");
                panel.parent.GetChild(1).gameObject.SetActive(true);
            }
            else
            {//�ƴϸ� ���� ��ġ�� ���ư�
                rectTransform.anchoredPosition = initialPos;
            }
        }
    }
}