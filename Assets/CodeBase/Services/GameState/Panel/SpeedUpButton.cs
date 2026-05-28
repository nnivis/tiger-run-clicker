using System;
using UnityEngine;
using UnityEngine.UI;

namespace TigerClicker.CodeBase.Services.GameState
{
    public class SpeedUpButton : MonoBehaviour
    {
        public event Action OnSpeedUpButtonClick;
        [SerializeField] Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnSpeedUpButtonClicked);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnSpeedUpButtonClicked);
        }

        private void OnSpeedUpButtonClicked()
        {
            if (OnSpeedUpButtonClick != null)
                OnSpeedUpButtonClick();
        }
    }
}
