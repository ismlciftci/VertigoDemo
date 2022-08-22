using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace WheelofFortune.Wheel
{
    public class SpinWheel : MonoBehaviour
    {
        [SerializeField] private SpinSettings _spinSettings;

        [SerializeField] private Button uiSpinButton;

        private bool spinning;
       

        public List<string> Items;
        public TextMeshProUGUI itemText;
        public int initialIndex;

        void Start()
        {
            uiSpinButton.onClick.AddListener(() =>
            {
                if (!spinning)
                {
                    StartCoroutine((SpinTheWheel(_spinSettings.Angle, _spinSettings.AngleSpeed)));
                }

            });

            spinning = false;
            _spinSettings.Angle = 45 * Random.Range(10, 50);
            
        }

        void Update()
        {
           
            if (Input.GetKeyDown(KeyCode.Space) && !spinning)
            {

                StartCoroutine(SpinTheWheel(_spinSettings.Angle, _spinSettings.AngleSpeed));
            }
        }

        IEnumerator SpinTheWheel(float endValue, float duration)
        {
            spinning = true;

            float time = 0.0f;
            float startValue = transform.eulerAngles.z;


            int animationCurveNumber = Random.Range(0, _spinSettings.AnimationCurves.Count);


            while (time < duration)
            {
                float valueToChange = Mathf.Lerp(startValue, endValue, _spinSettings.AnimationCurves[animationCurveNumber].Evaluate(time / duration));
                transform.eulerAngles = new Vector3(0, 0, valueToChange);

                time += Time.deltaTime;
                yield return null;
                //float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
                //transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
                //timer += Time.deltaTime;
                //yield return 0;
            }

            transform.eulerAngles = new Vector3(0.0f, 0.0f, endValue);
            GiveItem();
            _spinSettings.Angle = 45 * Random.Range(10, 50);
            spinning = false;
        }
       
        void GiveItem()
        {
            int mainAngle = (int)(_spinSettings.Angle) % 360;
            int splitValue = (int)mainAngle / 45;
            int currentIndex = ((initialIndex - splitValue) + 8) % 8;
            itemText.text = "" + Items[currentIndex];
        }
    }
}