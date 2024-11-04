using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private Transform dropSpawn;
    [SerializeField] private float shotDelay;
    [SerializeField] private float shotCount;
    [SerializeField] private float shootCooldown;
    [SerializeField] private Animator animator; 

    private bool isShooting;

    private IEnumerator ShootCorutine()
    {
        isShooting = true;
        for (int i = 0; i < shotCount; i++)
        {
            StartCoroutine("Shoot");
            yield return new WaitForSeconds(shotDelay);
        }

        yield return new WaitForSeconds(shootCooldown);
        isShooting = false;
    }

    private void SpawnDrop()
    {
        var drop = Instantiate(dropPrefab, dropSpawn.position, dropSpawn.rotation);
        drop.GetComponent<DropFlight>().Instantiate(dropSpawn.TransformDirection(Vector3.right).normalized);
    }

    private IEnumerator Shoot()
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips.First(a => a.name == "FlowerShot").length);
        animator.SetBool("isAttack", false);
        SpawnDrop();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isShooting) return;
        if (!collision.TryGetComponent(out PlayerComponent target)) return;
        StartCoroutine("ShootCorutine");
    }
}
