using Content.Shared.Containers.ItemSlots;
using Content.Shared.Radio;
using Robust.Shared.Audio;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Shared._Carpmosia.CartridgeLoader.Cartridges;

/// <summary>
/// Emergency "SOS" message cartridge broadcasting a plea for help on radio
/// </summary>
[RegisterComponent]
public sealed partial class EmergencyCartridgeComponent : Component
{
    /// <summary>
    /// Cooldown until the next message
    /// </summary>
    [DataField]
    [ViewVariables(VVAccess.ReadOnly)]
    public TimeSpan NextMessage;

    /// <summary>
    /// How long the cooldown between messages should be
    /// </summary>
    [DataField]
    [ViewVariables(VVAccess.ReadWrite)]
    public TimeSpan MessageCooldown = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Sound to play when sending a message
    /// </summary>
    [DataField]
    [ViewVariables(VVAccess.ReadWrite)]
    public SoundSpecifier? MessageSound = default!;

    /// <summary>
    /// Container to check for ID card
    /// </summary>
    [DataField]
    [ViewVariables(VVAccess.ReadOnly)]
    public string IdContainer = "PDA-id";

    /// <summary>
    /// Name to use for messages
    /// </summary>
    /// <remarks>
    /// Getting this at the same time as you send the message is easy and fine
    /// but we save this to use in the app UI clientside.
    /// </remarks>
    [DataField]
    [ViewVariables(VVAccess.ReadWrite)]
    public String LastIdName = String.Empty;

    /// <summary>
    /// Sound to play when sending a message
    /// </summary>
    /// <remarks>
    /// Saving this for the same UI on client reason.
    /// Plus only the serverside system can even /get/ this.
    /// </remarks>
    [DataField]
    [ViewVariables(VVAccess.ReadWrite)]
    public String LastLocationName = String.Empty;

    /// <summary>
    /// Channel to send alert message to
    /// </summary>
    /// <remarks>
    /// TODO: This needs to be reworked into a list of channels that get listed as buttons
    /// </remarks>
    [DataField]
    [ViewVariables(VVAccess.ReadWrite)]
    public ProtoId<RadioChannelPrototype> MessageChannel = "Security";
}
