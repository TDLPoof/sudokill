using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveController : MonoBehaviour
{
    public TextMeshProUGUI waveDisplayText, enemiesLeftText;
    public int wave = 1;
    public GameObject[] enemies;
    public KillsManager kills;
    public Vector3 waveCenter = new Vector3(25, 25, 25);
    public int enemiesInThisWave;
    public List<GameObject> currentEnemies;
    
    public void StartNewWave()
    {
        currentEnemies = new List<GameObject>();
        enemiesInThisWave = 0;
        kills.killsThisWave = 0;
        wave++;
        waveDisplayText.text = "W A V E   "  + wave;
        waveDisplayText.GetComponent<Animator>().Play("WaveStart");
        foreach (int enemy in GetEnemiesForWave(3 * wave))
        {
            Vector3 randomSpot = new Vector3(2 * Random.value - 1, 0, 2 * Random.value - 1);
            //if (enemies[enemy].GetComponent<FlyingPathFinder>() != null) randomSpot.y = -1f;
            currentEnemies.Add(Instantiate(enemies[enemy], waveCenter + (15 * randomSpot), Quaternion.identity));
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
            StartNewWave();
        }
        enemiesLeftText.text = "ENEMIES REMAINING: " + currentEnemies.Count;
    }
}
