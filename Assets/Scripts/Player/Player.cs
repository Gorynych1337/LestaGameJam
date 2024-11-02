using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _hp;

    public float Hp => _hp;

    private void Resize()
    {
        transform.localScale = Vector3.one * _hp / 100; 
    }

    public void GetHealed(float heal)
    {
        _hp += heal;
        Resize();
    }

    public void GetDamage(float damage)
    {
        if (_hp - damage <= 0)
            Debug.Log($"You died, got damage {damage}, actual hp: {_hp - damage}");
        else
        {
            _hp -= damage;
            Resize();
            Debug.Log($"Actual hp: {_hp}");
        }
    }
}
