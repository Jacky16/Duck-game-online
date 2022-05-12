using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ButtonClass : MonoBehaviour
{
    Class currentClass;

   
    public ButtonClass(Class currentClass)
    {
        this.currentClass = currentClass;
    }

    public Class GetCurrentClass(){
        return currentClass;
    }

    public void SetClass(Class c)
    {
        currentClass = c;
    }
    public void OnClick()
    {
        Debug.Log(currentClass.GetNameClass());
        
        //Enviamos la clase al servidor para que la añada a la BDD
        NetworkManager.instance.SendInfoToAddClassAndPlayer(currentClass.GetNameClass());
        
        //Asignamos la clase seleccionada al jugador
        NetworkManager.instance.AssingClassToUser(currentClass);
        
        NetworkManager.instance.LoadRoomsScene();
    }

}
