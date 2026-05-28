using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace TigerClicker.CodeBase.Domain.LootSystem
{
    public class LootViewAnimation
    {
        public event Action OnAnimationComplete;
        private const float MoveDistancePercentage = 2f;
        private const float Duration = 0.5f;
        private const float TimeInterval = 0.1f;

        private TMP_Text _lootText;

        public LootViewAnimation(TMP_Text lootText)
        {
            _lootText = lootText;
        }

        public void StartAnimation()
        {
            Color startColor = _lootText.color;
            startColor.a = 0f;
            _lootText.color = startColor;

            Sequence sequence = DOTween.Sequence();

            sequence.Append(_lootText.DOFade(1f, Duration));
            sequence.Join(_lootText.transform.DOMoveY(_lootText.transform.position.y + MoveDistancePercentage, Duration).SetEase(Ease.Linear)); 
            sequence.AppendInterval(TimeInterval); 
            sequence.Join(_lootText.DOFade(0f, Duration)); 

            sequence.OnComplete(() => OnAnimationComplete?.Invoke()); 
        }
    }
}
