using Robust.Shared.Serialization;
using Robust.Shared.Timing;

namespace Content.Shared.CartridgeLoader.Cartridges;

[Serializable, NetSerializable]
public sealed class EmergencyUiState : BoundUserInterfaceState
{
    public TimeSpan NextMessage;

    public EmergencyUiState(TimeSpan nextMessage)
    {
        NextMessage = nextMessage;
    }
}
