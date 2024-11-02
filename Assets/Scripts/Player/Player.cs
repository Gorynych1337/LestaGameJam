using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float minHP;
    [SerializeField] private float maxHP;

    public float Hp => _hp;

    private void Resize()
    {
        transform.localScale = Vector3.one * _hp / 100; 
    }

    public void GetHealed(float heal)
    {
        _hp += heal;
        _hp = MathF.Min(maxHP, _hp);
        Resize();
    }

    public void Death()
    {
        // Так вообще не нужно, но как заглушка
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GetDamage(float damage)
    {
        if (_hp - damage <= minHP) Death();
        else
        {
            _hp -= damage;
            Resize();
            Debug.Log($"Actual hp: {_hp}");
        }
    }
}
