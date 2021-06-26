using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CSGGenerator : MonoBehaviour
{
    public Transform m_CSGHolder;
    public Transform m_GenerationHolder;

    public bool m_CSGInFocus = true;

#if UNITY_EDITOR
    [MenuItem("AOB/Swap CSG %g")]
    public static void SwapContexts()
    {
        CSGGenerator[] gens = FindObjectsOfType<CSGGenerator>();
        for (int i = 0; i < gens.Length; i++)
        {
            gens[i].SwapContext();
        }
    }
#endif

    private void LoadGeneratedMesh()
    {
        m_CSGHolder.gameObject.SetActive(true);
        m_GenerationHolder.gameObject.SetActive(true);
        for (int i = m_GenerationHolder.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(m_GenerationHolder.GetChild(i).gameObject);
        }

        Transform originalMesh = m_CSGHolder.Find("MeshGroup");
        GameObject copiedMesh = Instantiate(originalMesh.gameObject);

        copiedMesh.transform.SetParent(m_GenerationHolder, true);
        copiedMesh.isStatic = true;
        for (int i = 0; i < copiedMesh.transform.childCount; i++)
        {
            copiedMesh.transform.GetChild(i).gameObject.isStatic = true;
        }

        m_CSGHolder.gameObject.SetActive(false);
    }

    private void LoadCSG()
    {
        m_GenerationHolder.gameObject.SetActive(false);
        m_CSGHolder.gameObject.SetActive(true);
    }

    public void SwapContext()
    {
        m_CSGInFocus = !m_CSGInFocus;

        if (m_CSGInFocus)
        {
            LoadCSG();
        }
        else
        {
            LoadGeneratedMesh();
        }
    }
}
