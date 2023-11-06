using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] GameObject jumpscareCanvas;
    [SerializeField] GameObject videoPlayer;

    private void Start()
    {
        jumpscareCanvas.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
    }

    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        StartCoroutine(JumpscareRoutine());
    }

    IEnumerator JumpscareRoutine()
    {
        jumpscareCanvas.gameObject.SetActive(true);
        videoPlayer.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        Application.Quit();
    }
}
