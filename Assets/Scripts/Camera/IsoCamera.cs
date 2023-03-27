using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoCamera : MonoBehaviour
{
    //player transform
    Transform _playerTransform;
    Transform _dialogueFocusTransform;

    bool _isOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _isOnPlayer = true;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerTransform.GetComponent<PlayerInput>().GetCanInteract() == false)
        {
            //update the positon
            transform.position = _playerTransform.position;
        }
        else
        {
            _dialogueFocusTransform = _playerTransform.GetComponent<PlayerInput>().GetDialogueTransform();
            transform.position = _dialogueFocusTransform.position;
        }


        
    }
}
