using Jabber.Attributes;

namespace Jabber.Enums;

[BetterEnum]
public enum StanzaType
{
    [XmppMember("stream")]
    Stream,

    [XmppMember("iq")]
    Iq,

    [XmppMember("message")]
    Message,

    [XmppMember("presence")]
    Presence,
}
