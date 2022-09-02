public class MapEnvironment : SingletonBehaviour<MapEnvironment> {
    private void Awake() {
        InitializeSingleton(this);

        Disable();
    }

    public void Enable() {
        gameObject.SetActive(true);
    }
    
    public void Disable() {
        gameObject.SetActive(false);
    }
}