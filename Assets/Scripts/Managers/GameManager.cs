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
        public float CurrentLineThickness { get; } = 0.0f;
        public float MinStrokeWidth { get; } = 0.01f;
        public float MaxStrokeWidth { get; } = 0.5f;
        public float MinObjectSize { get; } = 0.2f;
        public float MaxObjectSize { get; } = 2f;
        public string PathToSaveFile { get; set; }
        public const string UniversalTag = "CreatedObjects";
        public const string NonSerializableTag = "NonSerializable";
        public const string LineName = "Line2D";
        public const string Line3DName = "Line3D";
        public const string Line3DCubeSegmentName = "Line3DCubeSegment";
        public const string Line3DCylinderSegmentName = "Line3DCylinderSegment";
        public const string PrimitiveObjectName = "PrimitiveObject";

        void Awake()
        {
            Instance = this;
            PathToSaveFile = Application.persistentDataPath + "/save.json";
        }

        void Start()
        {
            ActionsData = new ActionsData();
            CurrentAction = ActionsData.Selecting;
            CurrentAction.Init();
        }

        public void changeCurrentAction(Action action)
        {
            CurrentAction.Finish();
            CurrentAction = action;
            CurrentAction.Init();
        }
    }
}