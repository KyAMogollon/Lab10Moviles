using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Data", order = 1)]
public class SaveDataSO : ScriptableObject
{
    public string email;
    
    public void SetEmail(string text)
    {
        email = text;
    }
}
