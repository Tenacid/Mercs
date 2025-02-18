using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SevenMagpies.LevelMapping;

[CustomEditor(typeof(MappingSettings))]
public class eMappingSettings : Editor
{
    private static string LastPath;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if ( GUILayout.Button( "Import Mapping Meta from file" ) )
        {
            ImportMappingMeta();
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
}
