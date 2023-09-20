using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class EmissionCheckerWindow : EditorWindow
{
    // Toggle for all emissions
    private bool showRedundantEmissions = false;

    // For the scrollable view.
    private Vector2 scrollPosition;

    // List of materials with emission enabled.
    private List<Material> problematicMaterials;

    // Dictionary to track which materials are selected.
    private readonly Dictionary<Material, bool> materialSelection = new Dictionary<Material, bool>();

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

    private void OnGUI()
    {
        // Refresh materials list button.
        if (GUILayout.Button("Refresh"))
        {
            FindProblematicMaterials();
        }

        // Button to disable emission for selected materials.
        if (problematicMaterials != null && problematicMaterials.Count > 0)
        {
            if (GUILayout.Button("Disable Emission for Selected Materials"))
            {
                DisableEmissionForSelected();
            }
        }

        // Toggle for showing all emissions or only black emissions.
        showRedundantEmissions = EditorGUILayout.Toggle("Show Redundant Emissions", showRedundantEmissions);

        // Button to select all materials.
        if (GUILayout.Button("Select All"))
        {
            SelectAllMaterials();
        }

        // Display problematic materials with selection checkboxes, clickable ObjectField, and emission color string.
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        if (problematicMaterials != null)
        {
            foreach (Material mat in problematicMaterials)
            {
                materialSelection.TryGetValue(mat, out bool isSelected);

                EditorGUILayout.BeginHorizontal();

                // Toggle checkbox for selection.
                bool newIsSelected = EditorGUILayout.Toggle(isSelected, GUILayout.Width(15));
                if (newIsSelected != isSelected)
                {
                    materialSelection[mat] = newIsSelected;
                }

                // ObjectField to display and select the material.
                EditorGUILayout.ObjectField(mat, typeof(Material), false);

                // Display the emission color as a string if the material has the "_EmissionColor" property.
                if (mat.HasProperty("_EmissionColor"))
                {
                    EditorGUI.DrawRect(GUILayoutUtility.GetRect(20, 20), mat.GetColor("_EmissionColor"));
                }

                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
    }

    private void FindProblematicMaterials()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        problematicMaterials = new List<Material>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(assetPath);

            if (mat != null && mat.HasProperty("_EmissionColor") && mat.IsKeywordEnabled("_EMISSION"))
            {
                Color emissionColor = mat.GetColor("_EmissionColor");
                if (!showRedundantEmissions || (emissionColor.r == 0 && emissionColor.g == 0 && emissionColor.b == 0))
                {
                    problematicMaterials.Add(mat);
                }
            }
        }

        // Sort the materials by name.
        problematicMaterials.Sort((mat1, mat2) => mat1.name.CompareTo(mat2.name));
    }


    private void DisableEmissionForSelected()
    {
        foreach (KeyValuePair<Material, bool> kvp in materialSelection)
        {
            if (kvp.Value) // if selected
            {
                Material mat = kvp.Key;
                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.DisableKeyword("_EMISSION");
                    mat.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
                    mat.SetColor("_EmissionColor", Color.black);
                }
            }
        }
    }

    private void SelectAllMaterials()
    {
        foreach (Material mat in problematicMaterials)
        {
            materialSelection[mat] = true;
        }
    }
}
