using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockThrower : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private Transform rockSpawner;

    private List<GameObject> rocks;
    private Queue<GameObject> rockQueue;

    private void Awake()
    {
        rocks = new List<GameObject>();
        rockQueue = new Queue<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            Throw();
        }
    }

    private void Throw()
    {
        GameObject rock;

        if (!rockQueue.TryDequeue(out rock))
        {
            rock = Instantiate(rockPrefab);
            rocks.Add(rock);
        }
        else
        {
            rock.SetActive(true);
        }
        rock.transform.position = rockSpawner.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var rock = collision.gameObject;
        if (rocks.IndexOf(rock) == -1) return;

        rockQueue.Enqueue(rock);
        rock.SetActive(false);
    }
}
