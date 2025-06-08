using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace SevenMagpies.LevelMapping
{
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;

        [SerializeField]
        private List<string> _surface;

        [SerializeField]
        private List<string> _background;

        [SerializeField]
        private List<string> _entities;



        public void FillData( string[,] surface, string[,] background, string[,] entities )
        {
            _width = surface.GetLength( 0 );
            _height = surface.GetLength( 1 );

            FillListFromArray( _surface, surface );
            FillListFromArray( _background, background );
            FillListFromArray( _entities, entities );
        }

        private void FillListFromArray<T>(List<T> list, T[,] array) 
        {
            list = new List<T>();
            for ( var x = 0; x < array.GetLength( 0 ); x++ )
            {
                for ( var y = 0; y < array.GetLength( 1 ); y++ )
                {
                    list.Add( array[ x, y ] );
                }
            }
        }

    }
}
