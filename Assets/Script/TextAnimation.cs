using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    [SerializeField] private Text text;

    bool isTyping = false;
    string[] dialouges;
    int dialougeIndex = 0;
    Sequence textSequence;
    float timer = 2f;

    string[] dial = {
        "배고프다 시발시발시발시발rrrrrrrrrrrrrrrrrrrrr",
        "섹스 ㅈㄴ게 하고싶네 ㅅㅂrrrrrrrrrrrrrrrrrrrrrr"
    }; // 예시

    void Start()
    {
        dialouges = dial; 
        InitializeDialouge();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
                textSequence.Complete();
            else
                ChangeNextDialouge();
        }
    }

    public void SetDialouge(string[] dial)
    {
        dialouges = dial;
    }

    void InitializeDialouge()
    {
        SetSequence();
        PlaySequence();
    }

    void SetSequence()
    {
        if (textSequence != null)
            textSequence.Kill();

        isTyping = true; 

        textSequence = DOTween.Sequence();
        textSequence.Append(text.DOText(dialouges[dialougeIndex], timer).SetEase(Ease.Linear).OnComplete(() => isTyping = false)
        );
    }

    void PlaySequence()
    {
        text.text = "";

        textSequence.Play();
    }

    void ChangeNextDialouge()
    {
        dialougeIndex++;

        if (dialougeIndex >= dialouges.Length)
        {
            Debug.Log("모든 대화 종료");
            return;
        }

        InitializeDialouge();
    }
}
