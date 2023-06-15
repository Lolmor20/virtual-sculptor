using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class ObjectTypeMenuIcon : MenuIcon
    {
        private Actions.ObjectAdding.ObjectType objectType;

        public ObjectTypeMenuIcon(GameObject icon, Action action, ObjectAdding.ObjectType objectType) : base(icon, action)
        {
            this.objectType = objectType;
        }

        public override void Select()
        {
            GameManager.Instance.ActionsData.ObjectAdding.SetObjectType(objectType);
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
        }
    }
}