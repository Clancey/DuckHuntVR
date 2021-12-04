using BitStrap;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class SetLibraryStatusAction : UMakeBuildAction
{
	public Object pluginObject;

	public BuildTarget targetPlatform;
	public bool compatibilityStatus;
	
	public override void Execute(UMake umake, UMakeTarget target)
	{
		var assetImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(pluginObject)) as PluginImporter;
		if (assetImporter == null)
			return;

		assetImporter.SetCompatibleWithPlatform(targetPlatform, compatibilityStatus);
	}
}
