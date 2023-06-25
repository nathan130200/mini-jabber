using Jabber.Attributes;

namespace Jabber.Enums;

[BetterEnum]
public enum StreamErrorCondition
{
    /// <summary>
    /// The entity has sent XML that cannot be processed.
    /// </summary>
    [XmppMember("bad-format")]
    BadFormat,

    /// <summary>
    /// The entity has sent a namespace prefix that is unsupported, or has sent no namespace prefix on an element that needs such a prefix.
    /// </summary>
    [XmppMember("bad-namespace-prefix")]
    BadNamespacePrefix,

    /// <summary>
    /// The server either is closing the existing stream for this entity because a new stream has been initiated that conflicts with the existing stream, or is refusing a new stream for this entity because allowing the new stream would conflict with an existing stream.
    /// </summary>
    [XmppMember("conflict")]
    Conflict,

    /// <summary>
    /// One party is closing the stream because it has reason to believe that the other party has permanently lost the ability to communicate over the stream.
    /// </summary>
    [XmppMember("connection-timeout")]
    ConnectionTimeout,

    /// <summary>
    /// The value of the <c>'to'</c> attribute provided in the initial stream header corresponds to an FQDN that is no longer serviced by the receiving entity.
    /// </summary>
    [XmppMember("host-gone")]
    HostGone,

    /// <summary>
    /// The value of the <c>'to'</c> attribute provided in the initial stream header does not correspond to an FQDN that is serviced by the receiving entity.
    /// </summary>
    [XmppMember("host-unknown")]
    HostUnknown,

    /// <summary>
    /// A stanza sent between two servers lacks a <c>'to'</c> or <c>'from'</c> attribute, the <c>'from'</c> or <c>'to'</c> attribute has no value, or the value violates the rules for XMPP addresses.
    /// </summary>
    [XmppMember("improper-addressing")]
    ImproperAddressing,

    /// <summary>
    /// The server has experienced a misconfiguration or other internal error that prevents it from servicing the stream.
    /// </summary>
    [XmppMember("internal-server-error")]
    InternalServerError,

    /// <summary>
    /// The data provided in a <c>'from'</c> attribute does not match an authorized JID or validated domain as negotiated between two servers using SASL or Server Dialback, or between a client and a server via SASL authentication and resource binding.
    /// </summary>
    [XmppMember("invalid-from")]
    InvalidFrom,

    /// <summary>
    /// The stream namespace name is something other than <c>http://etherx.jabber.org/streams</c>
    /// </summary>
    [XmppMember("invalid-namespace")]
    InvalidNamespace,

    /// <summary>
    /// The entity has sent invalid XML over the stream to a server that performs validation.
    /// </summary>
    [XmppMember("invalid-xml")]
    InvalidXml,

    /// <summary>
    /// The entity has attempted to send XML stanzas or other outbound data before the stream has been authenticated, or otherwise is not authorized to perform an action related to stream negotiation; the receiving entity MUST NOT process the offending data before sending the stream error.
    /// </summary>
    [XmppMember("not-authorized")]
    NotAuthorized,

    /// <summary>
    /// The initiating entity has sent XML that violates the well-formedness rules.
    /// </summary>
    [XmppMember("not-well-formed")]
    NotWellFormed,

    /// <summary>
    /// The entity has violated some local service policy (e.g., a stanza exceeds a configured size limit).
    /// </summary>
    [XmppMember("policy-violation")]
    PolicyViolation,

    /// <summary>
    /// The server is unable to properly connect to a remote entity that is needed for authentication or authorization.
    /// </summary>
    [XmppMember("remote-connection-failed")]
    RemoteConnectionFailed,

    /// <summary>
    /// The server is closing the stream because it has new (typically security-critical) features to offer, because the keys or certificates used to establish a secure context for the stream have expired or have been revoked during the life of the stream.
    /// </summary>
    [XmppMember("reset")]
    Reset,

    /// <summary>
    /// The server lacks the system resources necessary to service the stream.
    /// </summary>
    [XmppMember("resource-constraint")]
    ResourceConstraint,

    /// <summary>
    /// The entity has attempted to send restricted XML features such as a comment, processing instruction, DTD subset, or XML entity reference.
    /// </summary>
    [XmppMember("restricted-xml")]
    RestrictedXml,

    /// <summary>
    /// The server will not provide service to the initiating entity but is redirecting traffic to another host under the administrative control of the same service provider.
    /// </summary>
    [XmppMember("see-other-host")]
    SeeOtherHost,

    /// <summary>
    /// The server is being shut down and all active streams are being closed.
    /// </summary>
    [XmppMember("system-shutdown")]
    SystemShutdown,

    /// <summary>
    /// The error condition is not one of those defined by the other conditions in this list; this error condition <b>SHOULD NOT</b> be used except in conjunction with an application-specific condition.
    /// </summary>
    [XmppMember("undefined-condition")]
    UndefinedCondition,

    /// <summary>
    /// The initiating entity has encoded the stream in an encoding that is not supported by the server.
    /// </summary>
    [XmppMember("unsupported-encoding")]
    UnsupportedEncoding,

    /// <summary>
    /// The receiving entity has advertised a mandatory-to-negotiate stream feature that the initiating entity does not support, and has offered no other mandatory-to-negotiate feature alongside the unsupported feature.
    /// </summary>
    [XmppMember("unsupported-feature")]
    UnsupportedFeature,

    /// <summary>
    /// The initiating entity has sent a first-level child of the stream that is not supported by the server, either because the receiving entity does not understand the namespace or because the receiving entity does not understand the element name for the applicable namespace (which might be the content namespace declared as the default namespace).
    /// </summary>
    [XmppMember("unsupported-stanza-type")]
    UnsupportedStanzaType,

    /// <summary>
    /// The <c>'version'</c> attribute provided by the initiating entity in the stream header specifies a version of XMPP that is not supported by the server.
    /// </summary>
    [XmppMember("unsupported-version")]
    UnsupportedVersion
}
