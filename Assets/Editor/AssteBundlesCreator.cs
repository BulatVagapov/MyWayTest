using UnityEditor;

public class AssteBundlesCreator
{
    [MenuItem("Assets/CreateAssetBundle")]
    public static void CreateAssetBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
