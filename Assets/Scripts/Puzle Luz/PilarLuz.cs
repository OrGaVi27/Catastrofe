using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PilarLuz : MonoBehaviour
{
    [Header("Emisor")]
    public GameObject emisor;
    public bool permEncendido;
    public bool encendido = false;
    [Space]

    [Header("Rayo")]
    public float rayDistance = 10f;
    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    private PilarLuz ultimoPilarDetectado = null;
    [Space]
    
    [Header("TiempoRecarga")]
    public float maxTime = 0.5f;
    float currentTime;

    void Start()
    { 
        currentTime = 0; 

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        if(permEncendido == true)
        {
             encendido = true;
        }
    }

    void LateUpdate()
    {   
        currentTime += Time.deltaTime;

        if(currentTime >= maxTime)
        {
            currentTime = 0;
            DetectTarget();
        } 

        if (lineRenderer.enabled != encendido)
        {
            lineRenderer.enabled = encendido;
        }

        if(encendido == true)
        {
            LineRendererActivo();
        }
    }
    void DetectTarget()
    {
        RaycastHit hit;
        Vector3 direction = transform.forward;

        if (Physics.Raycast(emisor.transform.position, direction, out hit, rayDistance, layerMask) && encendido == true)
        {
            PilarLuz pilar = hit.transform.GetComponentInParent<PilarLuz>();
            PuertaPuzleOrbe puerta = hit.transform.gameObject.GetComponentInParent<PuertaPuzleOrbe>();

            if (pilar != null)
            {
                pilar.encendido = true;
                // Guardar el último Pilar detectado
                ultimoPilarDetectado = pilar;
            }
            else if(puerta != null)
            {
                puerta.AbrirPuerta();
            }
            else
            {
                // Si el objeto detectado NO es un PilarLuz, apagamos el último Pilar detectado
                if (ultimoPilarDetectado != null)
                {
                    ultimoPilarDetectado.encendido = false;
                    
                    ultimoPilarDetectado = null;
                }
            }
        }
        else
        {
            // Si no detectamos nada, apagamos el último Pilar encendido
            if (ultimoPilarDetectado != null)
            {
                ultimoPilarDetectado.encendido = false;
                
                ultimoPilarDetectado = null;
            }
        }
    }

    private void LineRendererActivo()
    {
        if (!encendido) return;

        RaycastHit hit;
        Vector3 startPosition = emisor.transform.position;
        Vector3 direction = transform.forward;

        // Realizar el Raycast
        if (Physics.Raycast(startPosition, direction, out hit, rayDistance, layerMask))
        {
            // Si hay colisión, establecer el punto final en el punto de impacto
            Vector3 endPosition = hit.point;
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }
        else
        {
            // Si no hay colisión, extender el Line Renderer hasta la distancia máxima
            Vector3 endPosition = startPosition + direction * rayDistance;
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, endPosition);
        }
    }

    private void OnDrawGizmos()
    {
        if(encendido == true)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(emisor.transform.position, transform.forward * rayDistance);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(emisor.transform.position, transform.forward * rayDistance);
        }
    }
}
