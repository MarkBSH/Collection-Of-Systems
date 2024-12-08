using UnityEditor;
using UnityEngine;
using System;

public class HierarchySorter : EditorWindow
{
    [MenuItem("Tools/Sort Hierarchy Alphabetically")]
    private static void SortHierarchy()
    {
        Transform[] allObjects = FindObjectsByType<Transform>(FindObjectsSortMode.None);
        // Sort all objects alphabetically by name
        Array.Sort(allObjects, (x, y) => string.Compare(x.name, y.name));

        // Set the sibling index based on the sorted order
        int index = 0;
        foreach (var name in new[] {
        "GameManager",
        "Main Camera",
        "Player"
        })
        {
            var obj = Array.Find(allObjects, t => t.name == name);
            if (obj != null)
            {
                obj.SetSiblingIndex(index++);
            }
        }

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (
            allObjects[i].name != "GameManager" &&
            allObjects[i].name != "Main Camera" &&
            allObjects[i].name != "Player"
            )
            {
                allObjects[i].SetSiblingIndex(index++);
            }
        }
    }
}
