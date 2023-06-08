using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{

    protected bool _canStartInteraction = false;//If the interaction can start
    [SerializeField]protected GameObject _mainCharacter;

    //UI


    [SerializeField]protected UiManager _uiManager;


    private void Start()
    {
        _uiManager = GameObject.Find("UI Manager").GetComponent<UiManager>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            _mainCharacter = other.gameObject;
            other.GetComponent<PlayerInput>().SetCanInteract(true);
            
            /*
            _canStartInteraction = true;
            _mainCharacter = other.gameObject;
            other.GetComponent<PlayerInput>().SetCanInteract(true);
            //other.GetComponent<PlayerInput>().SetDialogueTransform(transform);
            //other.GetComponent<PlayerInput>().SetDialogueTransform(transform);
            _uiManager.SetIsDialoguePanelOn(true);
            */
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerInput>().SetCanInteract(false);
        /*
        _canStartInteraction = false;
        _mainCharacter = null;
        other.GetComponent<PlayerInput>().SetCanInteract(false);
        _uiManager.SetIsDialoguePanelOn(false);
        */
    }


}
