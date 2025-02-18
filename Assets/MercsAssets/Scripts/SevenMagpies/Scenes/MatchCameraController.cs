using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchCameraController : MonoBehaviour
{
    Camera _camera;

    [SerializeField]
    private Vector2 _screenBaseSize;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void CorrectCameraSize()     
    {
        var ratio = 1;

        bool isVertical = Screen.height > Screen.width;
        if ( isVertical )
        {
            ratio = Screen.width / Screen.height;
        }
        else
        { 
            ratio = Screen.height / Screen.width;
        }

        _camera.orthographicSize = ratio;

    }
}
