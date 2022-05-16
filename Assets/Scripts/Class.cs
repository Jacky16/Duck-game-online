using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class
{
    public int idDatabase;
    public string name;
    public float life;
    public float speed;
    public float fireRate;
    public float damage;
    public enum ClassType { HEAVY, LIGHT }
    ClassType classType;
    public Class()
    {
        
    }

    public Class(string name,float speed, float fireRate, float life,float damage, int idBDD)
    {
        this.name = name;
        this.speed = speed;
        this.life = life;
        this.fireRate = fireRate;
        this.damage = damage;
        this.idDatabase = idBDD;

        switch (name)
        {
            case "Heavy":
                classType = ClassType.HEAVY;
                break;
            case "Light":
                classType = ClassType.LIGHT;
                break;
        }

    }
    public string GetNameClass(){
        return name;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetFireRate()
    {
        return fireRate;
    }
    public float GetLife()
    {
        return life;
    }
    public ClassType GetClassType()
    {
        return classType;
    }
    public void SetClass(ClassType classType)
    {
        this.classType = classType;
    }
    public float GetDamage()
    {
        return damage;
    }
    public int GetIdBDD()
    {
        return idDatabase;
    }
}
