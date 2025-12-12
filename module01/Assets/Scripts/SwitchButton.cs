using UnityEngine;

public class SwitchButton : MonoBehaviour
{
    public GameObject platform;

    // Assign these in the inspector
    public Material claireMaterial;
    public Material thomasMaterial;
    public Material johnMaterial;

    // order: Claire, Thomas, John
    private readonly string[] layerNames = { "ClaireElem", "ThomasElem", "JohnElem" };
    private Material[] materials;
    private int stateIndex = 0;

    void Awake()
    {
        materials = new Material[] { claireMaterial, thomasMaterial, johnMaterial };
        ApplyState(stateIndex); // optional initial state
    }

    // Change the layer and the material of the platform when any player triggers
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (platform == null) return;

        // cycle to next state
        stateIndex = (stateIndex + 1) % layerNames.Length;
        ApplyState(stateIndex);
    }

    private void ApplyState(int idx)
    {
        if (platform == null) return;

        // set material if assigned
        var rend = platform.GetComponent<Renderer>();
        if (rend != null && materials[idx] != null)
            rend.material = materials[idx];

        // set layer if exists
        int layer = LayerMask.NameToLayer(layerNames[idx]);
        if (layer != -1)
            platform.layer = layer;
        else
            Debug.LogWarning("Layer not found: " + layerNames[idx]);
    }
}