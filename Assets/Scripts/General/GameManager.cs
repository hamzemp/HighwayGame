using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;


    [SerializeField] Transform baseRoad;

    float zOffset = 70;

    [SerializeField]
    GameObject GameOverPanel;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void PoolRoad()
    {
        ObjectPooler.instance.SpawnFromPool("Roads", new Vector3(baseRoad.transform.position.x,
            baseRoad.transform.position.y,
            baseRoad.transform.position.z + zOffset),
            baseRoad.localRotation);

        zOffset += 70;
    }
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
    }
    public void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
