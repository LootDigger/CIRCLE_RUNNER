namespace AUL.Services
{

    public interface IDataService
    {
        PlayerData LoadPlayerData();
        bool SavePlayerData(PlayerData playerData);
    }
}
