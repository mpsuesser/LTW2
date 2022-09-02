using System.Collections.Generic;

public class TargetSelectedInterfaceTransitionData : InterfaceTransitionData {
    public List<ClientEntity> Targets { get; }

    public TargetSelectedInterfaceTransitionData(List<ClientEntity> targets) {
        Targets = new List<ClientEntity>(targets);
    }
}
