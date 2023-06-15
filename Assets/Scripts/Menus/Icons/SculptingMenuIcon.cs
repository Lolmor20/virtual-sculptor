using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus.Icons
{
    public class SculptingMenuIcon : MenuIcon
    {
        private readonly Sculpting.BooleanOperation operationToPerform;
        public SculptingMenuIcon(GameObject icon, Action action, Sculpting.BooleanOperation operation) : base(icon, action)
        {
            this.operationToPerform = operation;
        }

        public override void Select()
        {
            SetSelectedColor();
            GameManager.Instance.ActionsData.Sculpting.SetCurrentOperation(operationToPerform);
            getIconsMenu().SelectedIcon = this;
        }
    }
}