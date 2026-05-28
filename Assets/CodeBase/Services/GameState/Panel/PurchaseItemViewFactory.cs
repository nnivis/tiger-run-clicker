using UnityEngine;

namespace TigerClicker.CodeBase.Services.GameState
{
    [CreateAssetMenu(fileName = "PurchaseItemViewFactory", menuName = "PurchaseItem/PurchaseItemViewFactory", order = 3)]
    public class PurchaseItemViewFactory : ScriptableObject
    {
        [SerializeField] private PurchaseItemView _purchaseItemPrefab;

        public PurchaseItemView Get(PurchaseItem purchaseItemView, Transform parent)
        {
            PurchaseItemView instance = Instantiate(_purchaseItemPrefab, parent);
            instance.Initialize(purchaseItemView);
            return instance;
        }
    }
}
