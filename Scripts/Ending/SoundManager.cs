using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("오디오 소스 연결")]
    public AudioSource bgmPlayer;
    public AudioSource sfxPlayer;

    [Header("사운드 파일 등록 (인스펙터에서 드래그)")]
    public AudioClip[] bgmClips; // 배경음악들
    public AudioClip[] sfxClips; // 효과음들

    // 이름으로 찾기 쉽게 딕셔너리로 변환
    private Dictionary<string, AudioClip> bgmDict = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    void Start()
    {
        PlayBGM("BGM1");
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 음악 끊기면 안 됨
            InitDict();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitDict()
    {
        foreach (var clip in bgmClips) bgmDict[clip.name] = clip;
        foreach (var clip in sfxClips) sfxDict[clip.name] = clip;
    }

    // BGM 재생 (이름으로 호출)
    public void PlayBGM(string name)
    {
        if (bgmDict.ContainsKey(name))
        {
            bgmPlayer.clip = bgmDict[name];
            bgmPlayer.loop = true;
            bgmPlayer.Play();
        }
        else Debug.LogWarning($"BGM 없음: {name}");
    }

    // 효과음 재생 (이름으로 호출)
    public void PlaySFX(string name)
    {
        if (sfxDict.ContainsKey(name))
        {
            sfxPlayer.PlayOneShot(sfxDict[name]);
        }
        else Debug.LogWarning($"SFX 없음: {name}");
    }
    
    // 소리 멈춤 (엔딩 크레딧 등에서 필요)
    public void StopBGM() => bgmPlayer.Stop();
}