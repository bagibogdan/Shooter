using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace SaveSystem
{
    public class SaveController
    {
        private const string PATH = "/gameData.data";

        public void SaveData(GameData gameData)
        {
            var dataStream = new FileStream(Application.persistentDataPath + PATH, FileMode.OpenOrCreate);
            var converter = new BinaryFormatter();
            converter.Serialize(dataStream, gameData);
            dataStream.Close();
        }

        public GameData LoadData()
        {
            if (File.Exists(Application.persistentDataPath + PATH))
            {
                var dataStream = new FileStream(Application.persistentDataPath + PATH, FileMode.Open);
                var converter = new BinaryFormatter();
                var gameData = converter.Deserialize(dataStream) as GameData;
                dataStream.Close();
                return gameData;
            }
            else
            {
                return new GameData();
            }
        }
    }
}