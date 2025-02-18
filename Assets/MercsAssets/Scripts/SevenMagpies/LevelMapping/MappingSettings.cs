using System.Collections.Generic;
using UnityEngine;


namespace SevenMagpies.LevelMapping
{
    [CreateAssetMenu( fileName = "MappingSettings", menuName = "LevelMapping/New MappingSettings", order = 1 )]
    public class MappingSettings : ScriptableObject
    {
        public List<MappingField> Fields;
    }
}