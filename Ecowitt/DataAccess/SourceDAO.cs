using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Ecowitt.Models.Source;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TwoMQTT.Interfaces;

namespace Ecowitt.DataAccess;

public interface ISourceDAO : IPushingSourceDAO<Response>
{
}

/// <summary>
/// Untested, subject to change.
/// </summary>
public class SourceDAO : IPushingSourceDAO<Response>, ISourceDAO
{
    public SourceDAO(ILogger<SourceDAO> logger, UInt16 port)
    {
        this.Logger = logger;
        this.Port = port;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async IAsyncEnumerable<Response?> ReceiveAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var url = $"http://*:{this.Port}/";
        this.Logger.LogDebug($"Starting http server at {url}");
        using var listener = new HttpListener();
        listener.Prefixes.Add(url);
        listener.Start();

        while (!cancellationToken.IsCancellationRequested)
        {
            Response? res = null;

            try
            {
                var context = await listener.GetContextAsync();
                var json = await ParseBodyAsync(context.Request, cancellationToken) ?? string.Empty;
                var des = JsonConvert.DeserializeObject<Response?>(json);
                var bytes = Encoding.UTF8.GetBytes("OK\n");
                await context.Response.OutputStream.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
                context.Response.OutputStream.Close();

                res = des;
            }
            catch (Exception e)
            {
                this.Logger.LogError("Unable to receive context; {exception}", e);
            }

            yield return res;
        }

        this.Logger.LogDebug("Stopping http server");
        listener.Stop();
    }

    /// <summary>
    /// The logger used internally.
    /// </summary>
    protected readonly ILogger<SourceDAO> Logger;

    /// <summary>
    /// The Port to receive reqs.
    /// </summary>
    protected readonly UInt16 Port;

    private async Task<string?> ParseBodyAsync(HttpListenerRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
            var resp = await reader.ReadToEndAsync();
            this.Logger.LogDebug("Received payload: {payload}", resp);
            var dict = HttpUtility.ParseQueryString(resp);
            var json = dict.HasKeys() ? JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v])) : null;
            return json;
        }
        catch (Exception e)
        {
            this.Logger.LogWarning("Unable to parse body of HTTP Request; {exception}", e);
            return null;
        }
    }
}
