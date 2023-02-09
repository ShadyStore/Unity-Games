using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitor : MonoBehaviour
{
   // public Text LoadingPercentage;
    private static SceneTransitor instance;

    private Animator componentAnimator;
    private AsyncOperation loadingSceneOperation;
    private bool video_over;

    public static void SwitchToScene(string sceneName, string sceneTrigger)
    {
        instance.componentAnimator.SetTrigger(sceneTrigger);

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);

        // ����� ����� �� ������ ������������� ���� ������ �������� closing:
        instance.loadingSceneOperation.allowSceneActivation = false;
    }
    public static void SwitchToVideo(string sceneTrigger)
    {
        instance.componentAnimator.SetTrigger(sceneTrigger);
        
    }

    private void Start()
    {
        instance = this;

        componentAnimator = GetComponent<Animator>();
    }


    public void OnAnimationOver()
    {
        loadingSceneOperation.allowSceneActivation = true;
    }
}
