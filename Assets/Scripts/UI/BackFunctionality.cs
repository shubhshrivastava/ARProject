using UnityEngine;
namespace AR_Project.Scripts.AndroidNativeFunctionality
{
    public class BackFunctionality : MonoBehaviour
    {
        public void Update()
        {
#if (!UNITY_EDITOR)
            if (Application.platform == RuntimePlatform.Android)
#endif
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    OnBackEnable();
            }
        }
        public void OnBackEnable()
        {
            PopupUI.Instance.backCanvas.gameObject.SetActive(true);
            
        }

        public void OnBackDisable()
        {
            PopupUI.Instance.backCanvas.gameObject.SetActive(false);
        }
    }
}

