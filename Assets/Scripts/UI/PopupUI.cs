using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
namespace AR_Project.Scripts.AndroidNativeFunctionality
{
    public class PopupUI : MonoBehaviour
    {
        public static PopupUI Instance { get; private set; }
        [FormerlySerializedAs("m_BackCanvas")] public GameObject backCanvas;
        [FormerlySerializedAs("m_LockButton")] public Button lockButton;
        [FormerlySerializedAs("m_BackFunctionality")] public BackFunctionality backFunctionality;
        public TextMeshProUGUI text;

        public bool mUnlock;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            if (text==null)
            text = GetComponent<TextMeshProUGUI>();
            if (string.IsNullOrEmpty(text.text))
                Unlock(mUnlock);
        }

        private void Update()
        {
            lockButton.onClick.RemoveAllListeners();
            lockButton.onClick.AddListener(() => { mUnlock = !mUnlock; Unlock(mUnlock); });
        }

        public void OnYes()
        {
            Application.Quit();
        }

        public void OnNo()
        {
            backFunctionality.OnBackDisable();
        }

        private void Unlock(bool unlock)
        {
            text.text = unlock ? "lock" : "unlock";

        }
    }
}

