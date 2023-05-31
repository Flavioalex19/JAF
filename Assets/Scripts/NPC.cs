using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactables
{

    [SerializeField]List<string> _npcLines = new List<string>();
    public int _Myindex = 0;

    DialogueManager _dialogueManager;

    private void Start()
    {
        _dialogueManager = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();

    }

    private void Update()
    {
        
        if (_mainCharacter != null)
        {
            
            if (Input.GetKeyUp(KeyCode.Space))
            {

                if (_Myindex < _npcLines.Count)
                {
                    _mainCharacter.GetComponent<PlayerInput>().enabled = false;
                    print("NPC");
                    _uiManager.SetIsDialoguePanelOn(true);
                    _dialogueManager.Dialogue(_npcLines, _Myindex, _uiManager.GetIsDialoguePanelOn());
                    _Myindex++;
                }
                else
                {
                    _uiManager.SetIsDialoguePanelOn(false);
                    _Myindex = 0;
                    _mainCharacter.GetComponent<PlayerInput>().enabled = true;
                }
                


            }
            //else _Myindex = 0;
        }
    }

    

}
