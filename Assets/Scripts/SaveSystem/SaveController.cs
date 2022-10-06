using UnityEngine;

namespace SaveSystem
{
    public class SaveController
    {
        private const string SAVE_KEY = "DataGame";

        public void SaveData(GameData data)
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SAVE_KEY, json);
        }

        public GameData LoadData()
        {
            if (PlayerPrefs.HasKey(SAVE_KEY))
            {
                return JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(SAVE_KEY));
            }
            else
            {
                return new GameData();
            }
        }
    }
}