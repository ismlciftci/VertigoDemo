using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


namespace WheelofFortune.CardLevel
{
    public class Level_Card_UI : MonoBehaviour
    {

        [SerializeField] private Sprite _greenBackground;
        [SerializeField] private Sprite _blueBackground;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _levelText;

        [Header("Positions")]
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _endPoint;
        [SerializeField] private RectTransform _startPoint;
        [SerializeField] private RectTransform _midPoint;
        [SerializeField] private GameObject _currentCard;

        
        public void SetPosition(int _level)
        {
            if (_rectTransform.transform.position.x <= _endPoint.transform.position.x)
            {
                _rectTransform.anchoredPosition = _startPoint.anchoredPosition;
                _levelText.text = (3 + _level).ToString();
                if ((3 + _level) % 5 == 0)
                {
                    _image.sprite = _greenBackground;
                }
                else
                {
                    _image.sprite = _blueBackground;

                }
            }
            _rectTransform.DOAnchorPosX(_rectTransform.anchoredPosition.x -   125, .5f).OnComplete(() =>
            {
                if (_rectTransform.transform.position.x >= _midPoint.transform.position.x -72.5f && _rectTransform.transform.position.x <= _midPoint.transform.position.x + 72.5f)
                {
                    _currentCard.GetComponent<Image>().sprite = _image.sprite;
                    _currentCard.GetComponentInChildren<TextMeshProUGUI>().text = _level.ToString();
                }
            });
        }

    }
}