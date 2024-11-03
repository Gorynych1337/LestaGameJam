using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropSpawn;
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
        var drop = Instantiate(dropPrefab, dropSpawn.position, dropSpawn.rotation);
        drop.GetComponent<DropFlight>().Instantiate(dropSpawn.TransformDirection(Vector3.right).normalized);
        Debug.Log("Shoot" + temp);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isShooting) return;
        StartCoroutine("ShootCorutine");
    }

    private void Update()
    {
        if (isShooting) return;
        StartCoroutine("ShootCorutine");
    }
}
