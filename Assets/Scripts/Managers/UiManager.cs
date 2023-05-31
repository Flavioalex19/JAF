using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    #region InteractionText
    bool _isInteractionTextOn = false;

    #endregion

    [Header("Dialogue")]
    [SerializeField] Animator _dialogueAnimator;
    bool _isDialoguePanelOn = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isInteractionTextOn)
        {

        }


        if (_isDialoguePanelOn)
        {
            _dialogueAnimator.SetBool("isOn", true);
        }
        else _dialogueAnimator.SetBool("isOn", false);
    }

    #region Get& Set
    public bool GetIsInteractionTextOn()
    {
        return _isInteractionTextOn;
    }
    public void SetIsInteractionTextOn(bool isInteractionTextOn)
    {
        _isInteractionTextOn=isInteractionTextOn;
    }
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
