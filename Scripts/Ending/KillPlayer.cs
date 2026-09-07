using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    void Awake()
    {
        // 씬에 있는 "Player" 태그나 이름을 가진 녀석을 찾아서 즉시 파괴
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
        
        // 혹시 이름으로 찾을 경우를 대비해 하나 더
        GameObject playerByName = GameObject.Find("Player");
        if (playerByName != null)
        {
            Destroy(playerByName);
        }
    }
}