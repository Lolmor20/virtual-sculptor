using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ObjectAddingParametersMenu : ParametersMenu
    {
        private readonly ObjectSizeSlider objectSizeSlider = new ObjectSizeSlider(GameObject.Find("Object Size Slider"), GameManager.Instance.ActionsData.Selecting);
        private readonly ObjectTypeMenuIcon cubeIcon = new ObjectTypeMenuIcon(GameObject.Find("Cube Icon"), GameManager.Instance.ActionsData.Selecting, Actions.ObjectAdding.ObjectType.Cube);
		private readonly ObjectTypeMenuIcon cylinderIcon = new ObjectTypeMenuIcon(GameObject.Find("Cylinder Icon"), GameManager.Instance.ActionsData.Selecting, Actions.ObjectAdding.ObjectType.Cylinder);
		private readonly ObjectTypeMenuIcon sphereIcon = new ObjectTypeMenuIcon(GameObject.Find("Sphere Icon"), GameManager.Instance.ActionsData.Selecting, Actions.ObjectAdding.ObjectType.Sphere);
		private readonly ObjectTypeMenuIcon capsuleIcon = new ObjectTypeMenuIcon(GameObject.Find("Capsule Icon"), GameManager.Instance.ActionsData.Selecting, Actions.ObjectAdding.ObjectType.Capsule);
		private readonly ObjectTypeMenuIcon squareIcon = new ObjectTypeMenuIcon(GameObject.Find("Square Icon"), GameManager.Instance.ActionsData.Selecting, Actions.ObjectAdding.ObjectType.Square);
		private readonly ObjectTypeMenuIcon triangleIcon = new ObjectTypeMenuIcon(GameObject.Find("Triangle Icon"), GameManager.Instance.ActionsData.Selecting, Actions.ObjectAdding.ObjectType.Triangle);
		private readonly ObjectTypeMenuIcon pyramidIcon = new ObjectTypeMenuIcon(GameObject.Find("Pyramid Icon"), GameManager.Instance.ActionsData.Selecting, Actions.ObjectAdding.ObjectType.Pyramid);


        public ObjectAddingParametersMenu(GameObject gameObject) : base(gameObject)
        {
			icons = new List<MenuIcon> { objectSizeSlider, sphereIcon, cylinderIcon, cubeIcon, capsuleIcon, squareIcon, triangleIcon, pyramidIcon };
        }

    }
}