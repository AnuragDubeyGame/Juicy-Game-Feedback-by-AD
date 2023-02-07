using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ADFeedbacks.VFXSettings))]
public class VFXSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the VFX boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentVFX = parent.FindProperty("UseVFX");

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
        SerializedProperty parentCameraShake = parent.FindProperty("UseVFX");

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
