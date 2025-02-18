using SevenMagpies.LevelMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class MappingImporter
{

    public static void ImportMappingMeta( string filePath, MappingSettings settings ) 
    {
        if ( !TryLoadTexture( filePath, out var texture ) )
        {
            throw new Exception( $"Can't get texture from {filePath}" );
        }

        int width = texture.width;
        int height = texture.height;

        var colors = new HashSet<Color>();

        for ( int x = 0; x < width; x++ )
        {
            for ( int y = 0; y < height; y++ )
            {
                var color = texture.GetPixel( x, y );

                if ( !colors.Contains( color ) )
                {
                    colors.Add( color );
                }
            }
        }
        
        foreach ( var color in colors )
        {
            if ( settings.Fields.Any( i => i.Color == color ) )
            {
                continue;
            }

            settings.Fields.Add( new MappingField { Color = color, Id = "null" } );
        }
    }

    private static bool TryLoadTexture( string filePath, out Texture2D texture )        
    {
        texture = new Texture2D( 2, 2 );

        if ( !File.Exists( filePath ) )
        {
            Debug.LogError( $"File not found: {filePath}" );
            return false;
        }

        byte[] fileData = File.ReadAllBytes( filePath );

        
        if ( !texture.LoadImage( fileData ) )
        {
            Debug.LogError( "Failed to load image." );
            return false;
        }

        return true;
    }


    public static string[,] ImportLevel( string filePath, MappingSettings settings )
    {
        if ( !TryLoadTexture( filePath, out var texture ) )
        {
            throw new Exception($"Can't get texture from {filePath}");
        }

        int width = texture.width;
        int height = texture.height;
        string[,] result = new string[ width, height ];

        var mappingDict = new Dictionary<Color, string>();
        try
        {
            mappingDict = settings.Fields.ToDictionary( i => i.Color, i => i.Id );
        }
        catch ( Exception e )
        {
            Debug.LogError( $"Can't cast {settings.name} MappingSettings.Fields as dictionary." );
            throw e;            
        }



        for ( int x = 0; x < width; x++ )
        {
            for ( int y = 0; y < height; y++ )
            {
                var color = texture.GetPixel( x, y );

                result[x,y] = mappingDict[color];
            }
        }

        return result;
    }

}
