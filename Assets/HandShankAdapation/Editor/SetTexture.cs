using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


public class SetTexture
{
  
    [MenuItem("Tools/SetAndroidETC1")]
    public static void SetAndroidETC1()
    {
        LoopSetTexture(delegate( TextureImporter imp)
        {
            SetTextureSprite(imp);
            
        });
    }
   
    private static void LoopSetTexture(Action<TextureImporter> importaction)
    {
        Object[] textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

        foreach (Texture2D texture in textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter texImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            importaction(texImporter);
            AssetDatabase.ImportAsset(path);
        }
    }

    private static void SetTextureSprite(TextureImporter importer, string tag = null)
    {
        if (importer == null) throw new NullReferenceException();
        importer.textureType = TextureImporterType.Sprite;
        importer.mipmapEnabled = false;
        importer.isReadable = false;
        if (tag != null)
        {
            tag = tag.ToLower();
            importer.spritePackingTag = tag;
        }

#if UNITY_ANDROID
        int maxSize = 2048;
        TextureImporterFormat format;
        int quality = 50;
        importer.GetPlatformTextureSettings("Android", out maxSize, out format, out quality);
        TextureImporterPlatformSettings settings = new TextureImporterPlatformSettings();
        settings.name = "Android";
        settings.allowsAlphaSplitting = true;
        settings.compressionQuality = quality;
        settings.format = TextureImporterFormat.ETC_RGB4;
        settings.overridden = true;

       
        importer.textureType = TextureImporterType.Sprite;
        importer.mipmapEnabled = false;
        importer.SetPlatformTextureSettings(settings);
#endif
    }

  
}
