using UnityEngine;
using System.Collections;
using UnityEditor;

public class BundleBuilder : Editor
{
	//打包单个
	[MenuItem("Custom Editor/Create AssetBunldes For Windows")]
	static void CreateAssetBunldesMain ()
	{
		//获取在Project视图中选择的所有游戏对象
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
		
		//遍历所有的游戏对象
		foreach (Object obj in SelectedAsset) 
		{
			//本地测试：建议最后将Assetbundle放在StreamingAssets文件夹下，如果没有就创建一个，因为移动平台下只能读取这个路径
			//StreamingAssets是只读路径，不能写入
			//服务器下载：就不需要放在这里，服务器上客户端用www类进行下载。
			string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies)) {
				Debug.Log(obj.name +"资源打包成功");
			} 
			else 
			{
				Debug.Log(obj.name +"资源打包失败");
			}
		}
		//刷新编辑器
		AssetDatabase.Refresh ();	
		
	}


	[MenuItem("Custom Editor/Create AssetBunldes For Android")]
	static void CreateAssetBunldesAndroid ()
	{
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);

		foreach (Object obj in SelectedAsset) 
		{
			string targetPath = Application.dataPath + "/StreamingAssets/" + obj.name + ".assetbundle";

			//public static bool BuildAssetBundle(Object mainAsset, Object[] assets, string pathName, out uint crc, BuildAssetBundleOptions assetBundleOptions, BuildTarget targetPlatform);
			if (BuildPipeline.BuildAssetBundle (obj, null, targetPath, BuildAssetBundleOptions.CollectDependencies, BuildTarget.Android)) {
				Debug.Log(obj.name +"资源打包成功");
			} 
			else 
			{
				Debug.Log(obj.name +"资源打包失败");
			}
		}
		AssetDatabase.Refresh ();	
	}
	
	[MenuItem("Custom Editor/Create AssetBunldes ALL")]
	static void CreateAssetBunldesALL ()
	{
		Caching.CleanCache ();
		string Path = Application.dataPath + "/StreamingAssets/ALL.assetbundle";
		
		
		Object[] SelectedAsset = Selection.GetFiltered (typeof(Object), SelectionMode.DeepAssets);
		
		foreach (Object obj in SelectedAsset) 
		{
			Debug.Log ("Create AssetBunldes name :" + obj);
		}
		
		//这里注意第二个参数就行
		if (BuildPipeline.BuildAssetBundle (null, SelectedAsset, Path, BuildAssetBundleOptions.CollectDependencies)) {
			AssetDatabase.Refresh ();
		} else {
			
		}
	}
	
	[MenuItem("Custom Editor/Create Scene")]
	static void CreateSceneALL ()
	{
		//清空一下缓存
		Caching.CleanCache();
		string Path = Application.dataPath + "/MyScene.unity3d";
		string  []levels = {"Assets/Level.unity"};
		//打包场景
		BuildPipeline.BuildPlayer( levels, Path,BuildTarget.WebPlayer, BuildOptions.BuildAdditionalStreamedScenes);
		AssetDatabase.Refresh ();
	}
	
}