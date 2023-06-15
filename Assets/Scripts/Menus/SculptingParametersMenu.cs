using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Managers;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Menus
{
    public class SculptingParametersMenu : ParametersMenu
    {
        private readonly SculptingMenuIcon subtractIcon =
            new SculptingMenuIcon(GameObject.Find("Subtract"), GameManager.Instance.ActionsData.Sculpting, Sculpting.BooleanOperation.SUBTRACT);
        private readonly SculptingMenuIcon unionIcon =
            new SculptingMenuIcon(GameObject.Find("Union"), GameManager.Instance.ActionsData.Sculpting, Sculpting.BooleanOperation.UNION);
        private readonly SculptingMenuIcon intersectIcon =
            new SculptingMenuIcon(GameObject.Find("Intersect"), GameManager.Instance.ActionsData.Sculpting, Sculpting.BooleanOperation.INTERSECT);

        public SculptingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { subtractIcon, unionIcon, intersectIcon };
        }

    }
}