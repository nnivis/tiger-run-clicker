using TigerClicker.CodeBase.Services.GameState;

namespace TigerClicker.CodeBase.Services.Visitors
{
    public interface IPurchaseItemVisitors
    {
        void Visit(PurchaseItem purchaseItem);
    }
}
