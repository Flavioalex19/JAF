using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combos : MonoBehaviour
{
    #region Variables
    [SerializeField] float _comboTimer;
    [SerializeField] float _comboResetTimer;
    [SerializeField] int _comboCount;
    [SerializeField] int _maxCombo;
    [SerializeField] bool _canCombo = false;
    #endregion

    //test
    float maxComboWindow = .2f;
    float comboWindow;

    //Components
    [SerializeField] Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();

        _comboResetTimer = _comboTimer;
        //test
        comboWindow = maxComboWindow;
    }

    public bool GetCanCombo()
    {
        return _canCombo;
    }
    public void SetCanCombo(bool canCombo)
    {
        _canCombo = canCombo;
    }
    public int GetComboCount()
    {
        return _comboCount;
    }
    public void CheckCombo()
    {
        if (_canCombo)
        {
            if (_comboTimer > 0)
            {
                _comboTimer -= Time.deltaTime;
            }
            else
            {
                _comboTimer = _comboResetTimer;
                _canCombo = false;
                _comboCount = 0;
            }
            
        }
    }

    

    public IEnumerator Combo()
    {
        _comboTimer = _comboResetTimer;
        _canCombo = true;
        _comboCount++;
        if (_comboCount > _maxCombo)
        {
            _comboCount = 0;
            _canCombo = false;
            //ComboUpdate(_animator);
            yield return new WaitForSeconds(3f);
        }


    }

    public void AttackAnimationEvent()
    {
        StartCoroutine("Combo");
        //print(damage);
        //return damage;
    }
    //Update the animations
    public void ComboUpdate()
    {
        _animator.SetBool("CanCombo", _canCombo);
        _animator.SetFloat("ComboCount", _comboCount);

    }
}
