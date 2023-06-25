using Jabber.Attributes;

namespace Jabber.Enums;

[BetterEnum]
public enum IqType
{
    /// <summary>
    /// Requests data in a query.
    /// </summary>
    [XmppMember("get")]
    Get,

    /// <summary>
    /// Sends or assigns data in a query.
    /// </summary>
    [XmppMember("set")]
    Set,

    /// <summary>
    /// Indicates that the query was successful.
    /// </summary>
    [XmppMember("result")]
    Result,

    /// <summary>
    /// Indicates that the query returned an error.
    /// </summary>
    [XmppMember("error")]
    Error,
}