using System.Linq;
using System.Xml.Linq;

namespace Expat;

internal sealed class AttributeMapper
{
    readonly Dictionary<string, XNamespace> _namespaces;
    readonly Dictionary<string, XAttribute> _attributes;

    public AttributeMapper(IEnumerable<KeyValuePair<string, string>> attributes)
    {
        _namespaces = new Dictionary<string, XNamespace>();
        _attributes = new Dictionary<string, XAttribute>();

        foreach (var (key, value) in attributes.Where(x => x.Key.Contains("xmlns")))
        {
            var ofs = key.IndexOf(':');

            if (ofs == -1)
                _namespaces[string.Empty] = value;
            else
                _namespaces[key[(ofs + 1)..]] = value;
        }

        foreach (var (key, value) in attributes)
        {
            var ofs = key.IndexOf(':');

            if (ofs == -1)
            {
                // TODO: Test this again to ensure it will produce valid XML objects!
                // to be honest i don't remember why i disabled this?
                // maybe it's because is confliting with default XMLNS declaration in XElements
                // well, i will test it later :P at least in my internal tests its worked fine.

                if (key == "xmlns")
                    continue;

                _attributes[key] = new XAttribute(key, value);
            }
            else
            {
                var prefix = key[0..ofs];
                var name = key[(ofs + 1)..];

                if (prefix == "xmlns")
                    _attributes[key] = new XAttribute(XNamespace.Xmlns + name, value);
                else if (prefix == "xml")
                    _attributes[key] = new XAttribute(XNamespace.Xml + name, value);
                else
                    _attributes[key] = new XAttribute(GetNamespaceOfPrefix(prefix) + name, value);
            }
        }
    }

    public IEnumerable<XNamespace> GetNamespaces()
        => _namespaces.Values;

    public IEnumerable<XAttribute> GetAttributes()
        => _attributes.Values;

    public XNamespace GetNamespaceOfPrefix(string prefix)
    {
        if (!_namespaces.TryGetValue(prefix, out var ns))
            ns = XNamespace.None;

        return ns;
    }

    public string GetPrefixOfNamespace(string uri)
        => _namespaces.FirstOrDefault(x => x.Value == uri).Key;

    public XNamespace GetDefaultNamespace()
    {
        if (!_namespaces.TryGetValue(string.Empty, out var ns))
            ns = XNamespace.None;

        return ns;
    }
}