using UnityEngine;
using UnityEngine.UI;

public class ModelController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float translateSpeed;
    public float rotationSpeed;
    public float scaleSpeed;
    public float minScale;
    public float maxScale;

    public Sprite joystickBgBoth;
    public Sprite joystickBgHorizontal;
    public Sprite joystickBgVertical;

    public enum TransformType { Translate, Rotate, Scale, Reset };

    private TransformType m_currTransformType;
    public TransformType CurrTransformType { get { return m_currTransformType; } set { m_currTransformType = value; } }

    private GameObject m_activeModel;
    private Material m_activeMaterial;

    public void UpdateTransformType(TransformType type)
    {
        m_currTransformType = type;

        Image bgImg = joystick.gameObject.GetComponent<Image>();

        if (m_currTransformType == TransformType.Translate)
        {
            joystick.AxisOptions = AxisOptions.Both;
            bgImg.sprite = joystickBgBoth;
        }
        else if (m_currTransformType == TransformType.Rotate)
        {
            joystick.AxisOptions = AxisOptions.Horizontal;
            bgImg.sprite = joystickBgHorizontal;
        }
        else if (m_currTransformType == TransformType.Scale)
        {
            joystick.AxisOptions = AxisOptions.Vertical;
            bgImg.sprite = joystickBgVertical;
        }
    }

    public void TranslateModel()
    {
        Vector3 dir = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        transform.Translate(dir * translateSpeed * Time.deltaTime, Space.World);
    }

    public void RotateModel()
    {
        float angle = joystick.Horizontal * rotationSpeed;
        transform.RotateAround(transform.position, Vector3.up, angle);
    }

    public void ScaleModel()
    {
        float newScale = Mathf.Clamp(transform.localScale.x + (scaleSpeed * joystick.Vertical * Time.deltaTime), minScale, maxScale);
        transform.localScale = Vector3.one * newScale;
    }

    public void ResetAllTransforms()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public void UpdateTransform()
    {
        if (m_currTransformType == TransformType.Translate) TranslateModel();
        else if (m_currTransformType == TransformType.Rotate) RotateModel();
        else if (m_currTransformType == TransformType.Scale) ScaleModel();
    }

    public void SetModel(GameObject prefab)
    {
        Helper.DestroyChildren(gameObject);
        Helper.SpawnPrefab(prefab, gameObject);
        m_activeModel = prefab;
        SetMaterial(m_activeMaterial);
    }

    public void SetMaterial(Material newMaterial)
    {
        m_activeMaterial = newMaterial;
        if (newMaterial == null) newMaterial = GetOriginalModelMat(m_activeModel);

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer) renderer.material = newMaterial;
        }
    }

    public Material GetOriginalModelMat(GameObject model)
    {
        return model.GetComponentInChildren<MeshRenderer>(true).sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTransform();
    }
}
