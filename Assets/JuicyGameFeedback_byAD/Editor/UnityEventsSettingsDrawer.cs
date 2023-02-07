using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FeedBack_Base.TriggerEvent))]
public class UnityEventsSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the VFX boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentVFX = parent.FindProperty("UseEvents");

        // Only display the VFXSettings field in the inspector if VFX is true
        if (parentVFX.boolValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the CameraShake boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentCameraShake = parent.FindProperty("UseEvents");

        if (parentCameraShake.boolValue)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        else
        {
            return 0;
        }
    }
}
