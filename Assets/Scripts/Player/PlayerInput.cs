using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    //Private Variables
    Transform _cam;// A reference to the main camera in the scenes transform
    Vector3 _camForward;// The current forward direction of the camera
    Vector3 _move;
    bool _isMoving;
    public  Transform _dialogueTransform;// Changes de focus of the camera to the dialogue position

    bool _canInteract = false;//Verify if the player can interct with the object or NPC

    #region Components
    //Components
    Movement _movement;
    Combos _combos;
    #endregion

    Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    RaycastHit cameraRayHit;    // The object that the ray hits

    // Start is called before the first frame update
    void Start()
    {
        _combos = GetComponent<Combos>();
        // get the transform of the main camera
        if (Camera.main != null)
        {
            _cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        _movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        

        #region Combos
        //Combo
        //_combos.CanComboCheck();
        _combos.CheckCombo();
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse cursor
            cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // If the ray strikes an object...
            if (Physics.Raycast(cameraRay, out cameraRayHit))
            {
                // ...and if that object is the ground...
                if (cameraRayHit.transform.tag == "Ground")
                {
                    // ...make the cube rotate (only on the Y axis) to face the ray hit's position 
                    Vector3 targetPosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                    transform.LookAt(targetPosition);
                }
            }
            _combos._canCombo = true;
            //_combos.CheckCombo();
            print("Start");
            StartCoroutine("WaitFortTheInput");
            _combos.StartCoroutine("Combo");
            //_combos.AttackAnimationEvent();

        }
        _combos.ComboUpdate();
        #endregion
    }

    private void FixedUpdate()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // calculate move direction to pass to character
        if (_cam != null)
        {
            // calculate camera relative direction to move:
            _camForward = Vector3.Scale(_cam.forward, new Vector3(1, 0, 1)).normalized;
            _move = v * _camForward + h * _cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            _move = v * Vector3.forward + h * Vector3.right;
        }
        if (_move.magnitude > 0)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving= false;
        }
        // pass all parameters to the Movement script
        _movement.Move(_move, _isMoving);

        
    }

    #region Get&Set
    public bool GetCanInteract()
    {
        return _canInteract;
    }
    public void SetCanInteract(bool canInteract)
    {
        _canInteract = canInteract;
    }
    public Transform GetDialogueTransform()
    {
        return _dialogueTransform;
    }
    public void SetDialogueTransform(Transform dialogueTransform)
    {
        _dialogueTransform = dialogueTransform;
    }
    public bool GetIsMoving()
    {
        return _isMoving;
    }
    #endregion
    //Controll the input
    IEnumerator WaitFortTheInput()
    {

        yield return new WaitForSeconds(.5f);
    }
}
