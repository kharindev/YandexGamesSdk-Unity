using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class Ext 
{
    public static void UpdateListener(this Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    public static void Clear(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }
}
