using Content.Client.UserInterface.Fragments;
using Content.Shared.CartridgeLoader;
using Content.Shared.CartridgeLoader.Cartridges;
using Robust.Client.GameObjects;
using Robust.Client.UserInterface;
using Robust.Shared.Utility;

namespace Content.Client._Carpmosia.CartridgeLoader.Cartridges;

public sealed partial class EmergencyUi : UIFragment
{
    private EmergencyUiFragment? _fragment;

    public override Control GetUIFragmentRoot()
    {
        return _fragment!;
    }

    public override void Setup(BoundUserInterface userInterface, EntityUid? fragmentOwner)
    {
        _fragment = new EmergencyUiFragment();
    }

    public override void UpdateState(BoundUserInterfaceState state)
    {/*
        if (state is not EmergencyUiState emergencyState)
            return;

        _fragment.UpdateState(emergencyState);
    */}
}
