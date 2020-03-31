using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class LoginScript : MonoBehaviour
{
    // 新規登録
    public void OnClick_Signin()
    {
        //　Userインスタンスの生成
        NCMBUser user = new NCMBUser();

        // ユーザー名・パスワードを設定
        user.UserName = GameObject.Find("Text_UserName").GetComponent<Text>().text;
        user.Password = GameObject.Find("Text_Password").GetComponent<Text>().text;

        // ユーザーの新規登録処理
        user.SignUpAsync((NCMBException e) =>
        {
            if (e != null)
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = string.Format("ユーザーの新規登録失敗しました。\n{0}", e.ErrorMessage);
            }
            else
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "ユーザーの新規登録成功しました。";

            }
        });
    }

    // ログイン
    public void OnClick_Login()
    {
        // ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync(
            GameObject.Find("Text_UserName").GetComponent<Text>().text,
            GameObject.Find("Text_Password").GetComponent<Text>().text,
            (NCMBException e) =>
        {
            if (e != null)
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = string.Format("ログインに失敗しました。\n{0}", e.ErrorMessage);
            }
            else
            {
                GameObject.Find("Text_Log").GetComponent<Text>().text = "ログインに成功しました。";
                SceneManager.LoadScene(Define.GAME_LOBY);

            }
        });
    }
}
