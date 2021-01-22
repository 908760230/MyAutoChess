using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 在英雄模型上 显示生命条
public class HealthBar : MonoBehaviour
{
    private GameObject championGO;
    private ChampionController championController;
    public Image fillImage;

    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(championGO != null)
        {
            this.transform.position = championGO.transform.position;
            fillImage.fillAmount = championController.currentHealth;
            if (championController.currentHealth <= 0) canvasGroup.alpha = 0;
            else canvasGroup.alpha = 1;
        }
    }
    public void Init(GameObject _championGO)
    {   // 当 英雄被创建时调用
        championGO = _championGO;
        championController = championGO.GetComponent<ChampionController>();
    }
}
