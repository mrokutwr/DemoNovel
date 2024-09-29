using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(ToggleSpriteSwap))]
public class ToggleSpriteSwapEditor : Editor
{
    SerializedProperty targetImageProp;
    SerializedProperty onSpriteProp;
    SerializedProperty offSpriteProp;
    
    SerializedProperty isOnProp;
    SerializedProperty toggleTransitionProp;
    SerializedProperty graphicProp;
    SerializedProperty groupProp;
    SerializedProperty onValueChangedProp;

    void OnEnable()
    {
        targetImageProp = serializedObject.FindProperty("targetImage");
        onSpriteProp = serializedObject.FindProperty("onSprite");
        offSpriteProp = serializedObject.FindProperty("offSprite");

        isOnProp = serializedObject.FindProperty("m_IsOn");
        toggleTransitionProp = serializedObject.FindProperty("toggleTransition");
        graphicProp = serializedObject.FindProperty("graphic");
        groupProp = serializedObject.FindProperty("m_Group");
        onValueChangedProp = serializedObject.FindProperty("onValueChanged");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Custom Toggle Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(targetImageProp, new GUIContent("Target Image"));
        EditorGUILayout.PropertyField(onSpriteProp, new GUIContent("On Sprite"));
        EditorGUILayout.PropertyField(offSpriteProp, new GUIContent("Off Sprite"));

        EditorGUILayout.PropertyField(isOnProp);
        EditorGUILayout.PropertyField(toggleTransitionProp);
        EditorGUILayout.PropertyField(graphicProp);
        EditorGUILayout.PropertyField(groupProp);
        EditorGUILayout.PropertyField(onValueChangedProp);

        serializedObject.ApplyModifiedProperties();
    }
}
