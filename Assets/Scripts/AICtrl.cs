using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICtrl : MonoBehaviour
{
    [HideInInspector]
    public bool m_isCanAction=false;

    public int m_hp = 10;

    public GameObject m_restartButtonObj;

    public void Hit(int damage)
    {
        if (m_hp <= damage)
        {
            //出现YouWin
            m_restartButtonObj.SetActive(true);
        }
        else
            m_hp -= damage;
    }

    private void Start()
    {

        MapMgr.Instance.m_mapItems[(int)transform.position.x,(int)transform.position.z].m_currentType=MapItemType.Enemy;
    }
    private void Update()
    {
        if(m_isCanAction&&Input.GetKeyDown(KeyCode.Tab))
        {
            GameManager.Instance.NextTurn();
        }
    }
}
