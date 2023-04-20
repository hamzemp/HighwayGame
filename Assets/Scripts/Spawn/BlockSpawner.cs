using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Vector3 randpos;

    private void Start()
    {
        InvokeRepeating("Spawn", 1, 2);
    }

    private void Spawn()
    {
        Randomizer();
        ObjectPooler.instance.SpawnFromPool("Blocks",randpos,Quaternion.identity);
    }
    void Randomizer()
    {
        randpos.y = transform.position.y;
        randpos.x = Random.Range(player.localPosition.x - 7, player.localPosition.x + 7);

        randpos.z = Random.Range(player.localPosition.z + 20, player.localPosition.z + 35);
    }
}
