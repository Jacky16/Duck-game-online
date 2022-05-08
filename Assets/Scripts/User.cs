using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User: ScriptableObject
{
    string nick;
    int idInDatabase;
    Avatar avatar;


    public User(string username,int id)
    {
        this.nick = username;
        this.idInDatabase = id;
    }

    public string GetNick()
    {
        return nick;
    }
    public int GetId()
    {
        return idInDatabase;
    }
    public void SetAvatar(Avatar avatar)
    {
        this.avatar = avatar;
    }

}
