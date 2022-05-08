using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class : ScriptableObject
{
    string name;
    float life;
    float speed;
    float fireRate;
    public enum ClassType { HEAVY, LIGHT }
    ClassType classType;

    public Class(string name,float speed, float fireRate, float life)
    {
        this.name = name;
        this.speed = speed;
        this.life = life;
        this.fireRate = fireRate;
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
    public ClassType GetClass()
    {
        return classType;
    }
    public void SetClass(ClassType classType)
    {
        this.classType = classType;
    }
}
