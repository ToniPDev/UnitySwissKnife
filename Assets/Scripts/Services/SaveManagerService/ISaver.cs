using ObjectManagement.Scripts;

namespace Services.SaveManagerService
{
    public interface ISaver
    {
        void Save(GameDataWriter writer);

        void Load(GameDataReader reader);
    }
}