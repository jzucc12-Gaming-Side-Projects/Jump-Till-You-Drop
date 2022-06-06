using UnityEditor;
using UnityEditor.UI;
using JZ.BUTTONS;

#region //Editor
[CustomEditor(typeof(HoldButton))]
public class HoldButtonEditor : ButtonEditor
{
    public SerializedProperty holdtimerProp;

    protected override void OnEnable() 
    {
        base.OnEnable();
        holdtimerProp = serializedObject.FindProperty("timeToHold");
    }

    public override void OnInspectorGUI()
    {
        HoldButton holdButton = (HoldButton)target;
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.PropertyField(holdtimerProp);
        serializedObject.ApplyModifiedProperties();
    }
}
#endregion
