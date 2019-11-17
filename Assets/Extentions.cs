using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions
{
    public static int GetOrderOfBrother(this Transform t1)
    {
        if(t1.parent!=null)
        {
            Transform parent = t1.parent;

            for(int i=0;i<parent.childCount;i++)
            {
                if(parent.GetChild(i)==t1)
                {
                    return i;
                }
            }
            
        }
        return 0;
    }

}
