using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _hp;

    public float Hp => _hp;

    public void GetDamage(float damage)
    {
        if (_hp - damage < 0)
            Debug.LogError($"You died, got damage {damage}, actual hp: {_hp - damage}");
        else
        {
            _hp -= damage;
            Debug.LogError($"Actual hp: {_hp}");
        }
    }
}
