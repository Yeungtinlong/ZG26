using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class HttpHosting
{
    [MenuItem("Tools/Danny/Start Local Cdn")]
    static void OpenHosting()
    {
        string hostingPath =
            $"{Environment.CurrentDirectory}\\ServerData\\local\\{EditorUserBuildSettings.activeBuildTarget}";

        if (!Directory.Exists(hostingPath))
        {
            Debug.LogError($"Target hosting path {hostingPath} is not exists.");
            return;
        }

        Process process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            WindowStyle = ProcessWindowStyle.Normal,
            FileName = $"{Application.dataPath}/Addons/DannyHttpHosting/Editor/http-utils.exe",
            Arguments = $"\"-dir\" \"{hostingPath}\" \"-ip\" \"127.0.0.1\" \"-port\" \"62233\"",
            // Arguments = "\"-dir\" \"E:\\UnityDecomplieProject\\Worldbox v0.22.6-549\\ServerData\\local\\Android\" \"-ip\" \"127.0.0.1\" \"-port\" \"62233\"",
        };

        Debug.Log($"hosting on {hostingPath}");
        
        process.Start();
    }
}