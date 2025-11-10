using System.Collections;
using UnityEngine;

public abstract class CookingMiniGame : MonoBehaviour
{
    protected string gameName;
    protected int difficulty;
    protected bool isSuccess;


    protected void StartMiniGame()
    {
        Debug.Log(gameName + "게임 시작!");
        isSuccess = false;
    }

    public abstract IEnumerator Play();

    public virtual void EndMiniGame()
    {
        if (isSuccess)
            Debug.Log("요리 성공!");
        else
            Debug.Log("요리 실패... 재료 손실 발생");
    }

    public virtual void SetDifficulty()
    {
        //여기다가 난이도 조절 해버리기
    }

}
