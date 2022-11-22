using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class LoginController : MonoBehaviour
{

    [SerializeField] TMP_InputField loginName; 
    [SerializeField] TMP_InputField password;

    [SerializeField] Button loginBtn;
    [SerializeField] Button forgotPassword;

    [SerializeField] TextMeshProUGUI resultTxt ; 

    void Start()
    {
        loginBtn.onClick.AddListener(OnLoginBtnPressed);
        forgotPassword.onClick.AddListener(OnForgotPasswordPressed); 
    }

    void OnLoginBtnPressed()
    {
        StartCoroutine(LoginToApp());
    }

    void OnForgotPasswordPressed()
    {
        Application.OpenURL("https://mhrms.io/admin/login");
    }

    IEnumerator LoginToApp()
    {
        string username = loginName.text.Trim();
        string pswd = password.text.Trim();
        WWWForm form = new WWWForm();
        form.AddField("username", "sachin.s@maintec.in");
        form.AddField("password", "Admin@123");

        UnityWebRequest www = UnityWebRequest.Post("https://mhrms.io/test/api/user-login", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!" + www.downloadHandler.text);
        }
     
        LoginResponseData loginRes = 
                    JsonUtility.FromJson<LoginResponseData>(www.downloadHandler.text);
        Debug.Log("HELLO " + loginRes.success);

        if (loginRes.success)
        {
            AppControllService.Instance.TogglePanel(PanelName.MainPanel);
            PunchLogService.Instance.StartShift(); 
        }
        else
        {
            resultTxt.text = loginRes.message;
        }

        www.Dispose();
    }
   
    public class LoginResponseData
    {
        //{
        //    "success": true,
        //    "message": "Login Successfully",
        //    "otp_status": false
        //}
        public bool success;
        public string message;
        public bool otp_status;

      

    }


}
