using System.Xml.Linq;

namespace Jabber.Attributes;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class XmppMemberAttribute : Attribute
{
    public string MemberName { get; }

    public XmppMemberAttribute(string name)
        => MemberName = name;

    public static implicit operator string(XmppMemberAttribute attr)
        => attr.MemberName;
}

[AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
public class XmppNamespaceAttribute : Attribute
{
    public XNamespace Namespace { get; }
    public string NamespaceName => Namespace.NamespaceName;

    public XmppNamespaceAttribute(string ns)
        => Namespace = ns;
}
