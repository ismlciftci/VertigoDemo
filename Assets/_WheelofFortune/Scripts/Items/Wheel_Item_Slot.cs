using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace WheelofFortune.Items
{
    public class Wheel_Item_Slot : MonoBehaviour
    {
        public ItemData item;
        public Image artworkImage;
        public int amount;
        public TextMeshProUGUI amountText;

        public void SetItem()
        {
            amount = item.amount;
            if (amount > 0)
            {
                amountText.text = "x" + item.amount.ToString();
            }
            if (item.name == "Bomb")
            {
                amountText.text = "";
            }
            artworkImage.sprite = item.artworkImage;
        }

    }
}