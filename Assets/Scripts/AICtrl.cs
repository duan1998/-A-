using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtrl : MonoBehaviour
{
    [HideInInspector]
    public bool m_isCanAction=false;


    private void Update()
    {
        if(m_isCanAction&&Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.Instance.NextTurn();
        }
    }
}
