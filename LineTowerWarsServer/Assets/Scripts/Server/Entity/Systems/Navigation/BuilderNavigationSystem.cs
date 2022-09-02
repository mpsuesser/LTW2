public class BuilderNavigationSystem : NavigationSystem {
    public BuilderNavigationSystem(
        ServerBuilder e
    ) : base(
        e,
        BuilderConstants.BuilderMoveSpeed
    ) {
        
    }
}