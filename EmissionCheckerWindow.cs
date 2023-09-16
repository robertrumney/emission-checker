using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EmissionCheckerWindow : EditorWindow
{
    // For the scrollable view.
    private Vector2 scrollPosition;

    // List of materials with black emission.
    private List<Material> problematicMaterials; 

    // Add menu item to Unity's Window menu.
    [MenuItem("Window/Emission Checker")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EmissionCheckerWindow), false, "Emission Checker");
    }

    // Called when the window is enabled/opened.
    private void OnEnable()
    {
        FindProblematicMaterials();
    }

    // GUI layout of the editor window.
    private void OnGUI()
    {
        // Refresh materials list button.
        if (GUILayout.Button("Refresh"))
        {
            FindProblematicMaterials();
        }

        // Button to disable emission for all problematic materials.
        if (problematicMaterials != null && problematicMaterials.Count > 0)
        {
            if (GUILayout.Button("Disable Emission for All Listed Materials"))
            {
                DisableEmissionForAll();
            }
        }

        // Display problematic materials.
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        if (problematicMaterials != null)
        {
            foreach (Material mat in problematicMaterials)
            {
                EditorGUILayout.ObjectField("Material", mat, typeof(Material), false);
            }
        }
        EditorGUILayout.EndScrollView();
    }

    // Finds materials with emission enabled but set to black.
    private void FindProblematicMaterials()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        problematicMaterials = new List<Material>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(assetPath);

            if (mat != null && mat.HasProperty("_EmissionColor") && mat.IsKeywordEnabled("_EMISSION") && mat.GetColor("_EmissionColor") == Color.black)
            {
                problematicMaterials.Add(mat);
            }
        }
    }

    // Disables emission for all materials in the problematicMaterials list.
    private void DisableEmissionForAll()
    {
        foreach (Material mat in problematicMaterials)
        {
            if (mat.HasProperty("_EmissionColor"))
            {
                mat.DisableKeyword("_EMISSION");
            }
        }

        // Refresh the list after making changes.
        FindProblematicMaterials();
    }
}
