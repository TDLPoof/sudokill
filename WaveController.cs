using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    public TextMeshProUGUI waveDisplayText, enemiesLeftText;
    public int wave = 1;
    public int bossWaves = 10;
    public GameObject[] enemies;
    public KillsManager kills;
    public Vector3 waveCenter = new Vector3(25, 25, 25);
    public int enemiesInThisWave;
    public List<GameObject> currentEnemies;
    public GameObject bossEnemy;
    
    public void StartNewWave(bool boss = false)
    {
        if (boss)
        {
            enemiesInThisWave = 0;
            kills.killsThisWave = 0;
            wave++;
            currentEnemies = new List<GameObject>();
            currentEnemies.Add(Instantiate(bossEnemy, waveCenter, Quaternion.identity));
            int remainder = ((wave / 10) - 1) * 2;
            MapGenerator map = GameObject.Find("Map").GetComponent<MapGenerator>();
            map.noisemap = new float[map.noisemap.GetLength(0), map.noisemap.GetLength(1)];
            for (int i = 0; i < map.noisemap.GetLength(0); i++)
            {
                map.noisemap[i, 0] = 0.5f;
                map.noisemap[i, map.noisemap.GetLength(1) - 1] = 0.5f;
                map.noisemap[0, i] = 0.5f;
                map.noisemap[map.noisemap.GetLength(0) - 1, i] = 0.5f;
            }
            map.noisemap[0, 0] = 0.7f;
            map.noisemap[map.noisemap.GetLength(0) - 1, 0] = 0.7f;
            map.noisemap[0, map.noisemap.GetLength(1) - 1] = 0.7f;
            map.noisemap[map.noisemap.GetLength(0) - 1, map.noisemap.GetLength(1) - 1] = 0.7f;

            for (int i = 0; i < remainder; i++)
            {
                Vector3 randomSpot = new Vector3(2 * Random.value - 1, 0, 2 * Random.value - 1);
                //if (enemies[enemy].GetComponent<FlyingPathFinder>() != null) randomSpot.y = -1f;
                currentEnemies.Add(Instantiate(enemies[Random.Range(0, enemies.Length)], waveCenter + (10 * randomSpot), Quaternion.identity));
            }
            waveDisplayText.text = "BOSS WAVE   " + (wave / 10);
            waveDisplayText.GetComponent<Animator>().Play("WaveStart");
        }
        else
        {
            currentEnemies = new List<GameObject>();
            enemiesInThisWave = 0;
            kills.killsThisWave = 0;
            wave++;
            waveDisplayText.text = "W A V E   " + wave;
            waveDisplayText.GetComponent<Animator>().Play("WaveStart");
            MapGenerator map = GameObject.Find("Map").GetComponent<MapGenerator>();
            map.noisemap = map.GenerateNoisemap();
            foreach (int enemy in GetEnemiesForWave(3 * wave))
            {
                Vector3 randomSpot = new Vector3(2 * Random.value - 1, 0, 2 * Random.value - 1);
                //if (enemies[enemy].GetComponent<FlyingPathFinder>() != null) randomSpot.y = -1f;
                currentEnemies.Add(Instantiate(enemies[enemy], waveCenter + (15 * randomSpot), Quaternion.identity));
            }
        }
    }

    public List<int> GetEnemiesForWave(int cost)
    {
        List<int> result = new List<int>();
        int totalValue = 0;
        while (totalValue < cost)
        {
            int possibleEnemy = Random.Range(0, enemies.Length);
            if (possibleEnemy + 1 + totalValue > cost) continue;
            if (enemies[possibleEnemy].GetComponent<Enemy>().firstAvailableWave > wave) continue;
            result.Add(possibleEnemy);
            totalValue += 1 + possibleEnemy;
            enemiesInThisWave++;
        }
        return result;
    }

    private void Start()
    {
        StartNewWave();
    }

    private void PruneNullEntries()
    {
        for (int i = currentEnemies.Count - 1; i > -1; i--)
        {
            if (currentEnemies[i] == null) currentEnemies.RemoveAt(i);
        }
    }

    int lerpedEnemies = 0;
    private void Update()
    {
        PruneNullEntries();
        if (currentEnemies.Count == 0)
        {
            if ((wave) % bossWaves == 0) StartNewWave(true);
            else StartNewWave();
        }
        enemiesLeftText.text = "ENEMIES REMAINING: " + currentEnemies.Count;
    }
}
