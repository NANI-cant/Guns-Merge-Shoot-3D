using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SelectImplementationAttribute))]
public class SelectImplementationDrawer : PropertyDrawer {
    private class MenuClickResult {
        public Type IncomingType;
        public SerializedProperty Property;

        public MenuClickResult(Type incomingType, SerializedProperty property) {
            IncomingType = incomingType;
            Property = property;
        }
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        DrawChangeButton(position, property);
        DrawProperty(position, property);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUI.GetPropertyHeight(property, true);
    }

    private void DrawChangeButton(Rect parentPosition, SerializedProperty property) {
        var rightHalfRect = new Rect(parentPosition);
        {
            rightHalfRect.width *= 1f / 4f;
            rightHalfRect.x += parentPosition.width * 3f / 4f;
            rightHalfRect.height = EditorGUIUtility.singleLineHeight;
        }

        if (GUI.Button(rightHalfRect, "Change")) {
            ShowMenu(property);
        }
    }

    private void ShowMenu(SerializedProperty property) {
        GenericMenu menu = new GenericMenu();

        Type[] implementations = GetImplementations((attribute as SelectImplementationAttribute).FieldType);

        foreach (var implementation in implementations) {
            var label = new GUIContent(implementation.FullName);
            menu.AddItem(label, false, HandleMenuChoose, new MenuClickResult(implementation, property));
        }

        menu.ShowAsContext();
    }

    private void DrawProperty(Rect parentPosition, SerializedProperty property) {
        var singleLineRect = new Rect(parentPosition);
        {
            singleLineRect.height = EditorGUIUtility.singleLineHeight;
        }
        var label = new GUIContent(GetCurrentTypeName(property));
        if (label.text == "") {
            EditorGUI.PropertyField(singleLineRect, property, true);
            return;
        }
        EditorGUI.PropertyField(singleLineRect, property, label, true);
    }

    private void HandleMenuChoose(object menuClickObject) {
        Type incomingType = (menuClickObject as MenuClickResult).IncomingType;
        SerializedProperty property = (menuClickObject as MenuClickResult).Property;

        property.serializedObject.UpdateIfRequiredOrScript();

        if (incomingType.FullName == GetCurrentTypeName(property)) {
            return;
        }

        var instance = Activator.CreateInstance(incomingType);
        property.managedReferenceValue = instance;

        property.serializedObject.ApplyModifiedProperties();
    }


    private Type[] GetImplementations(Type interfaceType) {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
        return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
    }

    private string GetCurrentTypeName(SerializedProperty property) {
        string currentTypeNameFull = property.managedReferenceFullTypename;
        string[] splittedFullName = currentTypeNameFull.Split(' ');

        if (splittedFullName.Length == 0) {
            return "";
        }

        return splittedFullName[splittedFullName.Length - 1];
    }
}
