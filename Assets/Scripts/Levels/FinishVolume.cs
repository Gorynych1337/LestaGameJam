using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishVolume : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.FinishLevel();
        GameManager.Instance.FadeWithLoadScene(nextLevelName);
    }
}
