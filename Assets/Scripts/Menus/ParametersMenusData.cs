using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ParametersMenusData
    {
        public ColorPickingParametersMenu ColorPickingParametersMenu { get; set; } = new ColorPickingParametersMenu(GameObject.Find(ParametersMenuObjectName.ColorPicking));
        public LineDrawingParametersMenu LineDrawingParametersMenu { get; set; } = new LineDrawingParametersMenu(GameObject.Find(ParametersMenuObjectName.LineDrawing));
        public ObjectSelectingParametersMenu ObjectSelectingParametersMenu { get; set; } = new ObjectSelectingParametersMenu(GameObject.Find(ParametersMenuObjectName.ObjectSelecting));
        public SavingLoadingParametersMenu SavingLoadingParametersMenu { get; set; } = new SavingLoadingParametersMenu(GameObject.Find(ParametersMenuObjectName.SavingLoading));
        public ObjectAddingParametersMenu ObjectAddingParametersMenu { get; set; } = new ObjectAddingParametersMenu(GameObject.Find(ParametersMenuObjectName.ObjectAdding));
        public SkyboxSelectingParametersMenu SkyboxSelectingParametersMenu { get; set; } = new SkyboxSelectingParametersMenu(GameObject.Find(ParametersMenuObjectName.SkyboxSelecting));
        public ClearSceneParametersMenu ClearSceneParametersMenu { get; set; } = new ClearSceneParametersMenu(GameObject.Find(ParametersMenuObjectName.ClearScene));
    }
}