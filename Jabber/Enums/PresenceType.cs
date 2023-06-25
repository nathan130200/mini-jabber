using Jabber.Attributes;

namespace Jabber.Enums;

[BetterEnum]
public enum PresenceType
{
    /// <summary>
    /// Signals that the relevant entity is available for communication.
    /// </summary>
    Available,

    /// <summary>
    /// An error has occurred regarding processing of a previously sent presence stanza.
    /// </summary>
    [XmppMember("error")]
    Error,

    /// <summary>
    /// A request for an entity's current presence.
    /// </summary>
    [XmppMember("probe")]
    Probe,

    /// <summary>
    /// The sender wishes to subscribe to the recipient's presence.
    /// </summary>
    [XmppMember("subscribe")]
    Subscribe,

    /// <summary>
    /// The sender has allowed the recipient to receive their presence.
    /// </summary>
    [XmppMember("subscribed")]
    Subscribed,

    /// <summary>
    /// The sender is no longer available for communication.
    /// </summary>
    [XmppMember("unavailable")]
    Unavailable,

    /// <summary>
    /// The sender is unsubscribing from the receiver's presence.
    /// </summary>
    [XmppMember("unsubscribe")]
    Unsubscribe,

    /// <summary>
    /// The subscription request has been denied or a previously granted subscription has been canceled.
    /// </summary>
    [XmppMember("unsubscribed")]
    Unsubscribed,
}
