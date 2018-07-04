using UnityEngine;
using UnityEditor;


public class BuildProject : MonoBehaviour
{
    public static void BuildAndroid()
    {
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenesPath();
        buildPlayerOptions.locationPathName ="android.apk";
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;
        BuildPipeline.BuildPlayer(buildPlayerOptions);
       
    }

    protected static string[] GetScenesPath()
    {
        var buildscenes = EditorBuildSettings.scenes;
        string[] res = new string[buildscenes.Length];
        for (int i = 0; i < buildscenes.Length; ++i) res[i] = buildscenes[i].path;
        return res;
    }
   
}
