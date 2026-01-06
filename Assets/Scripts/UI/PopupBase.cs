using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupBase : BaseUI
{
    [SerializeField] protected Button closeBtn;

    protected Action OnClose;

    protected virtual void Start()
    {
        closeBtn?.onClick.AddListener(HandleCloseButtonClick);
    }

    protected virtual void HandleCloseButtonClick()
    {
        Close();
    }

    public virtual void Open(Action onClose)
    {
        OnClose = onClose;
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        OnClose?.Invoke();
        OnClose = null;
        base.Close();
    }
}