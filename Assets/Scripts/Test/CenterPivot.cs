using UnityEngine;

[ExecuteAlways]
public class CenterPivot : MonoBehaviour
{
    [ContextMenu("Center Pivot")]
    void Center()
    {
        // Calcola il bounds combinato di tutti i renderer figli
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogWarning("Nessun renderer trovato per calcolare il centro.");
            return;
        }

        Bounds bounds = renderers[0].bounds;
        foreach (Renderer rend in renderers)
            bounds.Encapsulate(rend.bounds);

        Vector3 center = bounds.center;

        // Crea un genitore vuoto
        GameObject parent = new GameObject(gameObject.name + "_CenteredPivot");
        parent.transform.position = center;
        parent.transform.rotation = transform.rotation;
        parent.transform.parent = transform.parent;

        // Sposta questo oggetto come figlio del genitore appena creato
        transform.parent = parent.transform;

        // Compensa la posizione per mantenere la stessa posizione mondiale
        transform.position = center;

        Debug.Log("Pivot centrato con successo.");
    }
}
