using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public static class ScriptsCollector {
    [MenuItem("Tools/Export All Scripts Text")]
    public static void ExportAllScriptsText() {
        string scriptsFolder = "Assets/Scripts"; // Путь к папке со скриптами
        string outputFile = "Assets/Editor/AllScriptsCombined.txt"; // Выходной файл

        if (!Directory.Exists(scriptsFolder)) {
            Debug.LogError($"Scripts folder not found: {scriptsFolder}");
            return;
        }

        StringBuilder combinedText = new StringBuilder();

        // Получаем все .cs файлы рекурсивно
        string[] scriptFiles = Directory.GetFiles(scriptsFolder, "*.cs", SearchOption.AllDirectories);

        foreach (string filePath in scriptFiles) {
            string fileName = Path.GetFileName(filePath);
            string fileContent = File.ReadAllText(filePath);

            combinedText.AppendLine($"// ===== {fileName} =====");
            combinedText.AppendLine(fileContent);
            combinedText.AppendLine("\n\n");
        }

        File.WriteAllText(outputFile, combinedText.ToString());
        EditorUtility.RevealInFinder(outputFile);

        Debug.Log($"Successfully exported {scriptFiles.Length} scripts to {outputFile}");
    }
}
