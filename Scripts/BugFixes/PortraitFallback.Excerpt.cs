// PORTFOLIO EXCERPT: selected contributions by dev_slime.
// Source: Assets/Scripts/System/GameManager.cs
// Revision: 608c01a388656b70295252ce72b51114897ecb6b
// Review only; omitted team code and dependencies are not included.
// Blocks retain their original scope; this is not a standalone Unity component.
#if false

// Original lines 157-178
                Sprite ptrSprite = null;
                if (isNpc && talkData.portraitIndex >= 0 && scanObject != null)
                {
                    ObjectData objData = scanObject.GetComponent<ObjectData>();
                    if (objData != null)
                    {
                        ptrSprite = objData.GetPortrait(talkData.portraitIndex);
                    }
                }

                if (ptrSprite != null)
                {
                    // 이미지가 진짜로 있을 때만 보여줌
                    portraitImg.sprite = ptrSprite;
                    portraitImg.color = new Color(1, 1, 1, 1); 
                }
                else
                {
                    // 이미지가 없으면 투명하게 숨김 (White Box 방지)
                    portraitImg.color = new Color(1, 1, 1, 0); 
                    portraitImg.sprite = null; // 잔상 제거
                }

#endif
