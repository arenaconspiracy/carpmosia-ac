using Robust.Shared.Serialization;
using Robust.Shared.Timing;

namespace Content.Shared._Carpmosia.CartridgeLoader.Cartridges;

[Serializable, NetSerializable]
public sealed class EmergencyUiState : BoundUserInterfaceState
{
    public TimeSpan NextMessage;
    public string LastIdName;
    public string LastLocationName;

    public EmergencyUiState(TimeSpan nextMessage, string lastIdName, string lastLocationName)
    {
        NextMessage = nextMessage;
        LastIdName = lastIdName;
        LastLocationName = lastLocationName;
    }
}
