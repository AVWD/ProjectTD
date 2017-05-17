using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections;
using System.IO;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject, // obj
            serializedObject.FindProperty("Waves"), // element
            true, // draggable
            true, // header
            true, // add button
            true); // remove button

        // Each prop of struct
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => 
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Type"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Prefab"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 30, rect.y, 30, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Count"), GUIContent.none);
        };

        // Header text
        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Mob Waves");
        };

        // Highlight prefab on select
        list.onSelectCallback = (ReorderableList l) =>
        {
            GameObject prefab = l.serializedProperty
                .GetArrayElementAtIndex(l.index)
                .FindPropertyRelative("Prefab")
                .objectReferenceValue as GameObject;

            if (prefab) EditorGUIUtility.PingObject(prefab.gameObject);
        };

        // Minimum of 1 element
        list.onCanRemoveCallback = (ReorderableList l) =>
        {
            return l.count > 1;
        };

        // Delete confirmation
        list.onRemoveCallback = (ReorderableList l) =>
        {
            if(EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete this wave?", "Yes", "No"))
            {
                ReorderableList.defaultBehaviours.DoRemoveButton(l);
            }
        };

        // Add default template
        list.onAddCallback = (ReorderableList l) =>
        {
            var idx = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            l.index = idx;
            var element = l.serializedProperty.GetArrayElementAtIndex(idx);
            element.FindPropertyRelative("Type").enumValueIndex = 0;
            element.FindPropertyRelative("Count").intValue = 20;
            element.FindPropertyRelative("Prefab").objectReferenceValue =
                AssetDatabase.LoadAssetAtPath("Assets/_game/prefabs/units/mobs/derp.prefab", typeof(GameObject)) as GameObject;
        };

        list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) =>
        {
            var menu = new GenericMenu();
            var guids = AssetDatabase.FindAssets("", new[] { "Assets/_game/prefabs/units/mobs" });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Mobs/" + Path.GetFileNameWithoutExtension(path)),
                    false, clickHandler,
                    new WaveCreationParams() { Type = MobWave.WaveType.normal, Path = path });
            }
            guids = AssetDatabase.FindAssets("", new[] { "Assets/_game/prefabs/units/bosses" });
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                menu.AddItem(new GUIContent("Bosses/" + Path.GetFileNameWithoutExtension(path)),
                    false, clickHandler,
                    new WaveCreationParams() { Type = MobWave.WaveType.boss, Path = path });
            }
            menu.ShowAsContext();
        };
        
    }

    public override void OnInspectorGUI()
    {
        // Render all standard items.
        // Uses [HideInInspector] to prevent wave data from rendering
        base.OnInspectorGUI();

        // Now render custom wave data editor
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void clickHandler(object target)
    {
        var data = (WaveCreationParams)target;
        var idx = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = idx;
        var element = list.serializedProperty.GetArrayElementAtIndex(idx);

        element.FindPropertyRelative("Type").enumValueIndex = (int)data.Type;
        element.FindPropertyRelative("Count").intValue = data.Type == MobWave.WaveType.boss ? 1 : 20;
        element.FindPropertyRelative("Prefab").objectReferenceValue =
            AssetDatabase.LoadAssetAtPath(data.Path, typeof(GameObject)) as GameObject;

        serializedObject.ApplyModifiedProperties();
    }

    private struct WaveCreationParams
    {
        public MobWave.WaveType Type;
        public string Path;
    }
}
