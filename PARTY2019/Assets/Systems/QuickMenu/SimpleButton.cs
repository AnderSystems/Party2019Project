using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine;

public class SimpleButton : MonoBehaviour
{
    public SimpleMenu Menu;
    public int Index { get; set; }
    public Animator Anim { get; set; }
    public GameConfiguration Settings;
    public TextMeshProUGUI Title;
    [Space]
    [TextArea]
    public string PTBR_Title = "Botão";
    [TextArea]
    public string ENUS_Title = "Button";
    [TextArea]
    public string SPN_Title = "Butón";
    [Space]
    public bool Activated = true;
    [Header("Animations")]
    public string AnimActivated = "Activated";
    public string AnimHover = "Hover";
    //[HideInInspector]
    [SerializeField]
    public SimpleButton LeftButton;
    //[HideInInspector]
    [SerializeField]
    public SimpleButton RightButton;
    //[HideInInspector]
    [SerializeField]
    public SimpleButton UpButton;
    //[HideInInspector]
    [SerializeField]
    public SimpleButton DownButton;
    public string Vertical = "Vertical";
    public string Horizontal = "Horizontal";

    void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    void Start()
    {
        Anim = GetComponent<Animator>();
        if (!Title)
        {
            if(GetComponent<TextMeshProUGUI>())
            {
                Title = GetComponent<TextMeshProUGUI>();
            } else
            {
                Title = GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        Anim.SetBool(AnimActivated, Activated);
        UpdateTitle();
    }

    void Update()
    {
        Navegate();
        if (Hover)
        {
            if (Input.GetButtonDown("Submit"))
            {
                ExecuteEvents.Execute<IPointerClickHandler>(this.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            } 
        }
    }

    bool NavegateActivated { get; set; } = true;
    public void EnableNavegate(bool Active)
    {
        NavegateActivated = Active;
    }
    public void ToggleNavegate()
    {
        EnableNavegate(!NavegateActivated);
    }

    void Navegate()
    {
        if (!NavegateActivated)
            return;
        if (!Hover)
            return;
        if (!Input.GetButtonDown(Vertical) && !Input.GetButtonDown(Horizontal))
            return;
        if (Input.GetAxisRaw(Vertical) > 0)
        {
            if (UpButton == null)
                return;
            UpButton.MouseOver(true);
        }

        if (Input.GetAxisRaw(Vertical) < 0)
        {
            if (DownButton == null)
                return;
            DownButton.MouseOver(true);
        }

        if (Input.GetAxisRaw(Horizontal) < 0)
        {
            if (LeftButton == null)
                return;
            LeftButton.MouseOver(true);
        }

        if (Input.GetAxisRaw(Horizontal) > 0)
        {
            if (RightButton == null)
                return;
            RightButton.MouseOver(true);
        }
    }

    public bool Hover;

    public void MouseOver(bool Over)
    {
        Hover = Over;
        if (Over)
        {
            Menu.ChangeButton(Index);
        }
        Anim.SetBool(AnimHover, Over);
        if (Over)
        {
            Menu.ActivateButton = Index;
        }
    }
    public void Activate(bool Active)
    {
        Activated = Active;
        Anim.SetBool(AnimActivated, Active);
    }

    /// <summary>
    /// Change Title
    /// </summary>
    /// <param name="PTBR">Text in Portuguese</param>
    /// <param name="ENUS">Text in English</param>
    /// <param name="SPN">Text in Spanish</param>
    public void SetTitle(string PTBR, string ENUS, string SPN)
    {
        PTBR_Title = PTBR;
        ENUS_Title = ENUS;
        SPN_Title = SPN;
        UpdateTitle();
    }

    /// <summary>
    /// Change Title for all avalible Languages
    /// </summary>
    /// <param name="Title">Title</param>
    public void SetTitle(string Title)
    {
        SetTitle(Title, Title, Title);
    }

    public void UpdateTitle()
    {
        if (Settings.GetLanguage() == GameConfiguration.GameLanguage.English)
        {
            Title.text = ENUS_Title;
        }

        if (Settings.GetLanguage() == GameConfiguration.GameLanguage.Espanol)
        {
            Title.text = SPN_Title;
        }

        if (Settings.GetLanguage() == GameConfiguration.GameLanguage.Português)
        {
            Title.text = PTBR_Title;
        }
    }
}
