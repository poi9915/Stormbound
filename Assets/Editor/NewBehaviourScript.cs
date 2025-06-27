using UnityEngine;
using UnityEditor;

public class ReplaceSecondFloor : EditorWindow
{
    GameObject prefabToReplaceWith;

    [MenuItem("Tools/Replace Second Floor")]
    public static void ShowWindow()
    {
        GetWindow<ReplaceSecondFloor>("Replace Second Floor");
    }

    void OnGUI()
    {
        GUILayout.Label("Thay thế toàn bộ sàn tầng 2", EditorStyles.boldLabel);
        prefabToReplaceWith = (GameObject)EditorGUILayout.ObjectField("Prefab thay thế", prefabToReplaceWith, typeof(GameObject), false);

        if (GUILayout.Button("Replace Now"))
        {
            Replace();
        }
    }

    void Replace()
    {
        if (prefabToReplaceWith == null)
        {
            Debug.LogError("Bạn chưa chọn prefab.");
            return;
        }

        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            // Tùy bạn sửa điều kiện này nếu tên khác
            if (obj.name.Contains("target_wall_large_B"))
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer == null) continue;

                Vector3 pos = obj.transform.position;
                Quaternion rot = obj.transform.rotation;

                GameObject newObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabToReplaceWith);
                newObj.transform.position = pos;
                newObj.transform.rotation = rot;
                newObj.transform.localScale = scale;
                newObj.name = obj.name;

                Undo.RegisterCreatedObjectUndo(newObj, "Replace Floor2");
                Undo.DestroyObjectImmediate(obj);
                count++;
            }
        }

        Debug.Log("Đã thay " + count + " sàn tầng 2.");
    }
}
