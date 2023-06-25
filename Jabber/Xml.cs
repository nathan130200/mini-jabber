using Jabber.Enums;
using Jabber.Protocol;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Jabber;

public static class Xml
{
    public static string GetAttribute(this XElement el, XName name)
        => el.Attribute(name)?.Value;

    public static void RemoveAttribute(this XElement el, XName name)
        => el.Attribute(name)?.Remove();

    public static XElement SwitchDirection(XElement el)
    {
        var from = el.GetAttribute("from");
        var to = el.GetAttribute("to");

        el.RemoveAttribute("from");
        el.RemoveAttribute("to");

        if (!string.IsNullOrEmpty(from))
            el.Add(new XAttribute("to", from));

        if (!string.IsNullOrEmpty(to))
            el.Add(new XAttribute("from", to));

        return el;
    }

    public static XElement Error(ErrorType type = ErrorType.Cancel, int code = 8, int custom_code = 0, string text = default)
    {
        var el = new XElement(xNS.Client + "error",
            new XAttribute("type", type.GetXmppMember()),
            new XAttribute("code", code),
            new XAttribute("custom_code", custom_code));

        if (!string.IsNullOrEmpty(text))
            el.Add(new XElement(xNS.Stanzas + "text", new XText(text)));

        return el;
    }

    public static XElement Error(ErrorType type = ErrorType.Cancel, ErrorCondition condition = ErrorCondition.FeatureNotImplemented, string text = default)
    {
        var el = new XElement(xNS.Client + "error",
            new XAttribute("type", type.GetXmppMember()));

        el.Add(new XElement(xNS.Stanzas + condition.GetXmppMember()));

        if (!string.IsNullOrEmpty(text))
            el.Add(new XElement(xNS.Stanzas + "text", new XText(text)));

        return el;
    }

    public static Stanza Stream(Jid from, string id = default, string version = "1.0", string language = "en")
    {
        var stz = new Stanza(StanzaType.Stream);

        stz.Tag
            .Attr(XNamespace.Xml + "lang", language)
            .Attr("id", id ?? Guid.NewGuid().ToString("N"))
            .Attr("version", version)
            .Attr("from", from);

        return stz;
    }

    public static string StartTag(this XElement el)
    {
        var copy = new XElement(el);
        copy.DescendantNodes().Remove();
        return string.Concat(copy.ToString()
            .Replace("/>", string.Empty)
            .TrimEnd(), '>');
    }

    public static string EndTag(this XElement el)
    {
        var name = el.Name;

        var sb = new StringBuilder("</");

        if (name.Namespace != XNamespace.None)
        {
            var prefix = el.GetPrefixOfNamespace(name.Namespace);

            if (!string.IsNullOrEmpty(prefix))
                sb.Append(prefix).Append(':');
        }

        return sb.Append(name.LocalName).Append('>').ToString();
    }

    public static XElement C(this XElement parent, XName name, object attrs = default)
    {
        var el = new XElement(name);

        if (attrs is not null)
        {
            foreach (var p in attrs.GetType()
                .GetTypeInfo()
                .DeclaredProperties)
            {
                var propertyName = p.Name;

                var propertyValue = p.GetValue(attrs) switch
                {
                    IFormattable fmt => fmt.ToString(null, CultureInfo.InvariantCulture),
                    string str => str,
                    var def => def.ToString()
                };

                var attr = el.Attribute(propertyName);

                if (attr != null)
                    attr.Value = propertyValue ?? string.Empty;
                else
                {
                    attr = new XAttribute(propertyName, propertyValue);
                    el.Add(attr);
                }
            }
        }

        parent.Add(el);
        return el;
    }

    public static XElement Up(this XElement el)
    {
        if (el.Parent is null)
            return el;

        return el.Parent;
    }

    public static XElement Attr(this XElement el, string name, string value)
    {
        el.Add(new XAttribute(name, value));
        return el;
    }

    public static XElement Attr(this XElement el, XName name, string value)
    {
        el.Add(new XAttribute(name, value));
        return el;
    }

    public static XElement GetRoot(this XElement el)
    {
        while (el.Parent != null)
            el = el.Parent;

        return el;
    }
}