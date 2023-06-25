using System.Text;

namespace Jabber;

public record class Jid
{
    public string User { get; init; }
    public string Server { get; init; }
    public string Resource { get; init; }
    private readonly string _cachedJID;

    public Jid(string jid)
    {
        var at = jid.IndexOf('@');

        if (at != -1)
        {
            User = jid[0..at];
            jid = jid[(at + 1)..];
        }

        var slash = jid.IndexOf('/');

        if (slash == -1)
            Server = jid;
        else
        {
            Server = jid[0..slash];
            Resource = jid[(slash + 1)..];
        }

        BuildJid(User, Server, Resource, out _cachedJID);
    }

    public Jid(string user, string server, string resource)
    {
        User = user;
        Server = server;
        Resource = resource;
        BuildJid(User, Server, Resource, out _cachedJID);
    }

    public override string ToString()
         => _cachedJID;

    public static implicit operator Jid(string str) => new(str);
    public static implicit operator string(Jid jid) => jid.ToString();

    static void BuildJid(string user, string server, string resource, out string result)
    {
        var sb = new StringBuilder();

        if (!string.IsNullOrEmpty(user))
            sb.Append(user).Append('@');

        if (!string.IsNullOrEmpty(server))
            sb.Append(server);

        if (!string.IsNullOrEmpty(resource))
            sb.Append('/').Append(resource);

        result = sb.ToString();
    }
}