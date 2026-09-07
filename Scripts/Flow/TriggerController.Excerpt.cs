// PORTFOLIO EXCERPT: contributions attributed to dev_slime in Git.
// Source: Assets/Scripts/System/TriggerController.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Supplements the other excerpts for this source file.
// Review only. Omitted team code and original scopes are not reconstructed.
#if false

// Original lines 28-31
                if (EventManager.Instance != null)
                    EventManager.Instance.StartEventSequence(seq);
                else
                    Debug.LogWarning("[TriggerController] EventManager 없음, Auto 이벤트 건너뜀: " + seq.eventID);

// Original lines 49-52
                if (EventManager.Instance != null)
                    EventManager.Instance.StartEventSequence(seq);
                else
                    Debug.LogWarning("[TriggerController] EventManager 없음, 위치 이벤트 건너뜀: " + seq.eventID);

// Original lines 63-63
            return;

// Original lines 67-68
            if (seq.triggerType == TriggerType.OnInteract &&
                !triggeredEventIDs.Contains(seq.eventID) &&

// Original lines 71-75
                if (EventManager.Instance != null)
                    EventManager.Instance.StartEventSequence(seq);
                else
                    Debug.LogWarning("[TriggerController] EventManager 없음, Interact 이벤트 건너뜀: " + seq.eventID);


// Original lines 91-95
                if (EventManager.Instance != null)
                    EventManager.Instance.StartEventSequence(seq);
                else
                    Debug.LogWarning("[TriggerController] EventManager 없음, TriggerByEventId 건너뜀: " + eventId);


#endif
