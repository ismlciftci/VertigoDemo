using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WheelofFortune.Wheel
{
    [CreateAssetMenu(menuName ="WheelSettings")]
    public class SpinSettings : ScriptableObject
    {
        [SerializeField] private int _angle;
        [SerializeField] private int _angleSpeed = 2;

        [SerializeField] private List<AnimationCurve> _animationCurves;

        public List<AnimationCurve> AnimationCurves {
            get { return _animationCurves; } }
        public int Angle { get; set; }
       

        public int AngleSpeed { get { return _angleSpeed; } }
    }
}