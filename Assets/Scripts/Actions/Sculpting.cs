namespace Assets.Scripts.Actions
{
    public class Sculpting : Action
    {
        private bool sculpting = false;

        public enum BooleanOperation
        {
            UNION,
            INTERSECT,
            SUBTRACT
        }

        public override void HandleTriggerDown()
        {
            sculpting = true;
        }

        public override void HandleTriggerUp()
        {
            sculpting = false;
        }

        public override void Update()
        {
        }

        public override void Init()
        {
        }

        public override void Finish()
        {
        }

        public void SetCurrentOperation(BooleanOperation operation)
        {
            //this.currentOperation = operation;
        }
    }
}