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

    private Vector2 initialPos; //아이템의 이동 시작위치

    
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
        //방어코드 아이템이 없다면 리턴
        if (item == null)
            return;

        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0.6f);
        initialPos = rectTransform.anchoredPosition;
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        //방어코드 아이템이 없다면 리턴
        if (item == null)
            return;
        //eventData.delta마우스가 얼마나 움직였는지 알려줌
        //panel.lossyScale.x; 패널의 스케일을 고려하여 드래그를 조정
        //즉, 스케일이 1보다 작은 아이템이 있을 경우
        //아이템이 부자연 스럽지 않도록 조정

        Vector2 localPoint;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(panel as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        { 
            rectTransform.localPosition = localPoint;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //방어코드 아이템이 없다면 리턴
        if (item == null)
            return;

        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1f);

        RectTransform dropTarget = InventoryManager.Instance.invenDeletArea;

        if (dropTarget != null)
        {
            Slot targetSlot = dropTarget.GetComponent<Slot>();
            if (targetSlot != null)
            {
                Debug.Log("안에 있음");
                if (targetSlot.item == null)
                {
                    targetSlot.SetItem(item);
                    targetSlot.item = item;
                    ClearItem();
                }
                else
                {
                    Debug.Log("아이템 스왑!");
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
        {//RectTransform내부에 있는지 확인하는 메소드
         //InventoryManager.Instance.DeleteArea영역에 있다면
         //즉, 인벤토리 밖으로 나갔다면 true, 인벤토리 안에있다면 false
            if (RectTransformUtility.RectangleContainsScreenPoint(InventoryManager.Instance.invenDeletArea, eventData.position))
            {//인벤토리 밖으로 나갔다면 버튼 활성화
                Debug.Log("밖으로 나감");
                panel.parent.GetChild(1).gameObject.SetActive(true);
            }
            else
            {//아니면 시작 위치로 돌아감
                rectTransform.anchoredPosition = initialPos;
            }
        }
    }
}