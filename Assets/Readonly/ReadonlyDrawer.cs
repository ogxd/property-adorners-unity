using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(ReadonlyAttribute), true)]
public class ReadonlyDrawer : PropertyAdornerDrawer {

    public object getValue(SerializedProperty property) {
        object obj = property.serializedObject.targetObject;

        FieldInfo field = null;
        foreach (var path in property.propertyPath.Split('.')) {
            var type = obj.GetType();
            field = type.GetField(path);
            obj = field.GetValue(obj);
        }
        return obj;
    }

    public override void OnBeforeGUI(ref Rect position, ref GUIContent label) {

    }

    public override void OnAfterGUI(Rect position) {

    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.LabelField(position, label, new GUIContent(getValue(property).ToString()));
    }
}