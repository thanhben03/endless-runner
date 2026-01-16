using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class NicknameManager : MonoBehaviour
{
    public TMP_InputField nicknameInput;
    public GameObject popup;
    public TextMeshProUGUI errorText;

    private string pattern = "^[a-z0-9]{3,10}$";

    void Start()
    {

        if (PlayerPrefs.HasKey("Nickname"))
        {
            popup.SetActive(false);
            //runButton.SetActive(true);
        }
        else
        {
            popup.SetActive(true);
            //runButton.SetActive(false);
        }

        errorText.gameObject.SetActive(false);
    }

    public void OnConfirmNickname()
    {
        string nickname = nicknameInput.text.Trim();

        if (!IsValidNickname(nickname))
        {
            ShowError("Nicknames consist only of lowercase letters and numbers (3–10 characters).");
            return;
        }

        StartCoroutine(PlayerAPI.Instance.CreatePlayer(
            nickname, 
            onSuccess: () =>
            {
                popup.SetActive(false);

            }, 
            onError: message =>
            {
                ShowError(message);
            }));
        //runButton.SetActive(true);
    }

    bool IsValidNickname(string nickname)
    {
        return Regex.IsMatch(nickname, pattern);
    }

    void ShowError(string message)
    {
        errorText.text = message;
        errorText.gameObject.SetActive(true);
    }
}
