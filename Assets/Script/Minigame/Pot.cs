using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pot : CookingMiniGame
{
    [SerializeField]private RectTransform target;
    [SerializeField] private RectTransform playerBar;
    [SerializeField] private RectTransform playArea;
    [SerializeField] private Slider SuccessGage;

    [SerializeField] private float barSpeed;
    [SerializeField] private float targetSpeed;
    [SerializeField] private float moveNext = 0f; // 좌표 이동 랜덤 타임
    private void Start()
    {
        gameName = "Pot";
        StartCoroutine(Play());
    }

    public override IEnumerator Play()
    {
        StartMiniGame();
            // 초기값 세팅
        Vector2 targetPos = target.anchoredPosition;
        Vector2 barPos = playerBar.anchoredPosition;

        float moveArea = playArea.rect.width;
        while(true)
        {
                   //플레이어 이동
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                barPos.x -= barSpeed * Time.deltaTime;
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                barPos.x += barSpeed * Time.deltaTime;
            }

            float limit = (moveArea / 2) - (playerBar.rect.width / 2);
            barPos.x = Mathf.Clamp(barPos.x, -limit, limit);
            playerBar.anchoredPosition = barPos;

            // 아이콘 이동
            
            moveNext -= Time.deltaTime;
            if(moveNext <= 0f)
            {
                targetPos.x = Random.Range(-limit, limit);
                target.DOKill();
                target.DOAnchorPosX(targetPos.x, targetSpeed).SetEase(Ease.Linear);
                moveNext = Random.Range(0.5f, 1.5f);
            }

            //충돌 처리
            float barHalf = playerBar.rect.width / 2;
            float targetHalf = target.rect.width / 2;

            float barLeft = barPos.x - barHalf;
            float barRight = barPos.x + barHalf;
            float targetLeft = target.anchoredPosition.x - targetHalf;
            float targetRight = target.anchoredPosition.x + targetHalf;

            bool overlap = !(targetRight < barLeft || targetLeft > barRight);
            
            if(overlap)
            {
                SuccessGage.value += 0.3f * Time.deltaTime;
                
            }
            else
            {
                SuccessGage.value -= 0.3f * Time.deltaTime;
                
            }

            if(SuccessGage.value <= 0f)
            {
                target.DOKill();
                break;
            }
            else if(SuccessGage.value >= 1)
            {
                isSuccess = true;
                target.DOKill();
                break;
            }

                yield return null;
        }


        EndMiniGame();
        
    }

    public override void SetDifficulty()
    {
        
    }
}
