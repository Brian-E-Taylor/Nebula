using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(Star))]
public class Star_Inspector : Editor
{
    public VisualTreeAsset inspectorXML;
    
    public override VisualElement CreateInspectorGUI()
    {
        var myInspector = new VisualElement();
        
        inspectorXML.CloneTree(myInspector);
        
        return myInspector;
    }
}
