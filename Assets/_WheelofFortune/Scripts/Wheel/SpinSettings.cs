using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WheelofFortune.Wheel
{
    [CreateAssetMenu(menuName ="WheelSettings")]
    public class SpinSettings : ScriptableObject
    {
        [SerializeField] private int _angleSpeed = 2;
        public int AngleSpeed { get { return _angleSpeed; } }
       
    }
}