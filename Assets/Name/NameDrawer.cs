using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(NameAttribute), true)]
public class NameDrawer : PropertyAdornerDrawer {

    private int _previousIndent;

    public override void OnBeforeGUI(ref Rect position, ref GUIContent label) {
        label.text = ((NameAttribute)attribute).name;
    }

    public override void OnAfterGUI(Rect position) {

    }
}