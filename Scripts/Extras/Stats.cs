using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class Stats
{
    private int health;
    private float speed;
    private int damage;

    public Stats(int health, float speed, int damage)
    {
        this.health = health;
        this.speed = speed;
        this.damage = damage;
    }

    public Stats(float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    public Stats(int health)
    {
        setHealth(health);
    }

    public Stats()
    {

    }

    public void setHealth(int health)
    {
        this.health = health;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setDamage(int damage)
    {
        this.damage = damage;
    }

    public int getHealth()
    {
        return health;
    }

    public float getSpeed()
    {
        return speed;
    }

    public int getDamage()
    {
        return damage;
    }

    public void addHealth()
    {
        //health += (int)Mathf.Ceil((15 - health) / 2f);
        health += 2;
    }

    public void takeDamage(int dmg)
    {
        health -= dmg;
        AudioManager.Instance.Play("hit");
    }
}
