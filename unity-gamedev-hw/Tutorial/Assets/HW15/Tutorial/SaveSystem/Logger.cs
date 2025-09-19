using System;
using System.IO;
using UnityEngine;

public sealed class Logger {
    private string _path = Application.persistentDataPath + "/logs.txt";

    public Logger() { }

    public void Log(string message) {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Console.WriteLine(message);
        else
            WriteLogToFile(message);
    }

    private void WriteLogToFile(string message) {
#if UNITY_EDITOR
        Debug.Log(message);
#endif

        using (var writer = new StreamWriter(_path, true)) {
            writer.WriteLine(message);
            writer.Close();
        }
    }
}


