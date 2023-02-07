using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(FeedBack_Base.TransformationSettings))]
public class TransformationSettingsDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the VFX boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentVFX = parent.FindProperty("UseTransformations");

        // Only display the VFXSettings field in the inspector if VFX is true
        if (parentVFX.boolValue)
        {
            Rect offsetPosition = new Rect(position.x + 15, position.y, position.width, position.height);
            EditorGUI.PropertyField(offsetPosition, property, label, true);
        }
    }
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Get the parent object that holds the CameraShake boolean field
        SerializedObject parent = property.serializedObject;
        SerializedProperty parentCameraShake = parent.FindProperty("UseTransformations");

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

