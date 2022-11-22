using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppControllService : MonoSingletonGeneric<AppControllService>
{
    [SerializeField] GameObject openPanel; 
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject mainPanel; 
    
    public List<GameObject> allPanels = new List<GameObject>();
    public AppState appState = AppState.None ;
    void Start()
    {
        
    }

    public void TogglePanel(PanelName panelName)
    {
        foreach (GameObject panel in allPanels)                    
          panel.SetActive(false);

        switch (panelName)
        {
            case PanelName.OpenPanel:
                openPanel.SetActive(true);
                break;
            case PanelName.LoginPanel:
                loginPanel.SetActive(true);
                break;
            case PanelName.MainPanel:
                mainPanel.SetActive(true);  
                break;          
            default:
                break;
        }

    }
}
    public enum PanelName
    {
        OpenPanel, 
        LoginPanel, 
        MainPanel,


    }

    public enum AppState
    {
        None,
        SignedIn, 
        SignedOut,
        PunchedIn, 
        PunchedOut,
    }
