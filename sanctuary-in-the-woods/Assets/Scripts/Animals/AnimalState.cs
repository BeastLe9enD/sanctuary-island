namespace Animals {
    public abstract class AnimalState {
        public abstract void OnEnter(AnimalStateManager manager);
        public abstract void OnUpdate(AnimalStateManager manager);
        public abstract void OnFixedUpdate(AnimalStateManager manager);

        public abstract void OnCollisionEnter(AnimalStateManager manager);
    }
}