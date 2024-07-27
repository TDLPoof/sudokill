using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public float damage = 1f;
    public bool projectile;
    public bool friendlyFire;

    [HideInInspector]
    public KillsManager killsManager;
}
