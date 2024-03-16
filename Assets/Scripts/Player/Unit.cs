using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int currHealth;
    public int Damage;
    public int maxHealth;
    public int Score;

    public Unit(int currHealth, int Damage, int maxHealth, int Score) //Builder
    {
        this.currHealth = currHealth;
        this.Damage = Damage;
        this.maxHealth = maxHealth;
        this.Score = Score;
    }
}
