using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public enum TypeOfInteraction { 
        None,
        Item,
        NPC
    
    }

    //public variables
    public TypeOfInteraction MytypeOfInteraction;

    protected bool _canStartInteraction = false;
    [SerializeField]protected GameObject _mainCharacter;

    [SerializeField]protected UiManager _uiManager;


    private void Start()
    {
        _uiManager = GameObject.Find("UI Manager").GetComponent<UiManager>();
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            _canStartInteraction = true;
            _mainCharacter = other.gameObject;
            //Debug.Log("Player");
            other.GetComponent<PlayerInput>().SetCanInteract(true);
            other.GetComponent<PlayerInput>().SetDialogueTransform(transform);
            if (MytypeOfInteraction == TypeOfInteraction.NPC)
            {
                other.GetComponent<PlayerInput>().SetDialogueTransform(transform);
            }
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        _canStartInteraction = false;
        _mainCharacter = null;
        other.GetComponent<PlayerInput>().SetCanInteract(false);
    }


}
