using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("Player");
            other.GetComponent<PlayerInput>().SetCanInteract(true);
            other.GetComponent<PlayerInput>().SetDialogueTransform(transform);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerInput>().SetCanInteract(false);
    }


}
