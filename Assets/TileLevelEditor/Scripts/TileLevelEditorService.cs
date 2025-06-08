using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using TMPro;
using UnityEngine.UI;

public class TileLevelEditorService : MonoBehaviour
{
    [Header( "UI" )]
    [SerializeField]
    private TMPro.TMP_Text _atlasPath;
    [SerializeField]
    private Button _openAtlasButton;

    private string _filesInitialPath;

    void Start()
    {
        _atlasPath.text = "Select atlas png";

        _openAtlasButton.onClick.RemoveAllListeners();
        _openAtlasButton.onClick.AddListener(ShowOpenAtlasPanel);

    }

    private void ShowOpenAtlasPanel()
    {
        if ( string.IsNullOrEmpty( _filesInitialPath ) )
        {
            _filesInitialPath = Application.dataPath;
        }

        FileBrowser.SetFilters( false, ".png" );
        FileBrowser.ShowLoadDialog( OnAtlasSelected, OnAtlasSelectionCancel, FileBrowser.PickMode.Files, false, _filesInitialPath );
    }

    private void OnAtlasSelected( string[] paths ) 
    {
        if ( paths.Length < 1 )
        {
            throw new System.Exception( "Invalid path. There is no path to atlas" );
        }

        var filePath = paths[0];

        if ( !System.IO.File.Exists( filePath ) )
        {
            throw new System.Exception($"File on path {filePath} not exist");
        }

        _filesInitialPath = System.IO.Path.GetFullPath( paths[0] );
    }

    private void OnAtlasSelectionCancel() { }


    private void TryLoadAtlas(string pngPath) 
    {
        _atlasPath.text = pngPath;

        //Смотрим есть ли настройка разметки


        //Если разметки нет, создаем новую
        //Загружаем тайлы согласно разметки
    }

    private bool IsMarkupExist(string pngFilePath) 
    {
           
    }

    private string GetMarkupPath( string atlasPngPath ) 
    {
        var directory = System.IO.Path.GetDirectoryName( atlasPngPath );
        var fileName = System.IO.Path.GetFileNameWithoutExtension( atlasPngPath );
        return System.IO.Path.Combine( directory, fileName, ".tlm" );
    }

    
}
