using UnityEditor;
using UnityEngine;

namespace BitStrap
{
	public sealed class UMake : ScriptableObject
	{
		public const string Path = "Assets/BitStrap/Plugins/UMake/Editor/UMake.asset";
		public const string buildPathPrefKey = "UMake_BuildPath_";

		public string version = "0.0";

		[UMakeTargetActions]
		public UMakeTarget[] targets;

		private static Option<UMake> instance = Functional.None;

		public static string BuildPathPref
		{
			get { return EditorPrefs.GetString( buildPathPrefKey + PlayerSettings.productName, "" ); }
			set { EditorPrefs.SetString( buildPathPrefKey + PlayerSettings.productName, value ); }
		}

		public static Option<UMake> Get()
		{
			UMake umake;
			if( !instance.TryGet( out umake ) )
			{
				umake = AssetDatabase.LoadAssetAtPath<UMake>( Path );
				if (umake == null)
				{
					var validTargets = AssetDatabase.FindAssets("t:UMake");
					if (validTargets.Length > 0)
						umake = AssetDatabase.LoadAssetAtPath<UMake>(AssetDatabase.GUIDToAssetPath(validTargets[0]));
				}
				
				instance = umake;
			}

			return umake;
		}

		public static Option<UMakeTarget> GetTarget( string targetName )
		{
			return
				from umake in Get()
				from target in umake.targets.First( t => t.name == targetName )
				select target;
		}

		public static string GetBuildPath()
		{
			string path = BuildPathPref;

			if( !string.IsNullOrEmpty( path ) )
				return path;

			return EditorUtility.OpenFolderPanel( "Build Path", path, "Builds" );
		}
	}
}