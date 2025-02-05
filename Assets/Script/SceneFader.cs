using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image mLoadingImage;
    public AnimationCurve mCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float time = 1.0f;

        while(time > 0)
        {
            time -= Time.deltaTime;
            float a = mCurve.Evaluate(time);
            mLoadingImage.color = new Color(0.0f,0.0f,0.0f,time);
            yield return 0;
        }

    }

    IEnumerator FadeOut(string scene)
    {
        float time = 0.0f;

        while (time < 1.0f)
        {
            time += Time.deltaTime;
            float a = mCurve.Evaluate(time);
            mLoadingImage.color = new Color(0.0f, 0.0f, 0.0f, time);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
