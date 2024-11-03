using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserQueueHandler : MonoBehaviour
{
    [SerializeField] private float nextGeyserDelay;
    [SerializeField] private List<GameObject> geysers;

    private void Start()
    {
        StartCoroutine("StartGeysers");
    }

    private IEnumerator StartGeysers()
    {
        foreach (var geyser in geysers)
        {
            geyser.GetComponentInChildren<GeyserResizer>().StartLoop();
            yield return new WaitForSeconds(nextGeyserDelay);
        }
    }
}
