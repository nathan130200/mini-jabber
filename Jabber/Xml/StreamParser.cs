using Expat.EventArgs;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Expat;

public class StreamParser : IDisposable
{
    private Parser _parser;
    private volatile bool _disposed;
    private XElement currentElement;

    public StreamParser()
    {
        _parser = new Parser();
        _parser.OnElementStart += OnElementStart;
        _parser.OnElementEnd += OnElementEnd;
        _parser.OnComment += OnComment;
        _parser.OnCdata += OnCdata;
        _parser.OnText += OnText;
    }

    public event Action<XElement> OnStreamStart;
    public event Action<XElement> OnStreamElement;
    public event Action<Exception> OnError;
    public event Action OnStreamEnd;

    public void Update(byte[] buffer, int? length = default)
    {
        ThrowIfDisposed();

        try
        {
            _parser.Update(buffer, length.GetValueOrDefault(buffer.Length));
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
    }

    void OnElementStart(Parser parser, ElementStartEventArgs e)
    {
        try
        {
            var mapper = new AttributeMapper(e.Attributes);
            var ofs = e.TagName.IndexOf(':');
            bool hasPrefix = ofs != -1;

            XName name;

            if (!hasPrefix)
                name = mapper.GetDefaultNamespace() + e.TagName;
            else
            {
                var ns = mapper.GetNamespaceOfPrefix(e.TagName[0..ofs]);
                name = ns + e.TagName[(ofs + 1)..];
            }

            var element = new XElement(name);

            foreach (var attr in mapper.GetAttributes())
                element.Add(attr);

            if (hasPrefix)
                element.Add(new XAttribute("xmlns", mapper.GetDefaultNamespace()));

            if (element.Name.Namespace != null && element.GetPrefixOfNamespace(element.Name.Namespace) == "stream") // stream:stream
            {
                if (element.Name.LocalName == "stream")
                    OnStreamStart?.Invoke(element);

                return;
            }

            currentElement?.Add(element);
            currentElement = element;
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
    }

    void OnElementEnd(Parser parser, ElementEventArgs e)
    {
        try
        {
            if (e.TagName == "stream:stream")
                OnStreamEnd?.Invoke();
            else
            {
                var parent = currentElement.Parent;

                if (parent == null)
                    OnStreamElement?.Invoke(currentElement);

                currentElement = parent;
            }
        }
        catch (Exception ex)
        {
            OnError?.Invoke(ex);
        }
    }

    void OnComment(Parser _, TextEventArgs e)
        => currentElement?.Add(new XComment(e.Value));

    void OnCdata(Parser _, TextEventArgs e)
        => currentElement.Add(new XCData(e.Value));

    void OnText(Parser _, TextEventArgs e)
        => currentElement.Add(new XText(e.Value));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }

    public void Dispose()
    {
        ThrowIfDisposed();
        _disposed = true;

        if (_parser != null)
        {
            _parser.Dispose();
            _parser = null;
        }
    }
}
