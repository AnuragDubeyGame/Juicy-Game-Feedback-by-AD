using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ADFeedbacks.CameraShakeSettings))]
public class CameraShakeSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the CameraShake boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentCameraShake = parent.FindProperty("CameraShake");

        // Only display the CameraShakeSettings field in the inspector if CameraShake is true
        if (parentCameraShake.boolValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the CameraShake boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentCameraShake = parent.FindProperty("CameraShake");

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