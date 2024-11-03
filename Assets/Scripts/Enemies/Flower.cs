using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private float shotDelay;
    [SerializeField] private float shotCount;
    [SerializeField] private float shootCooldown;

    private bool isShooting;

    private IEnumerator ShootCorutine()
    {
        isShooting = true;
        for (int i = 0; i < shotCount; i++)
        {
            Shoot(i.ToString());
            yield return new WaitForSeconds(shotDelay);
        }

        yield return new WaitForSeconds(shootCooldown);
        isShooting = false;
    }

    private void Shoot(string temp)
    {
        Debug.Log("Shoot" + temp);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isShooting) return;
        StartCoroutine("ShootCorutine");
    }
}
