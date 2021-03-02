using UnityEngine;
using UnityEngine.UI;
namespace OperatorService
{

    public class PrivacyLink : MonoBehaviour
    {
        [SerializeField] private string link = "https://app.babangida.be/privacy/en.php";
        private Button privacyBtn;

        private void Start()
        {
            privacyBtn = GetComponent<Button>();
            privacyBtn.onClick.AddListener(OpenPrivacyLink);
        }

        private void OpenPrivacyLink()
        {
            Application.OpenURL(link);
        }
    }

}