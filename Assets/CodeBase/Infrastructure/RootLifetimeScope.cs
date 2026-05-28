using UnityEngine;
using VContainer;
using VContainer.Unity;
using TigerClicker.CodeBase.Data;
using TigerClicker.CodeBase.Domain;
using TigerClicker.CodeBase.Domain.LootSystem;
using TigerClicker.CodeBase.Services.GameState;
using TigerClicker.CodeBase.Services.StateMachine;

namespace TigerClicker.CodeBase.Infrastructure
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField] private PurchaseItemContent _purchaseItemContent;

        protected override void Configure(IContainerBuilder builder)
        {
            // Data
            builder.Register<PersistentData>(Lifetime.Singleton).As<IPersistentData>();
            builder.Register<DataLocalProvider>(Lifetime.Singleton).As<IDataProvider>();

            // Domain
            builder.Register<Wallet>(Lifetime.Singleton);

            // Services
            builder.Register<PriceUpgrader>(Lifetime.Singleton);
            builder.Register<PurchaseService>(Lifetime.Singleton);
            builder.RegisterInstance(_purchaseItemContent);

            // MonoBehaviours
            builder.RegisterComponentInHierarchy<BootstrapComponents>();
            builder.RegisterComponentInHierarchy<GameState>();
            builder.RegisterComponentInHierarchy<LootHandler>();
        }
    }
}
