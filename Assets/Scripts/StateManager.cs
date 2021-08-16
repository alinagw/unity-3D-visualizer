using UnityEngine;

public class StateManager : MonoBehaviour
{
    public OptionsManager optionsManager;
    public ModelController modelController;

    private GameObject m_activeModel;
    private Material m_activeMaterial;

    public void SetModel(GameObject prefab)
    {
        Helper.DestroyChildren(modelController.gameObject);
        Helper.SpawnPrefab(prefab, modelController.gameObject);
        m_activeModel = prefab;
        SetMaterial(m_activeMaterial);
    }

    public void SetMaterial(Material newMaterial)
    {
        m_activeMaterial = newMaterial;
        if (newMaterial == null) newMaterial = GetOriginalModelMat(m_activeModel);

        MeshRenderer[] renderers = modelController.gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer) renderer.material = newMaterial;
        }
    }

    public void SetOption(OptionsManager.OptionType type, dynamic resource, bool isOn)
    {
        if (isOn)
        {
            if (type == OptionsManager.OptionType.Models) SetModel(((Option<GameObject>)resource).Item);
            else if (type == OptionsManager.OptionType.Materials) SetMaterial(((Option<Material>)resource).Item);
        }
    }

    public Material GetOriginalModelMat(GameObject model)
    {
        return model.GetComponentInChildren<MeshRenderer>(true).sharedMaterial;
    }

    public void InitializeState()
    {
        m_activeMaterial = null;
        SetModel(optionsManager.Models[0].Item);
    }
}
