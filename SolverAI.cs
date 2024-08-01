using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolverAI : MonoBehaviour
{
    public ParticleSystem doneParticle;
    public KillsManager killsManager;
    public Enemy currentTarget;
    public int enemiesToSolve = 10;
    public float postKillPauseTimer = 0.5f;
    float pauseTimer;
    Transform originalParent;

    // Start is called before the first frame update
    void Start()
    {
        originalParent = transform.parent;
        transform.SetParent(null);
        killsManager = GameObject.Find("Kills").GetComponent<KillsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null) RecalculateTarget();
        if (enemiesToSolve <= 0)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector3(0, 1, -2), 3f * Time.deltaTime);
            if (Utils.DistanceSQ(transform.localPosition, new Vector3(0, 1, -2)) < 0.0625f)
            {
                doneParticle.Play();
                Destroy(gameObject);
            }
        }
        else if (pauseTimer > postKillPauseTimer)
        {
            transform.LookAt(currentTarget.transform);
            transform.Translate(transform.forward * 24 * Time.deltaTime);
            transform.position = currentTarget.transform.position;
            currentTarget.health = 0f;
            killsManager.kills++;
            killsManager.killsThisWave++;
            enemiesToSolve--;
            currentTarget = null;
            pauseTimer = 0;
        }
        pauseTimer += Time.deltaTime;
    }

    void RecalculateTarget()
    {
        foreach (Enemy enemy in GameObject.FindObjectsOfType<Enemy>())
        {
            if (currentTarget == null ||
                Utils.DistanceSQ(enemy.transform.position, transform.position) >
                Utils.DistanceSQ(currentTarget.transform.position, transform.position))
            {
                if (enemy.maxHealth > 10) continue;
                currentTarget = enemy;
            }
        }
    }
}
