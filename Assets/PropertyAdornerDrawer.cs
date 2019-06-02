using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

public delegate Type TypeDelegate(Type type);

/// <summary>
/// The PropertyAdornerDrawer allows specific GUI adorning 
/// </summary>
public abstract class PropertyAdornerDrawer : PropertyDrawer {

    private bool _initialized = false;
    private PropertyDrawer _customPropertyDrawer;
    private List<PropertyAdornerDrawer> _allPropertyAdorners = new List<PropertyAdornerDrawer>();

    public abstract void OnBeforeGUI(ref Rect position, ref GUIContent label);

    public abstract void OnAfterGUI(Rect position);

    public object targetObject { get; private set; }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        if (!_initialized) {
            var fieldInfo = property.serializedObject.targetObject.GetType().GetField(property.name);
            var propertyAttributes = fieldInfo.GetCustomAttributes<PropertyAttribute>();
            foreach (var propertyAttribute in propertyAttributes) {
                PropertyDrawer drawer = CreatePropertyDrawerInstance(propertyAttribute, fieldInfo, propertyAttribute);
                if (drawer == null)
                    continue;
                if (propertyAttribute is PropertyAdornerAttribute) {
                    _allPropertyAdorners.Add((PropertyAdornerDrawer)drawer);
                } else {
                    _customPropertyDrawer = drawer;
                }
            }
            _initialized = true;
        }

        for (int d = 0; d < _allPropertyAdorners.Count; d++) {
            _allPropertyAdorners[d].targetObject = property.serializedObject.targetObject;
            _allPropertyAdorners[d].OnBeforeGUI(ref position, ref label);
        }

        if (_customPropertyDrawer != null) {
            _customPropertyDrawer.OnGUI(position, property, label);
        } else {
            EditorGUI.PropertyField(position, property, label, false);
        }

        for (int d = 0; d < _allPropertyAdorners.Count; d++) {
            _allPropertyAdorners[d].OnAfterGUI(position);
        }
    }

    public static PropertyDrawer CreatePropertyDrawerInstance(PropertyAttribute propertyAttribute, FieldInfo fieldInfo, PropertyAttribute attribute) {
        Type drawerType = GetDrawerTypeForType(propertyAttribute.GetType());
        var propertyDrawer = Activator.CreateInstance(drawerType) as PropertyDrawer;
        if (propertyDrawer == null)
            return null;
        typeof(PropertyDrawer).GetField("m_FieldInfo", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(propertyDrawer, fieldInfo);
        typeof(PropertyDrawer).GetField("m_Attribute", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(propertyDrawer, attribute);
        return propertyDrawer;
    }

    private static TypeDelegate _GetDrawerTypeForType;
    public static Type GetDrawerTypeForType(Type attributeType) {
        if (_GetDrawerTypeForType == null) {
            Type scriptAttributeUtilityType = Type.GetType("UnityEditor.ScriptAttributeUtility, UnityEditor");
            var method = scriptAttributeUtilityType.GetMethod("GetDrawerTypeForType", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            _GetDrawerTypeForType = (TypeDelegate)method.CreateDelegate(typeof(TypeDelegate));
        }
        return _GetDrawerTypeForType(attributeType);
    }
}