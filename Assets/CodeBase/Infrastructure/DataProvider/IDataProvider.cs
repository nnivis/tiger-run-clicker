
namespace TigerClicker.CodeBase.Infrastructure
{
    public interface IDataProvider
    {
        void Save();

        bool TryLoad();
    }
}
