using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace trwm.Source.Persistence
{
    public class ModDataStorage
    {
        private static readonly Lazy<ModSaveData> LazySaveData = new Lazy<ModSaveData>(ReadSaveData);
        public static ModSaveData SaveData => LazySaveData.Value;

        private static readonly string ModDataDirectory = Path.Combine(Application.persistentDataPath, ModData.NameShorthand);
        private static readonly string ModDataFilePath = Path.Combine(ModDataDirectory, "data.json");

        public static void Write(ModSaveData saveData)
        {
            try
            {
                Directory.CreateDirectory(ModDataDirectory);
                var serializedData = JsonConvert.SerializeObject(saveData, Formatting.Indented);
                File.WriteAllText(ModDataFilePath, serializedData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error writing mod save data: {e.Message}");
                throw;
            }
        }

        public static void Persist()
        {
            Write(SaveData);
        }

        private static ModSaveData ReadSaveData()
        {
            if (!File.Exists(ModDataFilePath))
            {
                return new ModSaveData();
            }

            try
            {
                var fileDataContents = File.ReadAllText(ModDataFilePath);
                return JsonConvert.DeserializeObject<ModSaveData>(fileDataContents) ?? new ModSaveData();
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error reading mod save data, creating new data: {e.Message}");
                return new ModSaveData();
            }
        }
    }
}