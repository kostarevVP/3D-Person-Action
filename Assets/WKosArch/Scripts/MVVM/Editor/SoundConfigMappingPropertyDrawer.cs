using UnityEditor;
using UnityEngine;
using WKosArch.Sound_Feature;

namespace WKosArch.MVVM.Editor
{
    [CustomPropertyDrawer(typeof(SoundConfigMapping))]
    public class SoundConfigMappingPropertyDrawer : PropertyDrawer
    {
        private readonly GUIContent _keyLabelGUIContent = new("SoundType:");
        private readonly GUIContent _valueGUIContent = new("SoundConfig");

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var key = property.FindPropertyRelative("Key");
            var value = property.FindPropertyRelative("Value");


            var keyLabelRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 5, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight);
            var valueRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2 + 10, EditorGUIUtility.currentViewWidth, EditorGUIUtility.singleLineHeight);


            EditorGUI.PrefixLabel(keyLabelRect, GUIUtility.GetControlID(FocusType.Passive), _keyLabelGUIContent);

            EditorGUI.PropertyField(keyLabelRect, key, _keyLabelGUIContent);

            EditorGUI.PropertyField(valueRect, value, _valueGUIContent);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3 + 10;
        }
    }
}