using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // 시작 버튼 누르면 실행될 함수
    public void OnClickStart()
    {
        // "Act 1" 씬으로 이동 (이름이 다르면 꼭 수정하세요!)
        SceneManager.LoadScene("Act 1");
    }

    // 종료 버튼 누르면 실행될 함수
    public void OnClickExit()
    {
        Debug.Log("게임 종료!"); // 에디터에서는 꺼지지 않으므로 로그로 확인
        Application.Quit();
    }
}