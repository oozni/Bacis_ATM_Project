using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TransactionUI : MonoBehaviour
{
    public UIManager uiManager;

    public void DepositButton() // 입금 버튼
    {
        foreach (GameObject obj in uiManager.targetLayer)
        {
            obj.SetActive(obj.CompareTag("DepositDetails"));
        }
    }
    public void WithdrawalButton() // 출금 버튼
    {
        foreach (GameObject obj in uiManager.targetLayer)
        {
            obj.SetActive(obj.CompareTag("WithdrawalDetails"));
        }
    }
    public void BackButton() // 뒤로가기 버튼
    {
        foreach (GameObject obj in uiManager.targetLayer)
        {
            bool isTransaction = obj.CompareTag("Withdrawal") || obj.CompareTag("Deposit");

            uiManager.dpInputText.text = null;
            obj.SetActive(isTransaction);
        }
    }
    public void AddMoney(GameObject clickedButton) // 통장에 입금되는 함수
    {
        var data = GameManager.Instance._userDataBase.users[GameManager.Instance.userIndex];

        TextMeshProUGUI uiText = clickedButton.GetComponent<TextMeshProUGUI>();
        string inputText = uiManager.dpInputText.text;

        if (uiText != null && int.TryParse(uiText.text.Replace(",", ""), out int amount))
        {
            if (data.curCash >= amount)
            {
                data.balance += amount;
                data.curCash -= amount;

                uiManager.OnUi();
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

                uiManager.OnUi();
                GameManager.Instance.JsonSave();

            }
            else
            {
                InsufficientOpen();
            }
        }
    }
    public void SubtractMoney(GameObject clickedButton)
    {
        var data = GameManager.Instance._userDataBase.users[GameManager.Instance.userIndex];

        TextMeshProUGUI uiText = clickedButton.GetComponent<TextMeshProUGUI>();
        string inputText = uiManager.wdInputText.text;

        if (uiText != null && int.TryParse(uiText.text.Replace(",", ""), out int amount))
        {
            if (data.balance >= amount)
            {
                data.balance -= amount;
                data.curCash += amount;

                uiManager.OnUi();
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

                uiManager.OnUi();
                GameManager.Instance.JsonSave();

            }
            else
            {
                InsufficientOpen();
            }
        }
    }
    public void InsufficientOpen()
    {
        foreach (GameObject obj in uiManager.targetLayer)
        {
            if (obj.CompareTag("Insufficient"))
            {
                obj.SetActive(true);
            }
        }
    }
    public void InsufficientClose()
    {
        foreach (GameObject obj in uiManager.targetLayer)
        {
            if (obj.CompareTag("Insufficient"))
            {
                obj.SetActive(false);
            }
        }
    }
}