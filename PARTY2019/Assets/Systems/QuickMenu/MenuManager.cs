using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] LoadingScreens;
    [Space]
    [Space]
    public int ActiveMenu = -1;
    public _Menu CurrentMenu;
    [System.Serializable]
    public class _Menu
    {
        [HideInInspector]
        public string MenuName;
        public int MenuIndex { get; set; }
        public SimpleMenu MenuObject;
        [Space]
        public bool SetPrevMenuAutomatic;
        public int PrevMenu;
    }
    [SerializeField]
    public _Menu[] Menus;

    void Start()
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            Menus[i].MenuIndex = i;
        }

        if(ActiveMenu != -1)
        {
            GoToMenu(ActiveMenu);
        }
    }

    public void GoToMenu(int Menu)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            Menus[i].MenuObject.gameObject.SetActive(false);
        }

        if (Menus[Menu].SetPrevMenuAutomatic)
        {
            Menus[Menu].PrevMenu = CurrentMenu.MenuIndex;
        }

        ActiveMenu = Menu;
        CurrentMenu = Menus[Menu];
        CurrentMenu.MenuObject.gameObject.SetActive(true);
    }
    public void GoToMenu(string Menu)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            Menus[i].MenuObject.gameObject.SetActive(false);
            if(Menus[i].MenuName.Remove(0,3) == Menu)
            {
                CurrentMenu = Menus[i];
            }
        }

        ActiveMenu = CurrentMenu.MenuIndex;
        CurrentMenu.MenuObject.gameObject.SetActive(true);
    }

    public void Back()
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            Menus[i].MenuObject.gameObject.SetActive(false);
        }

        CurrentMenu = Menus[CurrentMenu.PrevMenu];
        CurrentMenu.MenuObject.gameObject.SetActive(true);
    }

    public void EnableLoadScreens(int LoadScreen)
    {
        for (int i = 0; i < LoadingScreens.Length; i++)
        {
            LoadingScreens[i].SetActive(false);
        }
        LoadingScreens[LoadScreen].SetActive(true);
    }
    public void DisableAllLoadScreens()
    {
        for (int i = 0; i < LoadingScreens.Length; i++)
        {
            LoadingScreens[i].SetActive(false);
        }
    }
}
