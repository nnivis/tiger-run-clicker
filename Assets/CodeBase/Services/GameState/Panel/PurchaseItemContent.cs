using System.Collections.Generic;
using UnityEngine;

namespace TigerClicker.CodeBase.Services.GameState
{
    [CreateAssetMenu(fileName = "PurchaseItemContent", menuName = "PurchaseItem/PurchaseItemContent", order = 2)]
    public class PurchaseItemContent : ScriptableObject
    {
        [SerializeField] private List<PurchaseItem> _purchaseItems;
        public IEnumerable<PurchaseItem> PurchaseItems => _purchaseItems;
        public PurchaseItem GetItemByType(PurchaseItemType type)
        {
            return _purchaseItems.Find(item => item.PurchaseItemType == type);
        }

    }
}
