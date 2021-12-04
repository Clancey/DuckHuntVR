using System;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PopupSelectorAttribute))]
public class PopupSelectorDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		var buildTarget = (BuildTargetGroup) property.serializedObject.FindProperty("buildTarget").enumValueIndex;

		if (buildTarget == BuildTargetGroup.Unknown)
		{
			property.stringValue = "None";
			return;
		}

		//var options = PlayerSettings.GetAvailableVirtualRealitySDKs(buildTarget);
		//var previousIndex = Array.IndexOf(options, property.stringValue);

		//var i = EditorGUI.Popup(position, previousIndex, options);
		//if (i >= 0)
		//	property.stringValue = options[i];
	}
}