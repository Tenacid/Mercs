using System.Collections.Generic;
using UnityEngine;

namespace SevenMagpies.LevelMapping
{
    public class LevelData : ScriptableObject
    {
        [SerializeField]
        private int _width;

        [SerializeField]
        private int _height;

        [SerializeField]
        private List<string> _data;

        
        

        public void FillData( string[,] incomingData )
        {
            _width = incomingData.GetLength( 0 );
            _height = incomingData.GetLength( 1 );

            _data = new List<string>();
            for ( var x = 0; x < incomingData.GetLength( 0 ); x++ )
            {
                for ( var y = 0; y < incomingData.GetLength( 1 ); y++ )
                {
                    _data.Add( incomingData[ x, y ] );
                }
            }
        }
    }
}
