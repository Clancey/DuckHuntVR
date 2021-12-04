using BitStrap;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu]
public class SetXRSettingsAction : UMakeBuildAction
{
	public BuildTargetGroup buildTarget;
	
	[PopupSelector]
	public string[] xrDevices;

	public override void Execute(UMake umake, UMakeTarget target)
	{
		//PlayerSettings.SetVirtualRealitySDKs(buildTarget, xrDevices);
	}
}