using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Logo : MonoBehaviour {

    public GameObject TitleBackground = null;
    public GameObject TitleCanvas = null;

	// Use this for initialization
	void Start () {
        // 로고 페이드 효과
        var ren = GetComponent<SpriteRenderer>();
        ren.color = new Color(1, 1, 1, 0);
        Sequence sq = DOTween.Sequence()
            .AppendInterval(1.0f)
            .Append(ren.DOFade(1, 1.5f))
            .Append(ren.DOFade(0, 1.5f))
            // 시퀸스가 끝나면 콜백함수실행
            .AppendCallback(() =>
            {
                TitleBackground.SetActive(true);
                TitleCanvas.SetActive(true);

                // 타이틀 배경화면 페이드효과
                TitleBackground.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                TitleBackground.GetComponent<SpriteRenderer>().DOFade(1.0f, 1.0f);

                GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>().Play();
            });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
