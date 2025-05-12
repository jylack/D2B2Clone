using System;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static void ForEach(this Range range, Action<int> act)
    {
        for (int i = range.Start.Value; i < range.End.Value; i++)
            act?.Invoke(i);
    }

    public static void DestroyAllChildren(this GameObject obj)
    {
        (..obj.transform.childCount).ForEach(i =>
        {
            UnityEngine.Object.Destroy(obj.transform.GetChild(i).gameObject);
        });
    }
    
    public static TComp GetComponentInChildrenEx<TComp>(this GameObject parent, string name = null) where TComp : Component
    {
        return ScUtils.GetComponentInChildrenEx<TComp>(parent, name);
    }
    
    public static List<TComp> GetComponentsInChildrenEx<TComp>(this GameObject parent) where TComp : Component
    {
        return ScUtils.GetComponentsInChildrenEx<TComp>(parent);
    }
}