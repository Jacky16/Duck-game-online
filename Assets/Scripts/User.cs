using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User: ScriptableObject
{
    string nick;
    int idInDatabase;
    Class classs;


    public User(string username,int id)
    {
        this.nick = username;
        this.idInDatabase = id;
    }

    public string GetNickName()
    {
        return nick;
    }
    public int GetId()
    {
        return idInDatabase;
    }
    public void SetClass(Class _class)
    {
        this.classs = _class;
    }
    public Class GetClass()
    {
        return classs;
    }

}
