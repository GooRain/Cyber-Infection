using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour {

    public EnemysPool Pool { get; set; }

    void Start ()
    {
        StartCoroutine(SpawnControl());
    }

    private void Awake()
    {
        Pool = GetComponent<EnemysPool>();
    }

    private IEnumerator SpawnControl()
    {
        int enemyIndex = Random.Range(0, 4);
        string type = string.Empty;
        switch (monsterIndex)
        {
            case 0:
                type = "blue_enemy";
                break;
            case 1:
                type = "orange_enemy";
                break;
            case 2:
                type = "purple_enemy";
                break;
            case 3:
                type = "red_enemy";
                break;
        }

        Pool.GetObject(type);
        yield return new WaitForSeconds(1.0f);
    }
}
