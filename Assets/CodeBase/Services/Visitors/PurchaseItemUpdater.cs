using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Services.GameState;

namespace TigerClicker.CodeBase.Services.Visitors
{
    public class PurchaseItemUpdater : IPurchaseItemVisitors
    {
        private IPersistentData _persistentData;
        public PurchaseItemUpdater(IPersistentData persistentData) => _persistentData = persistentData;
        public void Visit(PurchaseItem purchaseItem)
        {
            _persistentData.PlayerData.UpdatePurchaseItemValue(purchaseItem.PurchaseItemType, purchaseItem.PriceIncreaseCountLimit);
        }
    }
}
