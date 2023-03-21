using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class LineDrawingParametersMenu : ParametersMenu
    {
        private readonly LineThicknessSlider lineThicknessSlider = new LineThicknessSlider(GameObject.Find(IconObjectName.LineThicknessSlider), GameManager.Instance.ActionsData.Selecting);
        private readonly LineTypeMenuIcon lineRendererIcon = new LineTypeMenuIcon(GameObject.Find(IconObjectName.LineRenderer), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.LineRenderer);
        private readonly LineTypeMenuIcon cylinderIcon = new LineTypeMenuIcon(GameObject.Find(IconObjectName.CylinderLine), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Cylinder);
        private readonly LineTypeMenuIcon cubeIcon = new LineTypeMenuIcon(GameObject.Find(IconObjectName.CubeLine), GameManager.Instance.ActionsData.Selecting, LineDrawing.LineType.Cube);

        public LineDrawingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { lineThicknessSlider, lineRendererIcon, cylinderIcon, cubeIcon };
        }
    }
}