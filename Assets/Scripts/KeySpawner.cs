using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public Transform[] keySpawnpoints;
    public Key key;

    private void Start()
    {
        int randomSpawnpoint = Random.Range(0, keySpawnpoints.Length);

        Instantiate(key, keySpawnpoints[randomSpawnpoint].position, Quaternion.identity);
    }
}
