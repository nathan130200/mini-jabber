using Jabber.Enums;
using System.Linq;
using System.Xml.Linq;

namespace Jabber.Protocol;

public class Stanza
{
    public StanzaType Type { get; }
    public XElement Tag { get; }

    public Stanza(StanzaType type)
    {
        Type = type;

        if (type != StanzaType.Stream)
            Tag = new XElement(xNS.Client + type.GetXmppMember());
        else
        {
            Tag = new XElement(xNS.Stream + "stream",
                new XAttribute("xmlns", NS.Client),
                new XAttribute(XNamespace.Xmlns + "stream", NS.Stream));
        }
    }

    public static bool Match(XElement element)
    {
        foreach (var value in Enum.GetValues<StanzaType>())
        {
            if (value.GetXmppMember() == element.Name.LocalName)
                return true;
        }

        return false;
    }

    public Stanza(XElement element)
    {
        Type = (Tag = element).Name.LocalName switch
        {
            "iq" => StanzaType.Iq,
            "presence" => StanzaType.Presence,
            "message" => StanzaType.Message,
            _ => throw new ArgumentException("Unknown stanza type '" + element.Name.LocalName + "'", nameof(element))
        };
    }

    public Stanza Add(object content)
    {
        Tag.Add(content);
        return this;
    }

    public XElement GetChild(string name, string xmlns = default) =>
        Tag.Descendants().FirstOrDefault(x =>
        {
            if (string.IsNullOrEmpty(xmlns))
                return x.Name.LocalName == name;
            else
                return x.Name == (XNamespace)xmlns + name;
        });

    public XElement GetChild(XName name)
        => Tag.Descendants().FirstOrDefault(x => x.Name == name);

    public bool HasChild(string name, string xmlns = default)
        => GetChild(name, xmlns) != null;

    public bool HasChild(XName name)
        => Tag.Element(name) != null;
}
