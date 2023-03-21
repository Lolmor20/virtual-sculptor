using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ObjectAddingParametersMenu : ParametersMenu
    {
        private readonly ObjectSizeSlider objectSizeSlider = new ObjectSizeSlider(GameObject.Find(IconObjectName.ObjectSizeSlider), GameManager.Instance.ActionsData.Selecting);
        private readonly ObjectTypeMenuIcon cubeIcon = new ObjectTypeMenuIcon(GameObject.Find(IconObjectName.Cube), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Cube);
        private readonly ObjectTypeMenuIcon cylinderIcon = new ObjectTypeMenuIcon(GameObject.Find(IconObjectName.Cylinder), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Cylinder);
        private readonly ObjectTypeMenuIcon sphereIcon = new ObjectTypeMenuIcon(GameObject.Find(IconObjectName.Sphere), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Sphere);
        private readonly ObjectTypeMenuIcon capsuleIcon = new ObjectTypeMenuIcon(GameObject.Find(IconObjectName.Capsule), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Capsule);

        public ObjectAddingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { objectSizeSlider, sphereIcon, cylinderIcon, cubeIcon, capsuleIcon };
        }

    }
}