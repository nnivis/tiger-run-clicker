using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Services.GameState;

namespace TigerClicker.CodeBase.Services.Visitors
{
    public class PurchaseItemCheker : IPurchaseItemVisitors
    {
        private IPersistentData _persistentData;
        public int PriceIncreaseCount { get; private set; }
        public PurchaseItemCheker(IPersistentData persistentData) => _persistentData = persistentData;
        public void Visit(PurchaseItem purchaseItem)
        {
             PriceIncreaseCount = _persistentData.PlayerData.PurchaseItems.ContainsKey(purchaseItem.PurchaseItemType) ? _persistentData.PlayerData.PurchaseItems[purchaseItem.PurchaseItemType] : 0;
        }
    }
}
