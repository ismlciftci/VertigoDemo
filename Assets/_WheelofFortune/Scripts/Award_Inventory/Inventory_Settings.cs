using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WheelofFortune.Items;

namespace WheelofFortune.Inventory
{
    [CreateAssetMenu(menuName = "Scriptable/AwardInventory")]
    public class Inventory_Settings : ScriptableObject
    {
        public List<Slot> inventorySlots = new List<Slot>();
        public bool AddItem(ItemData _item)
        {
            foreach (Slot slot in inventorySlots)
            {
                if (slot._item == _item)
                {
                    slot._itemAmount += _item.amount;
                    return true;
                    
                }
                else if (slot._itemAmount == 0)
                {
                    slot.AddItemToSlot(_item);
                   
                    return true;
                }
            }
            return false;
        }

    }
    [System.Serializable]
    public class Slot
    {
        public int _itemAmount;
        public ItemData _item;

        public void AddItemToSlot(ItemData _item)
        {
            this._item = _item;
            _itemAmount = _item.amount;
        }
    }
}