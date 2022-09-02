public class PersonalEventFeed : EventFeed {
    protected override bool PassesFilter(EFEvent efEvent) {
        switch (efEvent) {
            case EFEvent_LivesExchanged leEvent:
                Lane lane = ClientLaneTracker.Singleton.MyLane;
                
                return
                    lane != null
                    && (
                        lane == leEvent.LosingLane
                        || lane == leEvent.GainingLane
                    );
            
            default:
                return false;
        }
    }
}