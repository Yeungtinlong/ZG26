using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

public class LodSelector : OdinEditorWindow
{
    [MenuItem("Tools/Lod Selector")]
    public static void OpenWindow()
    {
        GetWindow<LodSelector>().Show();
    }

    [SerializeField] private List<LODGroup> _lodGroups;
    [SerializeField] private List<LODGroup> _nullRenderersLODGroups;
    
    [Button("SELECT ALL LODS")]
    private void SelectAllLodInActiveScene()
    {
        _lodGroups = FindObjectsOfType<LODGroup>(true).ToList();
    }

    [Button("DELETE ALL LODS GT 1")]
    private void DeleteAllLodsGreaterThanOne()
    {
        _nullRenderersLODGroups = new List<LODGroup>();
        
        foreach (var lodGroup in _lodGroups)
        {
            // if (lodGroup.lodCount == 0)
            // {
            //     continue;
            // }
            
            DestroyImmediate(lodGroup);
            continue;
            
            LOD[] lods = lodGroup.GetLODs();
            
            for (int i = 1; i < lods.Length; i++)
            {
                for (int j = 0; j < lods[i].renderers.Length; j++)
                {
                    if (lods[i].renderers[j] == null)
                    {
                        _nullRenderersLODGroups.Add(lodGroup);
                        continue;
                    }

                    if (!lods[i].renderers[j].name.Contains("LOD"))
                    {
                        continue;
                    }
                    
                    DestroyImmediate(lods[i].renderers[j].gameObject);
                }
            }

            var targetTransition = lods[lods.Length - 1].screenRelativeTransitionHeight;
            
            lods = new LOD[1] { lods[0] };
            lods[0].screenRelativeTransitionHeight = 0.03f;
            
            lodGroup.SetLODs(lods);
            // lodGroup.enabled = false;

            EditorUtility.SetDirty(lodGroup);
        }
    }
}