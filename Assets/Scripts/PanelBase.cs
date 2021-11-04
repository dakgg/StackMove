using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PanelBase : MonoBehaviour
    , IPointerDownHandler
    , IPointerUpHandler
{
    Ray m_Ray;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        m_Ray = RectTransformUtility.ScreenPointToRay(Camera.main, eventData.position);

        if (Physics2D.Raycast(m_Ray.origin, m_Ray.direction, float.PositiveInfinity, 1 << LayerMask.NameToLayer("TEST")))
        {
            Debug.Log("hit!!");
        }
        else
        {
            Debug.Log("not hit!!");
        }
    }

    void Update()
    {
        Debug.DrawRay(m_Ray.origin, m_Ray.direction, Color.red);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
    }
}
