using System.Collections;
using UnityEngine;

public class PillarSpawner : MonoBehaviour
{
    [SerializeField] private Transform pillarSpawner;
    [SerializeField] private GameObject pillar;
    
    private readonly float maxY = 3f;
    private readonly float minY = -3f;

    private readonly float spawnDelay = 2f;

    private void OnEnable()
    {
        StartCoroutine(SpawnPillar());
    }

    IEnumerator SpawnPillar()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            Vector3 randomPosition = new(0, Random.Range(minY, maxY), 0);
            Instantiate(pillar, pillarSpawner.position + randomPosition, Quaternion.identity, pillarSpawner);
        }
    }

    private void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        DestroyAllChildren(pillarSpawner);
    }
}
