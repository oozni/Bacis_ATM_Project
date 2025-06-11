using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class LogIn : MonoBehaviour
{
    [SerializeField] private GameObject SignUp;
    [SerializeField] private GameObject SignIn;

    public void SignInButton() // 로그인 버튼
    {
        var allUser = GameManager.Instance._userDataBase.users;

        for (int i = 0; GameManager.Instance._userDataBase.users.Count > 0; i++)
        {
            if (allUser[i].signID == UIManager.Instance.logID.text && allUser[i].signPW == UIManager.Instance.logPW.text)
            {
                GameManager.Instance.userIndex = i;
                SignIn.SetActive(false);
                UIManager.Instance.OnUi();
                return;
            }
            else
            {
                Debug.Log("아이디 또는 비밀번호가 맞지 않습니다.");
            }

        }
    }

    public void SignUpButton() // 회원가입 시작하는 버튼
    {
        SignUp.SetActive(true);
    }

    public void CancelButton()
    {
        UIManager.Instance.signID = null;
        UIManager.Instance.signPW= null;
        UIManager.Instance.signPwChack = null;
        UIManager.Instance.signName = null;
        SignUp.SetActive(false);
    }

    public void SiguUp() // 회원가입 완료 버튼
    {
        string signID = UIManager.Instance.signID.text;
        string signPW = UIManager.Instance.signPW.text;
        string signName = UIManager.Instance.signName.text;

        foreach (var uesrs in GameManager.Instance._userDataBase.users)
        {
            // json에 저장되어있는 아이디랑 입력하는 아이디가 같으면 오류창 출력
            if (uesrs.signID == signID)
            {
                Debug.Log($"이미 있는 아이디 입니다. : {uesrs.signID}");
                return;
            }
        }

        // 회원가입 창에서 패스워드와 패스워드 재확인 입력값이 다르다면 오류창 출력
        if (signPW != UIManager.Instance.signPwChack.text)
        {
            Debug.Log($"비밀번호 체크 : {UIManager.Instance.signPW.text}, {UIManager.Instance.signPwChack.text}");
            Debug.Log("Password Check");
            return;
        }

        UserData userData = new UserData(signName, signID, signPW);
        GameManager.Instance._userDataBase.users.Add(userData);

        GameManager.Instance.JsonSave();

        SignUp.SetActive(false);
    }
}
