using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMgr : MonoBehaviour
{

    private static MapMgr m_instance;
    public static MapMgr Instance
    {
        get { return m_instance; }
    }



    public int[][] m_mapCost = { 
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    new int[8]{ 1,1,1,1,1,1,1,1},
    } ;
    void Awake()
    {
        m_instance = this;
    }
    
    void Start()
    {
        InitMap();
    }
    void InitMap()
    {
        //for (int i = 0; i < m_mapCost.Length; i++)
        //{
        //    m_mapCost[i] = new int[8];
        //    for (int j = 0; j < 8; j++)
        //        m_mapCost[i][j] = 1;
        //}
    }

    
    
}
