using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SimpleExportAssetBundles : EditorWindow {
	
	// Add menu item to show this demo.
    [MenuItem("pixelfat/Asset Management/Prefab Exporter")]
    static void ShowWindow ()
    {
       EditorWindow.GetWindow (typeof(SimpleExportAssetBundles));
    }
	
	public List<Transform> mPrefabs = new List<Transform>();
	public Object mObjectToAdd;
	bool showingPrefabs = false;
	
	public Object assetBundleToTest;
	
	string mPlatformString = "Unknown platform.";
	
    void OnGUI ()
    {
		
		mPlatformString = EditorUserBuildSettings.activeBuildTarget.ToString();
		
        BeginWindows ();
		
		GUILayout.Label("");
		
		GUILayout.Label("Current platform: " + mPlatformString);
		
		GUILayout.Label("");
		
		showingPrefabs =  EditorGUILayout.Foldout(showingPrefabs, "Prefabs (" + mPrefabs.Count + ")");
		
		if(showingPrefabs)
			RenderPrefabMenu();
		
		GUILayout.Label("");
		
	    mObjectToAdd = EditorGUILayout.ObjectField("Additonal Prefab: ", mObjectToAdd, typeof(Transform), false);
		
		if(mObjectToAdd!=null) {
		
			if (!mPrefabs.Contains(mObjectToAdd as Transform)) {
				mPrefabs.Add(mObjectToAdd as Transform);
				mObjectToAdd = null;
			}
			
		}
		
		GUILayout.Label("");
		
		GUILayout.BeginHorizontal();
		GUILayout.FlexibleSpace();
		
		if(mPrefabs.Count>0) {

			if(GUILayout.Button("Export", GUILayout.Width(100))) {

	            // Build the resource file from the active selection.
				foreach(Transform thePrefabTransform in mPrefabs) {
					
					string _fullFilePath = Application.dataPath + "/Output/" + mPlatformString + "/" + thePrefabTransform.name + ".unity3d";
					
					Debug.Log("Exporting asset bundle to: " + _fullFilePath);
					
					Object[] _selection = new Object[1]{thePrefabTransform as Object};

					BuildPipeline.BuildAssetBundle(_selection[0], _selection, _fullFilePath , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget  );
				
				}

			}
			
			GUILayout.FlexibleSpace();
			
			if(GUILayout.Button("Export to...", GUILayout.Width(100))) {

				string _customPath = EditorUtility.SaveFolderPanel("Save To Folder", "", "New Asset.unity3d");
				
				if (_customPath.Length != 0) {
					
		            // Build the resource file from the active selection.
					foreach(Transform thePrefabTransform in mPrefabs) {
						
						string _fullFilePath = _customPath + "/" + thePrefabTransform.name + ".unity3d";
						
						Debug.Log("Exporting asset bundle to: " + _fullFilePath);
						
						Object[] _selection = new Object[1]{thePrefabTransform as Object};
						
						BuildPipeline.BuildAssetBundle(_selection[0], _selection, _fullFilePath , BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.DeterministicAssetBundle, EditorUserBuildSettings.activeBuildTarget  );
					
					}
					
				}
				
			} 
			
		}else
			GUILayout.Label("No prefabs selected");
		
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
       EndWindows ();
		
    }
	UnityEngine.Object[] _dependancies;
	void RenderPrefabMenu() {
		
		foreach(Transform thePrefabTransform in mPrefabs) {
			
			//GUILayout.BeginHorizontal();
			
			EditorGUILayout.ObjectField("Product Prefab: ", thePrefabTransform, typeof(Transform), false);
			
			_dependancies = EditorUtility.CollectDependencies(new Object[] {thePrefabTransform});
			
			if(GUILayout.Button("Select dependancies", GUILayout.Width(150))) {

                Selection.objects = _dependancies;	
				
			}
			
			//GUILayout.EndHorizontal();
			
		}
		
		GUILayout.Label("");
		
		if(mPrefabs.Count>0)
			if(GUILayout.Button("Clear", GUILayout.Width(100)))
				mPrefabs.Clear();
		
	}
	
}