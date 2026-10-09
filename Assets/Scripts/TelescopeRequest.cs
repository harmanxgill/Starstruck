using UnityEngine;
using UnityEngine.InputSystem;

public class TelescopeRequest : MonoBehaviour
{
    public Transform professor;
    public Renderer telescopeTube;
    public float helpDistance = 2.5f;
    public float helpDuration = 2f;
    public GameObject requestMarker;

    private float progress;
    private bool solved;
    private float nextRequest;
    public float patienceDuration = 20f;

    private float patience;
    private Renderer markerRenderer;
    private Color requestColor = new Color(1f, 0.55f, 0.15f);

    void Start()
    {
        telescopeTube.material.color = requestColor;
        patience = patienceDuration;

        if (requestMarker != null)
            markerRenderer = requestMarker.GetComponent<Renderer>();
    }

    void Update()
    {
        if (solved)
        {
            nextRequest -= Time.deltaTime;

            if (nextRequest <= 0f)
            {
                solved = false;
                progress = 0f;
                patience = patienceDuration;
                telescopeTube.material.color = requestColor;

                if (requestMarker != null)
                    requestMarker.SetActive(true);
            }

            return;
        }

        if (professor == null || Keyboard.current == null)
            return;

        patience = Mathf.Max(0f, patience - Time.deltaTime);

        if (markerRenderer != null)
        {
            float urgency = 1f - patience / patienceDuration;
            markerRenderer.material.color =
                Color.Lerp(requestColor, Color.red, urgency);
        }

        Vector3 offset = professor.position - transform.position;
        offset.y = 0f;

        bool nearby = offset.magnitude <= helpDistance;

        if (nearby && Keyboard.current.eKey.isPressed)
            progress += Time.deltaTime;
        else
            progress = 0f;

        telescopeTube.material.color = Color.Lerp(
            requestColor, Color.green,
            Mathf.Clamp01(progress / helpDuration)
        );

        if (progress >= helpDuration)
        {
            solved = true;
            nextRequest = Random.Range(8f, 15f);
            if (requestMarker != null)
                requestMarker.SetActive(false);
            Debug.Log("Group 1 helped!");
        }
    }
}