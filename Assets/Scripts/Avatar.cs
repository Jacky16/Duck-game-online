using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : ScriptableObject
{
    string name;
    float life;
    float speed;
    float fireRate;
    public enum ClassType { HEAVY, LIGHT }
    ClassType classType;

    public Avatar(string name,float speed, float fireRate, float life)
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
    public ClassType GetClass()
    {
        return classType;
    }
    public void SetClass(ClassType classType)
    {
        this.classType = classType;
    }
}
