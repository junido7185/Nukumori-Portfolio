// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/Cutscene/CutsceneManager.cs
// Revision: 801005d2e168c31afabeaeb62f64e60e9b88e740
// Historical prototype; not part of the current integrated project.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 1-65
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class CutsceneManager : MonoBehaviour
{
    [Header("Cinemachine Cameras")]
    public CinemachineCamera playerCam;
    public CinemachineCamera cutsceneCam;
    public CinemachineCamera dialogueCam;

    [Header("Transition Settings")]
    public float blendDuration = 1f;
    public AnimationCurve blendCurve;

    private CinemachineCamera currentCam;

    void Start()
    {
        currentCam = playerCam;
        ActivateCam(playerCam);
        DeactivateCams(cutsceneCam, dialogueCam);
    }

    public void ActivateCutscene(Transform focus, float orthoSize = -1)
    {
        if (orthoSize > 0)
            cutsceneCam.Lens = cutsceneCam.Lens.With { OrthographicSize = orthoSize };

        cutsceneCam.Target.TrackingTarget = focus;
        cutsceneCam.Target.LookAtTarget = focus;
        StartCoroutine(BlendTo(cutsceneCam));
    }

    public void ActivateDialogue(Transform speaker, float orthoSize = -1)
    {
        if (orthoSize > 0)
            dialogueCam.Lens = dialogueCam.Lens.With { OrthographicSize = orthoSize };

        dialogueCam.Target.TrackingTarget = speaker;
        dialogueCam.Target.LookAtTarget = speaker;
        StartCoroutine(BlendTo(dialogueCam));
    }

    public void RestorePlayerCam()
    {
        StartCoroutine(BlendTo(playerCam));
    }

    private IEnumerator BlendTo(CinemachineCamera nextCam)
    {
        DeactivateCams(currentCam);
        ActivateCam(nextCam);

        currentCam = nextCam;
        yield return new WaitForSeconds(blendDuration);
    }

    private void ActivateCam(CinemachineCamera cam) => cam.Priority = 20;
    private void DeactivateCams(params CinemachineCamera[] cams)
    {
        foreach (var c in cams)
            c.Priority = 0;
    }
}

#endif
