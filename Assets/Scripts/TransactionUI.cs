using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TransactionUI : MonoBehaviour
{
    public void DepositButton() // 입금 버튼
    {
        foreach (GameObject obj in UIManager.Instance.targetLayer)
        {
            obj.SetActive(obj.CompareTag("DepositDetails"));
        }
    }
    public void WithdrawalButton() // 출금 버튼
    {
        foreach (GameObject obj in UIManager.Instance.targetLayer)
        {
            obj.SetActive(obj.CompareTag("WithdrawalDetails"));
        }
    }
    public void BankTransferButton() // 이체 버튼
    {
        foreach (GameObject obj in UIManager.Instance.targetLayer)
        {
            obj.SetActive(obj.CompareTag("Transfer"));
        }
    }
    public void BackButton() // 뒤로가기 버튼
    {
        foreach (GameObject obj in UIManager.Instance.targetLayer)
        {
            bool isTransaction = obj.CompareTag("Withdrawal") || obj.CompareTag("Deposit");

            UIManager.Instance.dpInputText.text = null;
            obj.SetActive(isTransaction);
        }
    }
    public void AddMoney(GameObject clickedButton) // 통장에 입금되는 함수
    {
        var data = GameManager.Instance._userDataBase.users[GameManager.Instance.userIndex];

        TextMeshProUGUI uiText = clickedButton.GetComponent<TextMeshProUGUI>();
        string inputText = UIManager.Instance.dpInputText.text;

        if (uiText != null && int.TryParse(uiText.text.Replace(",", ""), out int amount))
        {
            if (data.curCash >= amount)
            {
                data.balance += amount;
                data.curCash -= amount;

                UIManager.Instance.OnUi();
                GameManager.Instance.JsonSave();
            }
            else
            {
                InsufficientOpen();
            }
        }
        if (inputText != null && int.TryParse(inputText, out int value))
        {
            if (data.curCash >= value)
            {
                data.balance += value;
                data.curCash -= value;

                UIManager.Instance.OnUi();
                GameManager.Instance.JsonSave();

            }
            else
            {
                InsufficientOpen();
            }
        }
    }
    public void SubtractMoney(GameObject clickedButton) // 통장에서 출금되는 함수
    {
        var data = GameManager.Instance._userDataBase.users[GameManager.Instance.userIndex];

        TextMeshProUGUI uiText = clickedButton.GetComponent<TextMeshProUGUI>();
        string inputText = UIManager.Instance.wdInputText.text;

        if (uiText != null && int.TryParse(uiText.text.Replace(",", ""), out int amount))
        {
            if (data.balance >= amount)
            {
                data.balance -= amount;
                data.curCash += amount;

                UIManager.Instance.OnUi();
                GameManager.Instance.JsonSave();

            }
            else
            {
                InsufficientOpen();
            }
        }
        if (inputText != null && int.TryParse(inputText, out int value))
        {
            if (data.balance >= value)
            {
                data.balance -= value;
                data.curCash += value;

                UIManager.Instance.OnUi();
                GameManager.Instance.JsonSave();

            }
            else
            {
                InsufficientOpen();
            }
        }
    }

    public void TransferButton() // 계좌 이체 함수
    {
        var alluser = GameManager.Instance._userDataBase.users;

        for (int i = 0; i < alluser.Count; i++)
        {
            // json에 저장되어 있는 아이디 중에 내가 입력한 아이디가 같다면
            if (alluser[i].signID == UIManager.Instance.tranferID.text)
            {
                // 입력받은 값을 문자열 변수에 저장
                string inputText = UIManager.Instance.tranferGold.text;

                // 문자열 변수에 저장된 숫자를 인트형으로 바꾼다.
                if (inputText != null && int.TryParse(inputText, out int value))
                {
                    alluser[GameManager.Instance.userIndex].balance -= value;
                    alluser[i].balance += value;
                    
                    UIManager.Instance.OnUi();
                    GameManager.Instance.JsonSave();
                    
                    return;
                }
            }
        }
        if (alluser[GameManager.Instance.userIndex].signID != UIManager.Instance.tranferID.text)
        {
            Debug.Log("없는 아이디 입니다.");
        }
    }
    public void InsufficientOpen()
    {
        foreach (GameObject obj in UIManager.Instance.targetLayer)
        {
            if (obj.CompareTag("Insufficient"))
            {
                obj.SetActive(true);
            }
        }
    }
    public void InsufficientClose()
    {
        foreach (GameObject obj in UIManager.Instance.targetLayer)
        {
            if (obj.CompareTag("Insufficient"))
            {
                obj.SetActive(false);
            }
        }
    }
}