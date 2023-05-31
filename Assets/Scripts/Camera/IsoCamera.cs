using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class IsoCamera : MonoBehaviour
{
    
    [SerializeField]Transform _playerTransform;//player transform
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
        transform.position = _playerTransform.position;

        // Check for input to rotate the object
        if (Input.GetKey(KeyCode.Q))
        {
            RotateObject(-1f);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            RotateObject(1f);
        }

    }

    private void RotateObject(float direction)
    {
        // Set the desired rotation angle for the object
        float rotationAngle = 2f * direction; // Adjust the rotation speed as desired

        // Rotate the object around the player's position
        transform.RotateAround(_playerTransform.position, Vector3.up, rotationAngle);
    }


}
