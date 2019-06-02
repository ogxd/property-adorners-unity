using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ColorAttribute), true)]
public class ColorDrawer : PropertyAdornerDrawer {

    private Color _tempColor;

    public override void OnBeforeGUI(ref Rect position, ref GUIContent label) {
        _tempColor = GUI.color;
        GUI.color = ((ColorAttribute)attribute).color;
    }

    public override void OnAfterGUI(Rect position) {
        GUI.color = _tempColor;
    }
}