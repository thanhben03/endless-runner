using UnityEngine;
using TMPro;
using System.Collections;

public class GameStartCountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject countdownPanel;
    private Animator anim;
    public GameObject player;

    private void Start()
    {
        player = GamePlayManager.Instance.GetPlayer();
        player.GetComponent<PlayerMovement>().forwardSpeed = 0; 
        countdownPanel.SetActive(true);
        anim = player.GetComponentInChildren<Animator>();
        anim.SetBool("Ready", true);
        StartCoroutine(CountdownRoutine());
        AudioManager.Instance.PlaySoundCountDown();
    }

    IEnumerator CountdownRoutine()
    {
        int count = 4;

        while (count > 0)
        {

            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(0.8f);
            count--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);

        countdownPanel.SetActive(false);
        Time.timeScale = 1f;
        anim.SetBool("Ready", false);
        player.GetComponent<PlayerMovement>().forwardSpeed = 7f;
        player.GetComponent<PlayerMovement>().canMove = true;
    }
}
