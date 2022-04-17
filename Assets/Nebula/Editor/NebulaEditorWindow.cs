using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class NebulaEditorWindow : EditorWindow
{
    public VisualTreeAsset nebulaUXML;
    public StyleSheet nebulaUSS;
    public VisualTreeAsset starInspectorUXML;

    private NebulaSettings _nebulaSettings;
    private SerializedObject _serializedNebulaSettings;

    [MenuItem("Fault In Our Stars/Nebula")]
    public static void ShowWindow()
    {
        NebulaEditorWindow wnd = GetWindow<NebulaEditorWindow>();
        wnd.titleContent = new GUIContent("Nebula");
    }

    public void CreateGUI()
    {
        _nebulaSettings = GameObject.Find("NebulaSettings").GetComponent<NebulaSettings>();
        if (_nebulaSettings == null)
        {
            var gameObj = new GameObject("NebulaSettings");
            gameObj.AddComponent<NebulaSettings>();
            _nebulaSettings = gameObj.GetComponent<NebulaSettings>();
        }
        _serializedNebulaSettings = new SerializedObject(_nebulaSettings);

        // Import UXML
        nebulaUXML.CloneTree(rootVisualElement);

        // Import USS
        rootVisualElement.styleSheets.Add(nebulaUSS);

        var arraySizeIntegerField = new IntegerField
        {
            bindingPath = $"{nameof(_nebulaSettings.starPresets)}.Array.size",
            style = { display = DisplayStyle.None }
        };
        arraySizeIntegerField.RegisterValueChangedCallback(evt => PrepareScrollView());
        rootVisualElement.Add(arraySizeIntegerField);

        PrepareScrollView();
        PrepareCreateNewPresetButton();

        rootVisualElement.Bind(_serializedNebulaSettings);
    }

    private void PrepareScrollView()
    {
        var scrollView = rootVisualElement.Q<ScrollView>("StarPresetsScrollView");
        scrollView.Unbind();
        scrollView.Clear();

        for (var i = 0; i < _nebulaSettings.starPresets.Count; i++)
        {
            var element = starInspectorUXML.CloneTree();

            var starNameTextField = element.Q<TextField>("StarName");
            starNameTextField.bindingPath = $"{nameof(_nebulaSettings.starPresets)}.Array.data[{i}].{nameof(StarData.starName)}";
            element.Q<ColorField>("StarColor").bindingPath = $"{nameof(_nebulaSettings.starPresets)}.Array.data[{i}].{nameof(StarData.starColor)}";
            element.Q<Slider>("StarRadiusSlider").bindingPath = $"{nameof(_nebulaSettings.starPresets)}.Array.data[{i}].{nameof(StarData.starRadius)}";
            element.Q<FloatField>("StarRadiusFloatField").bindingPath = $"{nameof(_nebulaSettings.starPresets)}.Array.data[{i}].{nameof(StarData.starRadius)}";
            element.Q<Slider>("GravityWellRadiusSlider").bindingPath = $"{nameof(_nebulaSettings.starPresets)}.Array.data[{i}].{nameof(StarData.gravityWellRadius)}";
            element.Q<FloatField>("GravityWellRadiusFloatField").bindingPath = $"{nameof(_nebulaSettings.starPresets)}.Array.data[{i}].{nameof(StarData.gravityWellRadius)}";

            // Create the container for the scroll view, spawn, and delete buttons
            var elementContainer = new VisualElement { name = "ScrollViewContainer" };

            // Create a new Foldout to contain the star preset
            var foldout = new Foldout();
            starNameTextField.RegisterValueChangedCallback(evt =>
            {
                // Change the text of the Foldout when the bound StarName value changes
                foldout.text = evt.newValue;
            });
            foldout.Add(element);
            elementContainer.Add(foldout);

            // Create a delete button
            var deleteButton = CreateDeleteButton(i);
            elementContainer.Add(deleteButton);

            // Create a spawn button
            var spawnButton = CreateSpawnButton(i);
            elementContainer.Add(spawnButton);

            scrollView.Add(elementContainer);
        }
        scrollView.Bind(_serializedNebulaSettings);
    }

    private Button CreateDeleteButton(int deleteIndex)
    {
        var deleteButton = new Button
        {
            name = "DeleteButton",
            text = "X",
        };
        deleteButton.clicked += () =>
        {
            var deletePreset = true;

            // Check for confirmation
            if (_nebulaSettings.askForConfirmationBeforeDeletingPreset)
            {
                var deleteOption = EditorUtility.DisplayDialogComplex(
                    "Delete Star Preset?",
                    $"Are you sure you want to delete the Star Preset \"{_nebulaSettings.starPresets[deleteIndex].starName}\"?",
                    "Delete",
                    "Cancel",
                    "Delete and don't ask again");
                if (deleteOption == 0 || deleteOption == 2)
                {
                    // serialized object will be dirtied and updated below
                    if (deleteOption == 2)
                        _nebulaSettings.askForConfirmationBeforeDeletingPreset = false;
                }
                else
                {
                    deletePreset = false;
                }
            }

            if (deletePreset)
            {
                _nebulaSettings.starPresets.RemoveAt(deleteIndex);
                _serializedNebulaSettings.Update();
                EditorUtility.SetDirty(_nebulaSettings);
            }
        };
        return deleteButton;
    }

    private Button CreateSpawnButton(int spawnIndex)
    {
        var spawnButton = new Button
        {
            name = "SpawnButton",
            text = "Spawn Star",
        };
        spawnButton.clicked += () =>
        {
            SpawnStarFromPreset(spawnIndex);
        };

        return spawnButton;
    }

    private void SpawnStarFromPreset(int spawnIndex)
    {
        var star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        star.transform.position = _nebulaSettings.spawnLocation;
        var starComponent = star.AddComponent<Star>();
        starComponent.starData = _nebulaSettings.starPresets[spawnIndex];
        starComponent.OnValidate();
    }

    private void PrepareCreateNewPresetButton()
    {
        var button = rootVisualElement.Q<Button>("CreateNewPresetButton");
        button.clicked += () =>
        {
            _nebulaSettings.starPresets.Add(new StarData());
            _serializedNebulaSettings.Update();
            EditorUtility.SetDirty(_nebulaSettings);
        };
    }
}