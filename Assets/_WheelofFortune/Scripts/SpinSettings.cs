using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace WheelofFortune {
    public class SpinSettings : MonoBehaviour
    {
        [SerializeField] private int _numberOfItem = 8;
        [SerializeField] private float _timeRotate;
        [SerializeField] private float _numberCircleRotate;

        private const float _circle = 360.0f;
        private float _angleOfOneItem;

        public RectTransform parent;
        private float _currentTime;

        public AnimationCurve curve;

        private void Start()
        {
            _angleOfOneItem = _circle / _numberOfItem;
        }

        void SetPositionData()
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                parent.GetChild(i).eulerAngles = new Vector3(0, 0, -_circle / _numberOfItem * i);
            }
        }
        public void RotateNow()
        {
            StartCoroutine(RotateWheel());
        }

        IEnumerator RotateWheel()
        {
            float startAngle = transform.eulerAngles.z;
            _currentTime = 0;
            int indexItemRandom = Random.Range(1, _numberOfItem);
            Debug.Log(indexItemRandom);
            float angleWant = (_numberCircleRotate * _circle) + _angleOfOneItem * indexItemRandom - startAngle;

            while (_currentTime<_timeRotate)
            {
                yield return new WaitForEndOfFrame();
                _currentTime += Time.deltaTime;

                float angleCurrent = angleWant * curve.Evaluate(_currentTime / _timeRotate);
                this.transform.eulerAngles = new Vector3(0, 0, angleCurrent);
            }
        }

    }
}