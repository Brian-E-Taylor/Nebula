using System;
using System.IO;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(NebulaSettings))]
public class NebulaSettings_Inspector : Editor
{
    public VisualTreeAsset inspectorXML;

    private string _lastUsedDirectory;

    void OnEnable()
    {
        _lastUsedDirectory = "";
    }

    public override VisualElement CreateInspectorGUI()
    {
        var myInspector = new VisualElement();

        inspectorXML.CloneTree(myInspector);
        myInspector.Q<Button>("SaveToJSONFileButton").clicked += SaveToJSONFile;
        myInspector.Q<Button>("LoadFromJSONFileButton").clicked += LoadFromJSONFile;

        return myInspector;
    }

    private void SaveToJSONFile()
    {
        var path = EditorUtility.SaveFilePanel(
            "Save Nebula Settings as JSON",
            _lastUsedDirectory,
            "nebulaSettings.json",
            "json"
        );

        if (path.Length != 0)
        {
            // save to json
            var nebulaSettings = target as NebulaSettings;
            var jsonString = EditorJsonUtility.ToJson(nebulaSettings);
            File.WriteAllText(path, jsonString);

            _lastUsedDirectory = Path.GetDirectoryName(path);
        }
    }

    private void LoadFromJSONFile()
    {
        var path = EditorUtility.OpenFilePanel(
            "Load Nebula Settings from JSON",
            _lastUsedDirectory,
            "json");

        if (path.Length != 0)
        {
            // load from json
            var nebulaSettings = target as NebulaSettings;
            var jsonString = File.ReadAllText(path);
            EditorJsonUtility.FromJsonOverwrite(jsonString, nebulaSettings);
            EditorUtility.SetDirty(nebulaSettings);

            _lastUsedDirectory = Path.GetDirectoryName(path);
        }
    }
}
