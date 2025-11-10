using System;
using System.Collections;
using UnityEngine;

public class Frypan : CookingMiniGame
{
    [SerializeField] private int arrow_Count = 3;
    [SerializeField] private int correct_Count = 0;
    
    private int success_Count = 0;
    private int fail_Count = 0;

    private float time_Limit = 0;
    private float delay = 20f;

    private void Start()
    {
        gameName = "후라이팬";
        StartCoroutine(Play());
    }

    public override IEnumerator Play()
    {
        StartMiniGame();

        KeyCode[] keys = new KeyCode[arrow_Count];

        for (int j = 0; j < 3; j++)
        {
            yield return null; // 한프레임 미루기 (Input.anyKeyDown)
            time_Limit = 0f;
            correct_Count = 0;
            Debug.Log(j + 1 + "번째 게임");
            
            for (int i = 0; i < arrow_Count; i++)
            {
                keys[i] = GetRandomKey();
            }

            Debug.Log("시작! 방향키를 순서대로 눌러봐!");
            Debug.Log($"목표: {keys[0]}, {keys[1]}, {keys[2]}"); //임시 출력
            
            while (time_Limit <= delay)
            {
                time_Limit += Time.deltaTime;


                if (Input.anyKeyDown)
                {
                    if (Input.GetKeyDown(keys[correct_Count]))
                    {

                        correct_Count++;
                        Debug.Log(correct_Count + "개 정답");
                        // 모든 입력 성공 시 종료
                        if (correct_Count == arrow_Count) 
                        {
                            success_Count++;
                            break;
                        }

                           
                    }
                    else
                    {
                        Debug.Log("❌ 실패 (잘못된 키)");
                        fail_Count++;
                        break;
                    }
                }
                
                yield return null;
            }
            
            if (success_Count == 2)
            {
                break;
            }
                
            else if (fail_Count == 2 || time_Limit >= delay)
            {
                break;
            }
                
            



        }

        isSuccess = success_Count >= fail_Count;
        EndMiniGame();
    }

    KeyCode GetRandomKey()
    {
        KeyCode[] key = { KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow };
        return key[UnityEngine.Random.Range(0, key.Length)];
    }

    public override void SetDifficulty()
    {
        
    }
}