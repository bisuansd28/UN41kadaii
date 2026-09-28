using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // 生成するコイン
    public GameObject coinPrefab;

    // 生成する数
    public int coinCount = 3;

    // 生成範囲
    public float minX = -4f;
    public float maxX = 4f;
    public float minZ = -4f;
    public float maxZ = 4f;

    void Start()
    {
        SpawnCoin();
    }

    public void CoinRespawn()
    {
        Debug.Log("CoinRespawnが呼ばれた！");
        SpawnCoin();
    }

    void SpawnCoin()
    {
        DeleteCoins();
        
        for (int i = 0; i < coinCount; i++)
        {
            float x = Random.Range(minX, maxX);
            float z = Random.Range(minZ, maxZ);

            Vector3 spawnPosition = new Vector3(
                x,
                0.5f,
                z
            );

            Instantiate(
                coinPrefab,
                spawnPosition,
                coinPrefab.transform.rotation
            );
        }
        
    }

    void DeleteCoins()
    {
        GameObject[] objects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in objects)
        {
            if (obj.name.Contains("Coin"))
            {
                Destroy(obj);
            }
        }
    }
}