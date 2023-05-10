using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IsoCamera : MonoBehaviour
{
    
    [SerializeField]Transform _playerTransform;//player transform
    [SerializeField]PlayerInput _playerInput;
    Transform _dialogueFocusTransform;
    [SerializeField]Camera _camera;

    public static int AlphaClipping = Shader.PropertyToID("_Alpha");
    public Material WallMaterial;
    public Camera Camera;
    public LayerMask LayerMask;

    float amount = 0;
    float increaseAmount = .9f;


    bool _isOnPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerInput = _playerTransform.GetComponent<PlayerInput>();
        _isOnPlayer = true;
    }

    private void Update()
    {
        var dir = Camera.transform.position - transform.position;
        Ray ray = new Ray(transform.position, dir.normalized);
        RaycastHit hit;
        if (Physics.Raycast(ray, 3000f, LayerMask))
        {
            if (amount < 1)
            {
                WallMaterial.SetFloat(AlphaClipping, amount += increaseAmount * Time.deltaTime);
            }
            else WallMaterial.SetFloat(AlphaClipping, 1f);

        }
        else
        {
            if (amount >= 0)
            {
                WallMaterial.SetFloat(AlphaClipping, amount -= increaseAmount * Time.deltaTime);
            }
            else
            {
                WallMaterial.SetFloat(AlphaClipping, 0);
                amount = 0;
            }

        }
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
            //_camera.orthographicSize = 3;
            //transform.position = _dialogueFocusTransform.position;
            

        }

        


        
    }
}
