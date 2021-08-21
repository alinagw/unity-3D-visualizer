using System;
using System.Linq;
using UnityEngine;

// Utility functions
public static class Helper
{
    // Instantiate a prefab and set its parent, local scale, and local rotation
    public static GameObject SpawnPrefab(GameObject prefab, GameObject parent)
    {
        GameObject newObject = GameObject.Instantiate(prefab);
        newObject.transform.SetParent(parent.transform);
        newObject.transform.localScale = Vector3.one;
        newObject.transform.localRotation = Quaternion.identity;
        return newObject;
    }

    // Destroy all first-level children of a GameObject
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

    // Add spaces before each capital letter in a string (camel case to title case)
    public static string AddSpacesToString(string str)
    {
        return string.Concat(str.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }

    // Return array of enum types for given type
    public static T[] GetEnumValues<T>()
    {
        return (T[])System.Enum.GetValues(typeof(T));
    }
}
