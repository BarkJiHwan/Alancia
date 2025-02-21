using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Image itemImage;
    public Sprite slotImage;
    public RectTransform panel;
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
        // ����ڵ� �������� ���ٸ� ����
        if (item == null)
            return;

        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0.6f);
        initialPos = rectTransform.anchoredPosition;
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        // ����ڵ� �������� ���ٸ� ����
        if (item == null)
            return;

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(panel as RectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        // ����ڵ� �������� ���ٸ� ����
        if (item == null)
            return;

        itemImage.color = new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 1f);

        RectTransform dropTarget = eventData.pointerEnter?.GetComponent<RectTransform>();
        Debug.Log(dropTarget);
        if (dropTarget != null)
        {
            Slot targetSlot = dropTarget.GetComponent<Slot>();
            HandleSlotSwap(targetSlot);
        }
        else
        {
            HandleOutsideInventory(eventData);
        }

        // �׸��� ���̾ƿ� �׷��� ������Ʈ�Ͽ� ������ ����
        GetComponentInParent<GridLayoutGroup>().enabled = false;
        GetComponentInParent<GridLayoutGroup>().enabled = true;

        // ���� �ʱ�ȭ �� �κ��丮 ������Ʈ
        InventoryManager.Instance.UpdateInventorySlots();
    }

    private void HandleSlotSwap(Slot targetSlot)
    {
        if (targetSlot != null)
        {
            Debug.Log("�ȿ� ����");

            if (targetSlot.item == null)
            {
                targetSlot.SetItem(item);
                ClearItem();
            }
            else
            {
                SwapItems(targetSlot);
            }
        }
        else
        {
            rectTransform.anchoredPosition = initialPos;
        }
    }

    private void SwapItems(Slot targetSlot)
    {
        Debug.Log("������ ����!");

        ItemInfo tempItemInfo = targetSlot.item;

        targetSlot.SetItem(item);
        SetItem(tempItemInfo);
    }

    private void HandleOutsideInventory(PointerEventData eventData)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(InventoryManager.Instance.invenDeletArea, eventData.position))
        {
            Debug.Log("������ ����");
            panel.parent.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            rectTransform.anchoredPosition = initialPos;
        }
    }
}
