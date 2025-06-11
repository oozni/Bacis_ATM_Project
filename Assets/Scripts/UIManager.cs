using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }

    public TextMeshProUGUI name;
    public TextMeshProUGUI balance;
    public TextMeshProUGUI money;

    [SerializeField] private TextMeshProUGUI depositTen;
    [SerializeField] private TextMeshProUGUI depositFiftiy;
    [SerializeField] private TextMeshProUGUI depositHundred;
    public TMP_InputField dpInputText;

    [SerializeField] private TextMeshProUGUI withdrawalTen;
    [SerializeField] private TextMeshProUGUI withdrawalFiftiy;
    [SerializeField] private TextMeshProUGUI withdrawalHundred;
    public TMP_InputField wdInputText;

    [Header("로그인 정보")]
    public TMP_InputField logID;
    public TMP_InputField logPW;

    [Header("회원가입 정보")]
    public TMP_InputField signID;
    public TMP_InputField signPW;
    public TMP_InputField signName;
    public TMP_InputField signPwChack;

    [HideInInspector] public int moneyTenThousands = 10000;
    [HideInInspector] public int moneyFiftyThousands = 50000;
    [HideInInspector] public int moneyHundredThousands = 100000;

    public List<GameObject> targetLayer;

    private void Start()
    {
        foreach (GameObject obj in targetLayer)
        {
            if (obj.CompareTag("DepositDetails") || obj.CompareTag("WithdrawalDetails") || obj.CompareTag("Insufficient"))
            {
                obj.SetActive(false);
            }
        }
        GameObject isSignUp = GameObject.FindWithTag("SignUp");
        if (isSignUp)
        {
            isSignUp.SetActive(false);
        }
    }
    public void OnUi()
    {
        name.text = GameManager.Instance._userDataBase.users[GameManager.Instance.userIndex].userName;
        balance.text = GameManager.Instance._userDataBase.users[GameManager.Instance.userIndex].balance.ToString("N0");
        money.text = GameManager.Instance._userDataBase.users[GameManager.Instance.userIndex].curCash.ToString("N0");

        depositTen.text = moneyTenThousands.ToString("N0");
        depositFiftiy.text = moneyFiftyThousands.ToString("N0");
        depositHundred.text = moneyHundredThousands.ToString("N0");

        withdrawalTen.text = moneyTenThousands.ToString("N0");
        withdrawalFiftiy.text = moneyFiftyThousands.ToString("N0");
        withdrawalHundred.text = moneyHundredThousands.ToString("N0");
    }
}
