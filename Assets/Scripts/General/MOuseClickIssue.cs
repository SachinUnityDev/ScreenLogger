using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class MOuseClickIssue : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("MOUSE CLICKED");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("MOUSE hovered");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
