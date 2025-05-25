#if  UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using DB.DBModels;
using SQLite;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class DBEditorUtility
    {
        private static readonly string DB_PATH = Application.persistentDataPath + "/persistent.db";

        [MenuItem("Tools/DB/Create All Tables")]
        public static void CreateAllTables()
        {
            using var db = new SQLiteConnection(DB_PATH);
            db.CreateTable<PlayerDBModel>();
        }
        
        [MenuItem("Tools/DB/Clear Persistent Data")]
        public static void ClearPersistentData()
        {
            

            if (!Directory.Exists(DB_PATH))
            {
                Debug.LogWarning("No persistent data directory found.");
                return;
            }

            try
            {
                Directory.Delete(DB_PATH, true);
                Debug.Log($"🗑 Cleared: {DB_PATH}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to clear persistent data: {ex.Message}");
            }
        }
    }
}
#endif