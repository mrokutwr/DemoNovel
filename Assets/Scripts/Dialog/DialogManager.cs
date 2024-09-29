using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public event Action<DialogFrame> OnShowDialogFrame;
    public event Action<string, string> OnCharacterEnter;
    [Inject] private AssetLoader assetLoader;
    [Inject] private DialogParser dialogParser;
    [SerializeField] private string dialogFileAddress;

    private List<DialogFrame> dialogFrames = new List<DialogFrame>();
    private int currentFrameIndex = 0;
    private bool isWaitingForInput = false;

    private void Start()
    {
        LoadDialogFrames().Forget();
    }

    private async UniTask LoadDialogFrames()
    {
        if (string.IsNullOrEmpty(dialogFileAddress))
        {
            Debug.LogWarning("Dialog file address is not assigned!");
            return;
        }

        TextAsset dialogFile = await assetLoader.LoadAsset<TextAsset>(dialogFileAddress);
        dialogFrames = dialogParser.Parse(dialogFile);

        await WaitForInputToStart();
    }

    private async UniTask WaitForInputToStart()
    {
        await WaitForInput();
        await ShowNextFrame();
    }

    private async UniTask ShowNextFrame()
    {
        if (currentFrameIndex >= dialogFrames.Count)
            return;

        var dialogFrame = dialogFrames[currentFrameIndex];
        OnShowDialogFrame?.Invoke(dialogFrame);
        OnCharacterEnter?.Invoke(dialogFrame.CharacterName, dialogFrame.Direction);

        await WaitForInput();
        NextFrame();
    }

    private async UniTask WaitForInput()
    {
        isWaitingForInput = true;
        while (isWaitingForInput)
        {
            if (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0) && !IsPointerOverUI()))
            {
                isWaitingForInput = false;
            }
            
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !IsPointerOverUI())
            {
                isWaitingForInput = false;
            }

            await UniTask.Yield();
        }
    }

    private bool IsPointerOverUI()
    {
        var data = PointerRaycast(Input.mousePosition);
            if (data.gameObject != null)
        {
            if (data.gameObject.layer == LayerMask.NameToLayer("UI"))
            {
                return true;
            }
        }
        return false;
    }
    private RaycastResult PointerRaycast(Vector2 screenPosition)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        return results.Count > 0 ? results[0] : new RaycastResult();
    }
    
    private void NextFrame()
    {
        Debug.Log($"Current Frame Index: {currentFrameIndex}, Total Frames: {dialogFrames.Count}");
        if (currentFrameIndex < dialogFrames.Count - 1)
        {
            currentFrameIndex++;
            ShowNextFrame().Forget();
        }
        else
        {
            DialogFrame dialogFrame = new DialogFrame("Autor",null,"endGame");
            OnShowDialogFrame?.Invoke(dialogFrame);
            Debug.Log("No more frames to show.");
        }
    }
}