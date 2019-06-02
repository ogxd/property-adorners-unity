using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(TriggerOnChangeAttribute), true)]
public class TriggerOnChangeDrawer : PropertyAdornerDrawer {

    private VoidDelegate _onUpdate;
    public VoidDelegate onUpdate {
        get {
            if (_onUpdate == null) {
                string methodStr = (attribute as TriggerOnChangeAttribute).onUpdate;
                var methodInfo = targetObject.GetType().GetMethod(methodStr,
                    BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (methodInfo == null)
                    throw new Exception($"Method '{methodStr}' does not exists in '{targetObject}'");
                _onUpdate = methodInfo.CreateDelegate(typeof(VoidDelegate), targetObject) as VoidDelegate;
                if (_onUpdate == null)
                    throw new Exception("Delegate must be a parameterless void method");
            }
            return _onUpdate;
        }
    }

    public override void OnBeforeGUI(ref Rect position, ref GUIContent label) {
        EditorGUI.BeginChangeCheck();
    }

    public override void OnAfterGUI(Rect position) {
        if (EditorGUI.EndChangeCheck()) {
            onUpdate();
        }
    }
}