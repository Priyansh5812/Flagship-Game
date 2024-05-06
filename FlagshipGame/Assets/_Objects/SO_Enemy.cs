using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{ 
    BAT,
    BEE,
    DRAGON_RIDER
}

[CreateAssetMenu(menuName = "Enemy")]
public class SO_Enemy : ScriptableObject
{
    public int Health;
    public int Damage;
    public AudioClip sfx;
    public EnemyType type;
}
