using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class SpinWheel : MonoBehaviour
{
    public List<AnimationCurve> animationCurves;

    private bool spinning;
    [SerializeField] private int _angle;
    [SerializeField] private int _angleSpeed = 2;

    void Start()
    {
        spinning = false;
        _angle = 45 * Random.Range(10, 50); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !spinning)
        {

            StartCoroutine(SpinTheWheel( _angle, _angleSpeed));
        }
    }

    IEnumerator SpinTheWheel(float endValue, float duration)
    {
        spinning = true;

        float time = 0.0f;
        float startValue = transform.eulerAngles.z;
        

        int animationCurveNumber = Random.Range(0, animationCurves.Count);
        

        while (time < duration)
        {
            float valueToChange = Mathf.Lerp(startValue, endValue, animationCurves[animationCurveNumber].Evaluate(time/duration));
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
        _angle =  45 * Random.Range(10,50);
        spinning = false; 
    }
    public List<string> Items;
    public TextMeshProUGUI itemText;
    public int initialIndex;

    void GiveItem()
    {
        int mainAngle = (int)(_angle) % 360;
        int splitValue = (int)mainAngle / 45;
        int currentIndex = ((initialIndex-splitValue)+8)% 8;
        itemText.text = "" + Items[currentIndex];
    }
}
