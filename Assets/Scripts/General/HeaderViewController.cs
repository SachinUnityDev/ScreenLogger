using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class HeaderViewController : MonoBehaviour
{

    [SerializeField] Button profileBtn;
    [SerializeField] Button breakBtn;

    [SerializeField] Sprite breakOnSprite; 
    [SerializeField] Sprite breakOffSprite;

    private void Start()
    {
        profileBtn.onClick.AddListener(OnProfileBtnPressed);
        breakBtn.onClick.AddListener(OnBreakBtnPressed);    
    }

    void OnProfileBtnPressed()
    {

    }
    void OnBreakBtnPressed()
    {
        breakData breakData = new breakData(BreakType.TeaBreak
                            , "chai peena sehat ke liye acha hota hai");
        bool breakOn = DataService.Instance.breakOn; 
        DataService.Instance.ToggleBreak(breakData);
        if (breakOn)  // on // off val change in service not here 
        {
            breakBtn.GetComponent<Image>().sprite = breakOffSprite;
        }
        else
        {
            breakBtn.GetComponent<Image>().sprite = breakOnSprite;
        }

    }
}
