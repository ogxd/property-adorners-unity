using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(IndentAttribute), true)]
public class IndentDrawer : PropertyAdornerDrawer {

    private int _previousIndent;

    public override void OnBeforeGUI(ref Rect position, ref GUIContent label) {
        _previousIndent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = ((IndentAttribute)attribute).indent;
    }

    public override void OnAfterGUI(Rect position) {
        EditorGUI.indentLevel = _previousIndent;
    }
}