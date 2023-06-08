using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    GameObject cc_player;

    [SerializeField] Animator _interactionText_Animator;

    [Header("Dialogue")]
    [SerializeField] Animator _dialogue_Animator;
    bool _isDialoguePanelOn = false;

    

    // Start is called before the first frame update
    void Start()
    {
        cc_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        _interactionText_Animator.SetBool("isOn", cc_player.GetComponent<PlayerInput>().GetCanInteract());
        

        if (_isDialoguePanelOn)
        {
            _dialogue_Animator.SetBool("isOn", true);
        }
        else _dialogue_Animator.SetBool("isOn", false);
    }

    #region Get& Set
   
    public bool GetIsDialoguePanelOn()
    {
        return _isDialoguePanelOn;
    }
    public void SetIsDialoguePanelOn(bool isDialoguePanelOn)
    {
        _isDialoguePanelOn=isDialoguePanelOn;
    }

    #endregion

}
