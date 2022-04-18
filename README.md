# Nebula Tool

A Unity UI Toolkit-based tool for creating and editing Star presets. These presets can easily be instantiated into the scene as `Star` GameObjects, which have their own custom inspectors for editing relevant values.

### Prerequisites
* Unity Editor version 2020.3 (created with 2020.3.32f1)

### Creating Star Presets

* Open the Nebula Tool by clicking the menu `Fault In Our Stars` and selecting `Nebula`
* In the `Nebula` window, click the `Create New Preset` button to add a new Star Preset
* Configure the values of the Star Preset

### Spawning Stars from Presets

* Adjust the `Star Spawn Location` field of the `Nebula` window to choose where the Star object will be spawned
* Click the `Spawn Star` button next to the preset to spawn to create a `Star` GameObject in the scene at the specified location

### Adjusting Spawned Star Properties in the Scene

* After a Star has been spawned, it will appear in the Scene and the Hierarchy view
* When selecting the `Star` GameObject, notice that the same custom inspector used in the `Nebula` window appears in the Inspector for this GameObject, and can be used to edit the properties of this star.

### Navigating and Deleting Star presets

* If more presets are created than what can fit in the window, a scrollbar will activate and can be used to scroll through the list of presets.
* Each preset can be collapsed for a more condensed listing of the presets.
  * Stars can still be spawned from a collapsed preset.
* Clicking the red `X` button at the top-right of a preset will delete that preset from the list.
  * A dialog will ask you for confirmation before deleting, which can be optionally disabled for one-click deletion of presets.
  * The confirmation dialog can be enabled or disabled in `NebulaSettings`

### Saving and Loading Star Presets and Configuration

* Selecting the `NebulaSettings` object in the Hierarchy view will show a custom inspector with buttons to save to a JSON file and load from a JSON file.
* Click `Save to JSON File` to save the current settings to a JSON file
* Click `Load from JSON File` to load from a JSON file and overwrite the current settings

### `NebulaSettings`

* `NebulaSettings` is a GameObject that is created when using the Nebula Tool.
* Various configuration options and data are stored in this object, including:
  * Star preset data
  * Confirmation dialog settings
* Deleting this GameObject will lose all current star preset data permanently, unless the settings have been saved
