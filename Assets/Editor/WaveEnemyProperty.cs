using Metric.Levels;
using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomPropertyDrawer(typeof(WaveEnemy))]
    public class WaveEnemyProperty: PropertyDrawer {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            WaveEnemy template = new WaveEnemy();

            var leftHalfRect = new Rect(position);
            leftHalfRect.width /= 2;
            EditorGUI.PropertyField(leftHalfRect, property.FindPropertyRelative(nameof(template.Enemy)));

            var rightHalfRect = new Rect(position);
            rightHalfRect.width /= 2;
            rightHalfRect.position += new Vector2(rightHalfRect.width, 0);
            EditorGUI.PropertyField(rightHalfRect, property.FindPropertyRelative(nameof(template.Count)));
        }
    }
}