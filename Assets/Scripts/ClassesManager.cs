using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClassesManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI textUser;
    [SerializeField] Transform spawnBtn;
    [SerializeField] GameObject btnClassPrefab;
    Button[] buttonsClass;


    private void Awake()
    {
        textUser.text += NetworkManager.instance.GetCurrentUser().GetNickName();
    }
    private void Start()
    {
        Invoke("AddClasses",1f);
    }

    //A?adir las classes en la UI
    void AddClasses()
    {
        foreach (Class c in NetworkManager.instance.GetAvaiableClasses())
        {
            GameObject btn = Instantiate(btnClassPrefab, spawnBtn);
            
            btn.GetComponentInChildren<TextMeshProUGUI>().text = c.GetNameClass();
            btn.GetComponent<ButtonClass>().SetClass(c);
        }
        RegisterEventsOnButtons();
    }
    
    void RegisterEventsOnButtons()
    {
        buttonsClass = spawnBtn.GetComponentsInChildren<Button>();
        foreach (Button btn in buttonsClass)
        {
            ButtonClass btnClass = btn.GetComponent<ButtonClass>();
            btn.onClick.AddListener(btnClass.OnClick);

        }
    }
}
