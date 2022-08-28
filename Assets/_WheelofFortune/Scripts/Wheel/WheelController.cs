using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using WheelofFortune.Items;
using WheelofFortune.Inventory;
using WheelofFortune.CardLevel;
using DG.Tweening;

namespace WheelofFortune.Wheel
{
    public class WheelController : MonoBehaviour
    {
        [Header("Lists")]
        public List<ItemData> scItems;
        public List<GameObject> wheelSlots;
        public List<GameObject> levelCards;

        [Header("Scripts")]
        [SerializeField] private InventoryUI_Controller inventoryUI;
        [SerializeField] private Inventory_Settings playerInventory;
        [SerializeField] private SpinSettings _spinSettings;

        [Space]

        [Header("Images")]
        [SerializeField] private Image _wheelImage;
        [SerializeField] private Sprite _wheelSilver;
        [SerializeField] private Sprite _wheelBronze;
        [SerializeField] private Sprite _wheelGold;
        [SerializeField] private Image _arrowImage;
        [SerializeField] private Sprite _arrowSilver;
        [SerializeField] private Sprite _arrowGold;
        [SerializeField] private Sprite _arrowBronze;
        [SerializeField] private CanvasGroup _wheelPanelImage;

        [Space]

        [Header("Buttons")]
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _collectRewardsButton;
        [SerializeField] private Button _goBackButton;
        [SerializeField] private Button _giveUpButton;
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _uiSpinButton;
        [SerializeField] private Button _playAgainButton;

        [Space]

        [Header("Other")]
        [SerializeField] private int _bombRate;
        [SerializeField] private GameObject _exitPanel;
        [SerializeField] private GameObject _bombPanel;
        [SerializeField] private GameObject _endPanel;
        [SerializeField] private TextMeshProUGUI _endPanelText;
        [SerializeField] private GameObject _rewardsTransportIcon;
        [SerializeField] private Transform _inventoryPosition;
        [SerializeField] private GameObject _startRewardsPosition;
        [SerializeField] private AudioSource _spinSound;


        private int _angle;
        private bool _isSpin;
        private int _initialIndex;
        private int _currentLevel = 1;
        private int _randomNumber;

        void Start()
        {
            CleanInventory();

            inventoryUI.UpdateUI();

            StartCoroutine(ChangeSlots());

            _isSpin = false;
            
                _uiSpinButton.onClick.AddListener(()=> {
                    Spin();
                });
            
            _exitButton.onClick.AddListener(OpenExitPanel);
            _collectRewardsButton.onClick.AddListener(CollectRewards);
            _goBackButton.onClick.AddListener(CloseExitPanel);
            _giveUpButton.onClick.AddListener(GiveUp);
            _reviveButton.onClick.AddListener(Revive);
            _playAgainButton.onClick.AddListener(PlayAgain);
        }

        #region Change wheel image and slots value
        IEnumerator ChangeSlots()
        {
            _randomNumber = Random.Range(0, 101);

            if (_randomNumber < _bombRate)
            {
                _angle = -1 * 45 * 32;
            }
            else
            {
                _angle = -1 * 45 * Random.Range(33, 40);
            }
            if (_currentLevel % 30 == 0)
            {
                _wheelPanelImage.DOFade(0, .5f).OnComplete(() =>
                {
                    _wheelImage.sprite = _wheelGold;
                    _arrowImage.sprite = _arrowGold;
                    _wheelPanelImage.DOFade(1, 1);
                });
            }
            else if (_currentLevel % 5 == 0)
            {
                _wheelPanelImage.DOFade(0, 1).OnComplete(() =>
                {
                    _wheelImage.sprite = _wheelSilver;
                    _arrowImage.sprite = _arrowSilver;
                    _wheelPanelImage.DOFade(1, 1);
                });
            }
            else if(_currentLevel>1)
            {
                _wheelPanelImage.DOFade(0, .5f).OnComplete(() =>
                {
                    _wheelImage.sprite = _wheelBronze;
                    _arrowImage.sprite = _arrowBronze;
                    _wheelPanelImage.DOFade(1, 1);
                });
            }
            yield return new WaitForSeconds(.5f);

            for (int i = 0; i < wheelSlots.Count; i++)
            {
                int j = Random.Range(1, scItems.Count);
                if (_currentLevel % 30 == 0)
                {


                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().item = scItems[j];
                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().item.amount += _currentLevel / 10;
                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().SetItem();
                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().item.amount -= _currentLevel / 10;

                }
                else if (_currentLevel % 5 == 0 || _currentLevel == 1)
                {

                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().item = scItems[j];
                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().item.amount += _currentLevel / 10;
                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().SetItem();
                    wheelSlots[i].GetComponent<Wheel_Item_Slot>().item.amount -= _currentLevel / 10;

                }
                else
                {
                    if (i == 0)
                    {
                        wheelSlots[i].GetComponent<Wheel_Item_Slot>().item = scItems[0];
                        wheelSlots[i].GetComponent<Wheel_Item_Slot>().SetItem();
                    }
                    else
                    {
                        wheelSlots[i].GetComponent<Wheel_Item_Slot>().item = scItems[j];
                        wheelSlots[i].GetComponent<Wheel_Item_Slot>().item.amount += _currentLevel / 10;
                        wheelSlots[i].GetComponent<Wheel_Item_Slot>().SetItem();
                        wheelSlots[i].GetComponent<Wheel_Item_Slot>().item.amount -= _currentLevel / 10;
                    }
                }
                
            }
            yield return new WaitForSeconds(.5f);

            _uiSpinButton.interactable = true;
        }
        #endregion

     
        private void OnValidate()
        {
            if (!_isSpin)
            {
                _uiSpinButton.onClick.AddListener(Spin);
            }
            _exitButton.onClick.AddListener(OpenExitPanel);
            _collectRewardsButton.onClick.AddListener(CollectRewards);
            _goBackButton.onClick.AddListener(CloseExitPanel);
            _giveUpButton.onClick.AddListener(GiveUp);
            _reviveButton.onClick.AddListener(Revive);
            _playAgainButton.onClick.AddListener(PlayAgain);
        }

        #region Spin the wheel
        private void Spin()
        {
            if (!_isSpin)
            {
                _uiSpinButton.interactable = false;
                StartCoroutine((SpinTheWheel(_angle, _spinSettings.AngleSpeed)));
                
            }
        }
        IEnumerator SpinTheWheel(float endValue, float duration)
        {
            _spinSound.Play();
            _isSpin = true;
            
            transform.DORotate(new Vector3(0, 0, endValue), duration, RotateMode.FastBeyond360).SetEase(Ease.OutBack,.5f).OnComplete(() =>
            {
                _spinSound.Stop();
                transform.eulerAngles = new Vector3(0.0f, 0.0f, endValue);
                GiveItem();

            });
            yield return new WaitForSeconds(duration + 2);
            if (_bombRate < 60)
            {
                _bombRate += 2;
            }

            _currentLevel++;
            for (int i = 0; i < levelCards.Count; i++)
            {
                levelCards[i].GetComponent<Level_Card_UI>().SetPosition(_currentLevel);
            }

            StartCoroutine(ChangeSlots());
            _isSpin = false;
            
        }
        #endregion
        
        void GiveItem()
        {
            int mainAngle = (int)(_angle) % 360;
            int splitValue = (int)mainAngle / 45;
            int currentIndex = ((_initialIndex - splitValue) + 8) % 8;
            if (wheelSlots[currentIndex].GetComponent<Wheel_Item_Slot>().item.name == "Bomb")
            {
                OpenBombPanel();
            }
            else
            {
                wheelSlots[currentIndex].GetComponent<Wheel_Item_Slot>().item.amount += _currentLevel / 10;
                playerInventory.AddItem(wheelSlots[currentIndex].GetComponent<Wheel_Item_Slot>().item);
                wheelSlots[currentIndex].GetComponent<Wheel_Item_Slot>().item.amount -= _currentLevel / 10;

                _rewardsTransportIcon.GetComponent<Image>().sprite = wheelSlots[currentIndex].GetComponent<Wheel_Item_Slot>().item.artworkImage;
                _rewardsTransportIcon.transform.position = _startRewardsPosition.transform.position;

                _rewardsTransportIcon.transform.DOMove(
                    (new Vector2(_rewardsTransportIcon.transform.position.x + Random.Range(50, 100), _rewardsTransportIcon.transform.position.y + Random.Range(50, 100))), 1f);
                _rewardsTransportIcon.transform.DOScale(Vector2.one, 1f).OnComplete(() =>
                {
                    _rewardsTransportIcon.transform.DOMove(_inventoryPosition.position, 1).OnComplete(() =>
                    {
                        _rewardsTransportIcon.transform.DOScale(Vector2.zero, .2f).OnComplete(() =>
                        {
                            inventoryUI.UpdateUI();
                        });
                    });
                });
            }

        }
        private void CleanInventory()
        {
            foreach (var slot in playerInventory.inventorySlots)
            {
                slot._item = null;
                slot._itemAmount = 0;
            }
        }
        private void OpenExitPanel()
        {
            _exitPanel.SetActive(true);
        }
        private void CloseExitPanel()
        {
            _exitPanel.SetActive(false);
        }
        private void CollectRewards()
        {
            OpenEndPanel("");
            _exitPanel.SetActive(false);
        }

        private void OpenBombPanel()
        {
            _bombPanel.SetActive(true);
        }
        private void GiveUp()
        {
            OpenEndPanel("Rewards were lost");
            _bombPanel.SetActive(false);
        }
        private void Revive()
        {
            _bombPanel.SetActive(false);
        }
        private void OpenEndPanel(string message)
        {
            if (message !="")
            {
                _endPanelText.text =message;
            }
            _endPanel.SetActive(true);
        }

        private void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}