using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Color CurrentColor { get; set; } = Color.magenta;
        public Action CurrentAction { get; set; }
        public ActionsData ActionsData { get; set; }
        public float CurrentLineThickness { get; set; } = 0.0f;
        public float MinStrokeWidth { get; set; } = 0.5f;
        public float MaxStrokeWidth { get; set; } = 5f;
        public float MinObjectSize { get; set; } = 0.2f;
        public float MaxObjectSize { get; set; } = 2f;
        public string PathToSaveFile { get; set; }
        public float LineSize { get; set; } = 0.5f;
        public Material LineMaterial { get; set; }

        void Awake()
        {
            Instance = this;
            PathToSaveFile = Application.persistentDataPath + "/save.json";
            LineMaterial = new Material(Shader.Find("Standard"));
            //LineMaterial = Resources.Load<Material>("Materials/ComicMat");
        }

        void Start()
        {
            ActionsData = new ActionsData();
            CurrentAction = ActionsData.Selecting;
            CurrentAction.Init();
            ActionsData.LineDrawing.InstantiateDrawingTool();
        }

        public void changeCurrentAction(Action action)
        {
            CurrentAction.Finish();
            CurrentAction = action;
            CurrentAction.Init();
        }
    }
}