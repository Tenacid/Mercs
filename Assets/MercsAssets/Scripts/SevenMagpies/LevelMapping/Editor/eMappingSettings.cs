using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SevenMagpies.LevelMapping;

[CustomEditor(typeof(MappingSettings))]
public class eMappingSettings : Editor
{
    private static string LastPath;
    private static string LevelDataPath;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if ( GUILayout.Button( "Import Mapping Meta from file" ) )
        {
            ImportMappingMeta();
        }

        if ( GUILayout.Button( "Import Level" ) )
        {
            ImportLevel();
        }
    }

    private void ImportMappingMeta() 
    {
        if ( string.IsNullOrEmpty( LastPath ) )
        {
            LastPath = Application.dataPath;
        }

        var filePath = EditorUtility.OpenFilePanel( "Select png file with level mapping", LastPath, "png" );

        if ( string.IsNullOrEmpty( filePath ) )
        {
            return;
        }

        LastPath = System.IO.Path.GetFullPath( filePath );

        MappingImporter.ImportMappingMeta( filePath, ( MappingSettings ) target );
    }

    private void ImportLevel() 
    {
        if ( string.IsNullOrEmpty( LastPath ) )
        {
            LastPath = Application.dataPath;
        }

        var filePath = EditorUtility.OpenFilePanel( "Select png file with level", LastPath, "png" );

        if ( string.IsNullOrEmpty( filePath ) )
        {
            return;
        }

        LastPath = System.IO.Path.GetFullPath( filePath );

        var data = MappingImporter.ImportLevel( filePath, ( MappingSettings ) target );

        var levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.FillData( data );

        if ( string.IsNullOrEmpty( LevelDataPath ) )
        {
            LevelDataPath = Application.dataPath;
        }

        var savePath = EditorUtility.SaveFilePanel( "Save level", LevelDataPath, System.IO.Path.GetFileNameWithoutExtension( filePath ), "asset" );
        var appPath = Application.dataPath;

        if ( string.IsNullOrEmpty( savePath ) )
        {
            return;
        }

        LevelDataPath = System.IO.Path.GetFullPath( savePath );

        UnityEditor.AssetDatabase.CreateAsset( levelData, savePath.Replace(appPath, "Assets" ) );

    }
}
