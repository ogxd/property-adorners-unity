using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(DisableOnConditionAttributeAttribute), true)]
public class DisableOnConditionDrawer : PropertyAdornerDrawer {

    private BoolDelegate _condition;
    public BoolDelegate condition {
        get {
            if (_condition == null) {
                string methodStr = (attribute as DisableOnConditionAttributeAttribute).condition;
                var methodInfo = targetObject.GetType().GetMethod(methodStr,
                    BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (methodInfo == null)
                    throw new Exception($"Method '{methodStr}' does not exists in '{targetObject}'");
                _condition = methodInfo.CreateDelegate(typeof(BoolDelegate), targetObject) as BoolDelegate;
                if (_condition == null)
                    throw new Exception("Delegate must be a parameterless method that returns a boolean value");
            }
            return _condition;
        }
    }

    public override void OnBeforeGUI(ref Rect position, ref GUIContent label) {
        EditorGUI.BeginDisabledGroup(condition());
    }

    public override void OnAfterGUI(Rect position) {
        EditorGUI.EndDisabledGroup();
    }
}