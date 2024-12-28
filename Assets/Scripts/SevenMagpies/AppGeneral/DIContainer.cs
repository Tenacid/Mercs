using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SevenMagpies.AppGeneral
{
    public static class DIContainer
    {
        private static Dictionary<Type, object> _bindings;
        private static Dictionary<Type, Dictionary<string, object>> _bindingsTags;
        private static Dictionary<object, IContext> _contextOwnership;

        static DIContainer()
        {
            _bindings = new Dictionary<Type, object>();
            _bindingsTags = new Dictionary<Type, Dictionary<string, object>>();
            _contextOwnership = new Dictionary<object, IContext>();
        }

        public static void Bind<T>( T instance, IContext ownerContext )
        {
            var type = typeof( T );
            _bindings[ type ] = instance;
            _contextOwnership[ instance ] = ownerContext;
        }

        public static void Bind<T>( T instance, string tag, IContext ownerContext )
        {
            var type = typeof( T );
            if ( !_bindingsTags.ContainsKey( type ) )
            {
                _bindingsTags.Add( type, new Dictionary<string, object>() );
            }

            _bindingsTags[ type ][ tag ] = instance;
            _contextOwnership[ instance ] = ownerContext;
        }

        public static T Get<T>()
        {
            var type = typeof( T );

            if ( !_bindings.TryGetValue( type, out var result ) )
            {
                UnityEngine.Debug.LogError( $"DIContainer.Get can't find object of type {typeof( T )}" );
                throw new Exception( $"DIContainer.Get can't find object of type {typeof( T )}" );
            }

            return ( T ) result;
        }

        public static List<T> GetAll<T>( object byContext )
        {
            var result = new List<T>();

            foreach ( var bindingkv in _bindings )
            {
                var instance = bindingkv.Value;
                if ( byContext != null && _contextOwnership[ instance ] != byContext )
                {
                    continue;
                }

                if ( bindingkv.Value is T casted )
                {
                    result.Add( casted );
                }
            }

            return result;
        }

        public static void CleartByContext( object byContext )
        {
            var toClear = new List<Type>();

            foreach ( var bindingkv in _bindings )
            {
                var instance = bindingkv.Value;
                if ( byContext != null && _contextOwnership[ instance ] != byContext )
                {
                    continue;
                }

                toClear.Add( bindingkv.Key );
            }

            foreach ( var cType in toClear )
            {
                var instance = _bindings[ cType ];
                _bindings.Remove( cType );
                _bindingsTags.Remove( cType );
                _contextOwnership.Remove( instance );
            }
        }

        public static void ClearCache()
        {
            foreach ( var bindingkv in _bindings )
            {
                var instance = bindingkv.Value;
                if ( instance is IContextCacheHolder cacheHolder )
                {
                    cacheHolder.ClearCache();
                }
            }
        }

        public static T Get<T>( string tag )
        {
            var type = typeof( T );
            return ( T ) _bindingsTags[ type ][ tag ];
        }

    }
}