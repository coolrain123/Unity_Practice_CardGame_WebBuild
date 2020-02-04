using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region KID
    [Header("卡片陣列")]
    public GameObject[] cards;
    [Header("發牌按鈕")]
    public Button btnGetCard;

    public Button reBtn;

    private int player, pc;     // 玩家、電腦卡片編號

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        
        reBtn.onClick.AddListener(GameReset);   //按下重來時呼叫  GameReset
    }

    void GameReset()
    {        
            Application.LoadLevel(0);   //跳到場景(0)
    }

    /// <summary>
    /// 玩家取得卡片
    /// </summary>
    public void PlayerGetCard()
    {
        btnGetCard.interactable = false;  //發卡鈕變成不能按

        player = GetCard(new Vector3(0, -3, 0));

        Invoke("PcGetCard", 1.5f);
        Invoke("GameWinner", 2.5f);
    }

    /// <summary>
    /// 電腦取得卡片
    /// </summary>
    private void PcGetCard()
    {
        pc = GetCard(new Vector3(0, 3, 0));
    }

    /// <summary>
    /// 取得卡片
    /// </summary>
    /// <param name="pos">卡片座標</param>
    /// <returns>取得的卡片編號</returns>
    private int GetCard(Vector3 pos)
    {
        aud.PlayOneShot(soundGetCard);

        int r = Random.Range(0, cards.Length);

        Instantiate(cards[r], pos, Quaternion.Euler(0, 180, 0));

        return r + 1;
    }
    #endregion

    #region 練習區域
    [Header("音效區域")]
    public AudioClip soundGetCard;  // 發牌
    public AudioClip soundWin;      // 獲勝
    public AudioClip soundLose;     // 失敗
    public AudioClip soundTie;      // 平手

    private AudioSource aud;        // 音效來源：喇叭

    public GameObject re;
    public GameObject gametext;
    
    /// <summary>
    /// 勝負顯示：使用玩家與電腦取得卡片判斷獲勝、平手或失敗
    /// 玩家卡片編號：player
    /// 電腦卡片編號：pc
    /// 顯示結算畫面
    /// </summary>
    private void GameWinner()
    {
        if (player > pc)
        {
            re.SetActive(true);
            aud.PlayOneShot(soundWin, 1f);
        }
        else if(player < pc)
        {
            gametext.GetComponent<Text>().text = "失敗";
            re.SetActive(true);
            aud.PlayOneShot(soundLose, 1f);
        }
        else if(player == pc)
        {
            gametext.GetComponent<Text>().text = "平手";
            re.SetActive(true);
            aud.PlayOneShot(soundTie, 1f);
        }
            
    }

    

    #endregion
}
