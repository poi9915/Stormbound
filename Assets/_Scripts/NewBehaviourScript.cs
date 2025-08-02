using UnityEngine;
using UnityEditor;

public class ReplaceAllByName : EditorWindow
{
    GameObject prefabToReplaceWith;
    string namePrefix = "Primitive_Floor";

    [MenuItem("Tools/Replace All Matching Names")]
    static void Init()
    {
        ReplaceAllByName window = (ReplaceAllByName)GetWindow(typeof(ReplaceAllByName));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Replace All Objects By Name", EditorStyles.boldLabel);
        namePrefix = EditorGUILayout.TextField("Object name starts with:", namePrefix);
        prefabToReplaceWith = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabToReplaceWith, typeof(GameObject), false);

        if (GUILayout.Button("Replace Now"))
        {
            ReplaceAll();
        }
    }

    void ReplaceAll()
    {
        if (prefabToReplaceWith == null)
        {
            Debug.LogError("Chưa chọn prefab thay thế!");
            return;
        }

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        int count = 0;
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.StartsWith(namePrefix))
            {
                Vector3 pos = obj.transform.position;
                Quaternion rot = obj.transform.rotation;
                Vector3 scale = obj.transform.localScale;

                GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabToReplaceWith);
                newObj.transform.position = pos;
                newObj.transform.rotation = rot;
                newObj.transform.localScale = scale;
                newObj.name = obj.name;

                Undo.RegisterCreatedObjectUndo(newObj, "Replace Object");
                Undo.DestroyObjectImmediate(obj);
                count++;
            }
        }

        Debug.Log("Đã thay thế " + count + " object.");
    }
}
