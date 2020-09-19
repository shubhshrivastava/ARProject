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
            PopupUI.Instance.backCanvas.alpha = 1;
            PopupUI.Instance.backCanvas.interactable = true;
            PopupUI.Instance.backCanvas.blocksRaycasts= true;
        }

        public void OnBackDisable()
        {
            PopupUI.Instance.backCanvas.alpha = 0;
            PopupUI.Instance.backCanvas.gameObject.SetActive(false);
            PopupUI.Instance.backCanvas.interactable = false;
            PopupUI.Instance.backCanvas.blocksRaycasts = false;
        }
    }
}

