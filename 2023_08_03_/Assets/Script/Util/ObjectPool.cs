using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour
{
    List<GameObject> list;
    GameObject Prefabs;

    public ObjectPool(GameObject prefabs)
    {
        Prefabs = prefabs;
        list = new List<GameObject>();
    }

    public T GetObj()
    {
        for(int i = 0; i < list.Count; i++)
        {
            if (!list[i].activeSelf)
                return list[i].GetComponent<T>();
        }

        GameObject createObj = Instantiate(Prefabs);
        list.Add(createObj);
        return createObj.GetComponent<T>();
    }
}
