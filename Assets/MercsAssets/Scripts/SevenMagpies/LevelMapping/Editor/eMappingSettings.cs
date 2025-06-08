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

        var surfaceData = ReadData( "Select png file with surface", LastPath );
        var backgroundData = ReadData( "Select png file with background", LastPath );        
        var entitiesData = ReadData( "Select png file with entities", LastPath );

        var levelData = ScriptableObject.CreateInstance<LevelData>();
        levelData.FillData( surfaceData, backgroundData, entitiesData );

        if ( string.IsNullOrEmpty( LevelDataPath ) )
        {
            LevelDataPath = Application.dataPath;
        }

        var savePath = EditorUtility.SaveFilePanel( "Save level", LevelDataPath, "Level", "asset" );
        var appPath = Application.dataPath;

        if ( string.IsNullOrEmpty( savePath ) )
        {
            return;
        }

        LevelDataPath = System.IO.Path.GetFullPath( savePath );

        UnityEditor.AssetDatabase.CreateAsset( levelData, savePath.Replace(appPath, "Assets" ) );

    }

    private string[,] ReadData(string title , string directory) 
    {
        var filePath = EditorUtility.OpenFilePanel( title, directory, "png" );

        if ( string.IsNullOrEmpty( filePath ) )
        {
            throw new System.Exception($"Invalid file path: {filePath}");
        }

        LastPath = System.IO.Path.GetFullPath( filePath );
        return MappingImporter.ImportLevel( filePath, ( MappingSettings ) target );

    }
}
