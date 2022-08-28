using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace WheelofFortune.Items
{
    [CreateAssetMenu(menuName = "Item")]
    public class ItemData : ScriptableObject
    {
        public new string  name;
        public string type;
        public int amount;
        public Sprite artworkImage;


    }
}