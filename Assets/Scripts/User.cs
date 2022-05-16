using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string nick;
    public int idInDatabase;
    public int idClassAssigned;
    public Class classs;

    public User()
    {
        
    }
    public User(string username,int idDB,int idClass)
    {
        this.nick = username;
        this.idInDatabase = idDB;
        this.idClassAssigned = idClass;
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
    public int GetIdClassAssigned()
    {
        return idClassAssigned;
    }

}
