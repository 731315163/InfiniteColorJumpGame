using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class SetResourcePackingTag : EditorWindow
{
    [MenuItem("Tools/修改资源的PackingTag")]
    static public void Init()
    {
        var window = EditorWindow.GetWindow<SetResourcePackingTag>("修改资源的PackingTag");
        window.Show();
    }

    private List<string> textureList = new List<string>();
    Vector2 scrollPos = Vector2.zero;
    private string DirectoryName = "请输入文件夹";

    void Awake()
    {
        string uipath = EditorPrefs.GetString("UIpath");
        if (!String.IsNullOrEmpty(uipath)) DirectoryName = uipath;
    }

    private void StartCheck()
    {
        
        List<string> withoutExtensions = new List<string>() { ".png", ".jpg" };
        string[] files = Directory.GetFiles(Application.dataPath + DirectoryName , "*.*", SearchOption.AllDirectories)
            .Where(s => withoutExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
        
        foreach (string file in files)
        {
            string strFile = file.Substring(file.IndexOf("Asset")).Replace('\\', '/');
            textureList.Add(strFile);
        }
    }

    void OnGUI()
    {
        DirectoryName = GUILayout.TextField(DirectoryName);
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("搜索出所有Texutre", GUILayout.Width(200), GUILayout.Height(50)))
        {
            textureList.Clear();
            StartCheck();
        }

        if (GUILayout.Button("修改所有PackingTag", GUILayout.Width(200), GUILayout.Height(50)))
        {
            float count = 0;
            foreach (string t in textureList)
            {
                count++;
                EditorUtility.DisplayProgressBar("Processing...", "替换中... (" + count + " / " + textureList.Count + ")", count / textureList.Count);
                TextureImporter ti = AssetImporter.GetAtPath(t) as TextureImporter;

                //可以重新导入
                //ti.SaveAndReimport();
                string dirName = Path.GetDirectoryName(t);
                string folderStr = Path.GetFileName(dirName);
                ti.spritePackingTag = folderStr;
            }

            EditorUtility.ClearProgressBar();
        }
        EditorGUI.EndChangeCheck();
        EditorGUILayout.EndHorizontal();

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (string t in textureList)
        {
            EditorGUILayout.BeginHorizontal();
            string path = t;
            EditorGUILayout.LabelField("文件路径 : " + path, GUILayout.Width(400));

            string dirName = Path.GetDirectoryName(path);
            string folderStr = Path.GetFileName(dirName);

            Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(t, typeof(Texture2D));
            EditorGUILayout.ObjectField(texture, typeof(Texture2D), false);

            TextureImporter ti = AssetImporter.GetAtPath(t) as TextureImporter;
            if(ti == null)return;
            
            EditorGUILayout.LabelField("当前PackingTag类型 :   [" + ti.spritePackingTag + "]");

            EditorGUILayout.Space();
            if (ti.spritePackingTag != folderStr)
            {
                EditorGUILayout.LabelField("错误 !!!  文件夹名字 : [" + folderStr + "]");
            }
            else
            {
                EditorGUILayout.LabelField("");
            }

            if (GUILayout.Button("修改PackingTag", GUILayout.Width(150)))
            {
                //可以重新导入
                //ti.SaveAndReimport();
                ti.spritePackingTag = folderStr;
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        EditorGUILayout.EndScrollView();
    }

    void OnDestroy()
    {
        EditorPrefs.SetString("UIpath",DirectoryName);
    }
}