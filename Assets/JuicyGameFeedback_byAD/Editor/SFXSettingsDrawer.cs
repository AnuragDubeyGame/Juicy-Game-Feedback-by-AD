using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FeedBack_Base.SFXSettings))]
public class SFXSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the SFX boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentSFX = parent.FindProperty("UseSFX");

        // Only display the SFXSettings field in the inspector if SFX is true
        if (parentSFX.boolValue)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the CameraShake boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentCameraShake = parent.FindProperty("UseSFX");

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

