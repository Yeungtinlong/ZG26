using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SupportUtils
{
    public class DeleteSave
    {
        [MenuItem("Danny/Tools/[SAVE] Delete All PlayerPrefs")]
        static void DeletePrefs()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("Danny/Tools/[SAVE] Delete PersistentDataPath")]
        static void DeletePersistentDataPath()
        {
            try
            {
                if (Directory.Exists(Application.persistentDataPath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Application.persistentDataPath);
                    directoryInfo.Delete(true);
                    Debug.Log($"Delete {Application.persistentDataPath} successfully!");
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
    }
}