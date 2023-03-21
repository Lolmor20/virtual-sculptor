using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ColorPickingParametersMenu : ParametersMenu
    {
        private readonly ColorPickingMenuIcon predefinedRedIcon = new ColorPickingMenuIcon(GameObject.Find(IconObjectName.Red), GameManager.Instance.ActionsData.Selecting, Color.red) { DefaultColor = Color.red };
        private readonly ColorPickingMenuIcon predefinedGreenIcon = new ColorPickingMenuIcon(GameObject.Find(IconObjectName.Green), GameManager.Instance.ActionsData.Selecting, Color.green) { DefaultColor = Color.green };
        private readonly ColorPickingMenuIcon predefinedBlueIcon = new ColorPickingMenuIcon(GameObject.Find(IconObjectName.Blue), GameManager.Instance.ActionsData.Selecting, Color.blue) { DefaultColor = Color.blue };
        private readonly ColorPickingMenuIcon predefinedWhiteIcon = new ColorPickingMenuIcon(GameObject.Find(IconObjectName.Blue), GameManager.Instance.ActionsData.Selecting, Color.white) { DefaultColor = Color.white };
        private readonly ColorPickingMenuIcon predefinedBlackIcon = new ColorPickingMenuIcon(GameObject.Find(IconObjectName.Black), GameManager.Instance.ActionsData.Selecting, Color.black) { DefaultColor = Color.black };
        private readonly ColorPaletteIcon colorPalette = new ColorPaletteIcon(GameObject.Find(IconObjectName.ColorPalette), GameManager.Instance.ActionsData.Selecting);

        public ColorPickingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { predefinedRedIcon, predefinedGreenIcon, predefinedBlueIcon, predefinedWhiteIcon, predefinedBlackIcon, colorPalette };
        }
    }
}