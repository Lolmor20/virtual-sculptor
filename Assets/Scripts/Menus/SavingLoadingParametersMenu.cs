using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class SavingLoadingParametersMenu : ParametersMenu
    {
        private readonly SavingMenuIcon savingIcon1 = new SavingMenuIcon(GameObject.Find(IconObjectName.Saving1), GameManager.Instance.ActionsData.Selecting, 1);
        private readonly SavingMenuIcon savingIcon2 = new SavingMenuIcon(GameObject.Find(IconObjectName.Saving2), GameManager.Instance.ActionsData.Selecting, 2);
        private readonly SavingMenuIcon savingIcon3 = new SavingMenuIcon(GameObject.Find(IconObjectName.Saving3), GameManager.Instance.ActionsData.Selecting, 3);
        private readonly LoadingMenuIcon loadingIcon1 = new LoadingMenuIcon(GameObject.Find(IconObjectName.Loading1), GameManager.Instance.ActionsData.Selecting, 1);
        private readonly LoadingMenuIcon loadingIcon2 = new LoadingMenuIcon(GameObject.Find(IconObjectName.Loading2), GameManager.Instance.ActionsData.Selecting, 2);
        private readonly LoadingMenuIcon loadingIcon3 = new LoadingMenuIcon(GameObject.Find(IconObjectName.Loading3), GameManager.Instance.ActionsData.Selecting, 3);
        public SavingLoadingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { savingIcon1, savingIcon2, savingIcon3, loadingIcon1, loadingIcon2, loadingIcon3 };
        }
    }
}