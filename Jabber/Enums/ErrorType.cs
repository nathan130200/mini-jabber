using Jabber.Attributes;

namespace Jabber.Enums;

[BetterEnum]
public enum ErrorType
{
    /// <summary>
    /// Indicates that the error was due to authentication issues or this feature is only allowed in authenticated context.
    /// </summary>
    [XmppMember("auth")]
    Auth,

    /// <summary>
    /// Indicates that the query failed and should not attempt to request again.
    /// </summary>
    [XmppMember("cancel")]
    Cancel,

    /// <summary>
    /// Indicates that the query failed but it was not something that prevents the connection from continuing.
    /// </summary>
    [XmppMember("continue")]
    Continue,

    /// <summary>
    /// Indicates that the query failed because of an invalid parameter or the request was not correct, it can be modified and sent again.
    /// </summary>
    [XmppMember("modify")]
    Modify,

    /// <summary>
    /// Indicates that the query failed because of some resource not available at that time (for example: destination JID is not on connected server, the server cannot handle this query at this moment or that request is temporarily unavailable)
    /// </summary>
    [XmppMember("wait")]
    Wait
}
