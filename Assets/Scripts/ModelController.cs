using UnityEngine;
using UnityEngine.UI;

public class ModelController : MonoBehaviour
{
    // List of all types of transformations available to perform on the model
    public enum TransformType { Translate, Rotate, Scale, Reset };

    // Customize the speed and min/max of different transformations
    public float translateSpeed;
    public float rotationSpeed;
    public float scaleSpeed;
    public float minScale;
    public float maxScale;

    // Canvas joystick used to control transformations
    public FixedJoystick joystick;
    // Sprites used for the canvas joystick UI depending on joystick's axis support
    public Sprite joystickBgBoth;
    public Sprite joystickBgHorizontal;
    public Sprite joystickBgVertical;
    private Image joystickBgImg;

    // Current transform enabled for the model
    private TransformType m_currTransformType;
    public TransformType CurrTransformType { get { return m_currTransformType; } set { m_currTransformType = value; } }

    // Model's currently active model/prefab and material
    private GameObject m_activeModel;
    private Material m_activeMaterial;

    // Change current enabled type of transform 
    public void UpdateTransformType(TransformType type)
    {
        // Allow vertical and horizontal motion for translations (translate along X and Z axes)
        if (type == TransformType.Translate)
        {
            joystick.AxisOptions = AxisOptions.Both;
            joystickBgImg.sprite = joystickBgBoth;
        }
        // Allow only horizontal motion for rotations (rotate around Y axis only)
        else if (type == TransformType.Rotate)
        {
            joystick.AxisOptions = AxisOptions.Horizontal;
            joystickBgImg.sprite = joystickBgHorizontal;
        }
        // Allow only vertical motion for scaling (scale up and down)
        else if (type == TransformType.Scale)
        {
            joystick.AxisOptions = AxisOptions.Vertical;
            joystickBgImg.sprite = joystickBgVertical;
        }

        m_currTransformType = type;
    }

    // Translate model based on joystick position
    public void TranslateModel()
    {
        Vector3 dir = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
        transform.Translate(dir * translateSpeed * Time.deltaTime, Space.World);
    }

    // Rotate model based on joystick position
    public void RotateModel()
    {
        float angle = joystick.Horizontal * rotationSpeed;
        transform.RotateAround(transform.position, Vector3.up, angle);
    }

    // Scale model based on joystick position
    public void ScaleModel()
    {
        float newScale = Mathf.Clamp(transform.localScale.x + (scaleSpeed * joystick.Vertical * Time.deltaTime), minScale, maxScale);
        transform.localScale = Vector3.one * newScale;
    }

    // Set model position, rotation, and scale back to initial state
    public void ResetAllTransforms()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    // Call appropriate model transform function based on currently enabled transform type
    public void UpdateTransform()
    {
        if (m_currTransformType == TransformType.Translate) TranslateModel();
        else if (m_currTransformType == TransformType.Rotate) RotateModel();
        else if (m_currTransformType == TransformType.Scale) ScaleModel();
    }

    // Update the model
    public void SetModel(GameObject prefab)
    {
        // Destroy current active model and spawn new model prefab
        Helper.DestroyChildren(gameObject);
        Helper.SpawnPrefab(prefab, gameObject);
        m_activeModel = prefab;
        // Set the material of the new model to the currently active material
        SetMaterial(m_activeMaterial);
    }

    // Update the model's material
    public void SetMaterial(Material newMaterial)
    {
        m_activeMaterial = newMaterial;
        // If user selected the first material option ("Original") which stores a null material, set the new material to the model's original material
        if (newMaterial == null) newMaterial = m_activeModel.GetComponentInChildren<MeshRenderer>(true).sharedMaterial;

        // Update the material on all of the model's children meshes
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            if (renderer) renderer.material = newMaterial;
        }
    }

    void Awake() {
        joystickBgImg = joystick.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        // Transform the model each frame (model won't transform if joystick is not being moved)
        UpdateTransform();
    }
}
