using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelofFortune.Inventory;
using DG.Tweening;
namespace WheelofFortune.Inventory
{
    public class InventoryUI_Controller : MonoBehaviour
    {
        public List<Inventory_Slot_UI> uiList = new List<Inventory_Slot_UI>();
        public Inventory_Settings userInventory;
        public void UpdateUI()
        {
            for (int i = 0; i < uiList.Count; i++)
            {
                if (userInventory.inventorySlots[i]._itemAmount > 0)
                {
                    uiList[i].gameObject.SetActive(true);
                    uiList[i].gameObject.transform.DOScale(Vector2.one, .5f);

                    uiList[i].itemImage.sprite = userInventory.inventorySlots[i]._item.artworkImage;

                    uiList[i].itemAmountText.text = userInventory.inventorySlots[i]._itemAmount.ToString();

                }
                else
                {
                    uiList[i].gameObject.transform.localScale = Vector2.zero;
                    uiList[i].gameObject.SetActive(false);
                }
            }
        }

    }
}