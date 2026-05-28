using TigerClicker.CodeBase.Domain;

namespace TigerClicker.CodeBase.Domain.LootSystem
{
    public interface ILoot
    {
        int Amount { get; }
        void GenerateLoot(Wallet wallet);

    }
}
