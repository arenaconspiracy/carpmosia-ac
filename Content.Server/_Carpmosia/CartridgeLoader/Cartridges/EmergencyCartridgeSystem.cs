using Content.Shared.Access.Components;
using Content.Shared.CartridgeLoader;
using Content.Shared.Database;
using Content.Server.Administration.Logs;
using Content.Server.Pinpointer;
using Content.Server.Radio.EntitySystems;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Containers;
using Robust.Shared.Timing;
using Robust.Shared.Utility;

namespace Content.Server._Carpmosia.CartridgeLoader.Cartridges;

/// <summary>
/// Emergency "SOS" message cartridge broadcasting a plea for help on radio
/// </summary>
public sealed partial class EmergencyCartridgeSystem : EntitySystem
{
    [Dependency] private readonly IAdminLogManager _adminLogger = default!;
    [Dependency] private readonly SharedAudioSystem _audio = default!;
    [Dependency] private readonly SharedContainerSystem _container = default!;
    [Dependency] private readonly NavMapSystem _navMap = default!;
    [Dependency] private readonly RadioSystem _radio = default!;
    [Dependency] private readonly IGameTiming _timing = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<EmergencyCartridgeComponent, CartridgeActivatedEvent>(OnActivated);
    }

    private void OnActivated(Entity<EmergencyCartridgeComponent> ent, ref CartridgeActivatedEvent args)
    {
        // cooldown logic
        var curTime = _timing.CurTime;

        if (curTime < ent.Comp.NextMessage)
            return;

        ent.Comp.NextMessage = curTime + ent.Comp.MessageCooldown;

        // get the ID container
        if (!_container.TryGetContainer(args.Loader, ent.Comp.IdContainer, out var idContainer))
            return;

        // play the message sound
        _audio.PlayPvs(ent.Comp.MessageSound, ent);

        // get the location for messages
        var xform = Transform(ent);
        var location = FormattedMessage.RemoveMarkupOrThrow(
            _navMap.GetNearestBeaconString((ent, xform)));

        // log the action
        _adminLogger.Add(LogType.PdaInteract, LogImpact.Low, // TODO: this stupid fucking event doesn't let you get the user
            $"SAMPLE TEXT broadcast an emergency message on {ent.Comp.MessageChannel} using {args.Loader}");

        // empty message if there is no ID
        if (idContainer.Count == 0)
        {
            _radio.SendRadioMessage(ent,
                Loc.GetString("emergency-message-noid", ("location", location)),
                ent.Comp.MessageChannel, ent);
            return;
        }

        // get the ID name
        string name = default!;
        foreach (var idCard in idContainer.ContainedEntities)
        {
            if (!TryComp<IdCardComponent>(idCard, out var idCardComp))
                return;

            Log.Info($"{idCard}, {idCardComp}, {idCardComp.FullName}");

            // need to specify a default because FullName is nullable
            name = idCardComp.FullName ?? "An unknown caller";
        }

        // filled message using the ID name
        _radio.SendRadioMessage(ent,
            Loc.GetString("emergency-message", ("name", name), ("location", location)),
            ent.Comp.MessageChannel, ent);
    }
}
