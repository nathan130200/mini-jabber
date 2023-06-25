using Expat;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace Jabber;

internal class Program
{
    static void Main(string[] args)
    {

    }
}

public class Server
{
    private Socket _socket;
    private CancellationTokenSource _cts;

    public Server()
    {
        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.Bind(new IPEndPoint(IPAddress.Any, 5222));
    }

    public void StartListen(CancellationToken token)
    {
        _socket.Listen(10);
        _cts = CancellationTokenSource.CreateLinkedTokenSource(token);
        _ = Task.Run(BeginAcceptAsync, token);
    }

    async Task BeginAcceptAsync()
    {
        while (_cts != null && !_cts.IsCancellationRequested)
        {
            await Task.Delay(16);

            try
            {
                var client = await _socket.AcceptAsync();
                _ = Task.Run(async () => await EndAcceptAsync(client));
            }
            catch (Exception ex)
            {
                Log.Verbose(ex, "accept failed");
            }
        }
    }

    async Task EndAcceptAsync(Socket client)
    {

    }
}

public class Connection
{
    record class ByteBufferEntry(byte[] Data, TaskCompletionSource Completition);

    private Socket _socket;
    private StreamParser _parser;
    private ConcurrentQueue<ByteBufferEntry> _queue;
    private CancellationTokenSource _cts;

    public string Id { get; } = Guid.NewGuid().ToString("D");
    public Jid Jid { get; private set; }
    public IPEndPoint RemoteEndPoint { get; }

    public Connection(Socket socket, CancellationToken token)
    {
        _socket = socket;
        _cts = CancellationTokenSource.CreateLinkedTokenSource(token);
        _queue = new ConcurrentQueue<ByteBufferEntry>();

        RemoteEndPoint = _socket.RemoteEndPoint as IPEndPoint;
    }
}