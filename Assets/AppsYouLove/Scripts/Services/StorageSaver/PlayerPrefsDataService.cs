using System;
using UnityEngine;

namespace AUL.Services
{
    public class PlayerPrefsDataService : IDataService
    {
        private PlayerPrefsSaveContext _saveContext = new();

        public PlayerData LoadPlayerData()
        {
            try
            {
                return JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(_saveContext.GetPrefsKey()));
            }
            catch (Exception e)
            {
                Debug.LogError("Can't Load Player Data. Return new empty data");
                return new PlayerData();
            }
        }

        public bool SavePlayerData(PlayerData playerData)
        {
            try
            {
                PlayerPrefs.SetString(_saveContext.GetPrefsKey(), JsonUtility.ToJson(playerData));
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("Can't Save Player Data");
                return false;
            }
        }
    }

    public struct PlayerPrefsSaveContext
    {
        private const string KEY = "PLAYER_DATA";
        public string GetPrefsKey() => KEY;
    }
}
