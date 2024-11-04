using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishVolume : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.FinishLevel();
    }
}
