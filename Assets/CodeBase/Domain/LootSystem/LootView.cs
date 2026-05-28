using TMPro;
using UnityEngine;

namespace TigerClicker.CodeBase.Domain.LootSystem
{
    public class LootView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _lootText;
        private LootViewAnimation _animation;
        private void PlayAnimation()
        {
            _animation.StartAnimation();
        }
        private void DestroyLootView()
        {
            Destroy(gameObject);
        }
        public void Startwork()
        {
            _animation = new LootViewAnimation(_lootText);
            _animation.OnAnimationComplete += DestroyLootView;
            PlayAnimation();
        }

        public void SetLootText(int value)
        {
            if (_lootText != null)
            {
                _lootText.text = value.ToString();
            }
        }

    }
}
