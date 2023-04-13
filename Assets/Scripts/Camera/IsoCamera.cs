using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour
{
    
    [SerializeField]Transform _playerTransform;//player transform
    PlayerInput _playerInput;
    Transform _dialogueFocusTransform;
    [SerializeField]Camera _camera;

    bool _isOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerInput = _playerTransform.GetComponent<PlayerInput>();
        _isOnPlayer = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerInput.GetCanInteract() == false)
        {
            _camera.orthographicSize = 5;
            //update the positon
            transform.position = _playerTransform.position;
        }
        else
        {
            _dialogueFocusTransform = _playerInput.GetDialogueTransform();
            _camera.orthographicSize = 3;
            transform.position = _dialogueFocusTransform.position;
        }

        


        
    }
}
