using System;
using System.Linq;
using UnityEngine;

public static class Helper
{
    public static GameObject SpawnPrefab(GameObject prefab, GameObject parent)
    {
        GameObject newObject = GameObject.Instantiate(prefab);
        newObject.transform.SetParent(parent.transform);
        newObject.transform.localScale = Vector3.one;
        newObject.transform.localRotation = Quaternion.identity;
        return newObject;
    }

    public static void DestroyChildren(GameObject parent)
    {
        if (parent.transform.childCount > 0)
        {
            foreach (Transform child in parent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public static T[] GetEnumValues<T>()
    {
        return (T[])System.Enum.GetValues(typeof(T));
    }
}
