using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fryer : CookingMiniGame
{
    [Header("UI References")]
    [SerializeField] private RectTransform pointer;     
    [SerializeField] private Image successZone;          

    [Header("Settings")]
    [SerializeField] private float speed = 0.5f;         
    [SerializeField] private float successSize = 0.1f;   
    [SerializeField] private float delay = 15f;          
    [SerializeField] private int requiredSuccess = 2;    

    private float successStart;
    private float successEnd;
    private float arrowValue;                            // 0~1 사이 회전값
    private int successCount;
    private float timer;

    private void Start()
    {
        gameName = "튀김기";
        StartCoroutine(Play());
    }

    public override IEnumerator Play()
    {
        StartMiniGame();
        yield return new WaitForSeconds(1f);

        for (int round = 0; round < 3; round++)
        {
            // 랜덤 성공 구간 설정
            successStart = Random.Range(0f, 1f - successSize);
            successEnd = successStart + successSize;

            // 시각적으로 성공 구간 표시 (fillAmount와 회전 동기화)
            successZone.fillAmount = successSize;
            successZone.transform.localEulerAngles = new Vector3(0, 0, -360f * successStart);

            

            arrowValue = 0f;
            timer = 0f;

            while (timer <= delay)
            {
                timer += Time.deltaTime;

                // 바늘 회전 (0~1로 반복)
                arrowValue += speed * Time.deltaTime;
                if (arrowValue >= 1f)
                    arrowValue -= 1f;

                pointer.localRotation = Quaternion.Euler(0, 0, -360f * arrowValue);

                // 입력 처리
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (arrowValue >= successStart && arrowValue <= successEnd)
                    {
                        Debug.Log("성공!");
                        successCount++;
                        break;
                    }
                    else
                    {
                        Debug.Log("실패!");
                        break;
                    }
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);

            // 종료 조건
            if (successCount >= requiredSuccess)
            {
                isSuccess = true;
                break;
            }
        }

        // 최종 판정
        isSuccess = successCount >= requiredSuccess;
        

        EndMiniGame();
    }

    public override void SetDifficulty()
    {
        
    }
}
