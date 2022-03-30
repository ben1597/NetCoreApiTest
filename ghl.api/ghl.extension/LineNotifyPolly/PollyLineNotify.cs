using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace ghl.extension.LineNotifyPolly
{
    public static class PollyLineNotify
    {
      public  static  IAsyncPolicy<HttpResponseMessage> Policy(IServiceProvider provider, HttpRequestMessage message)
        {
            
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(s => s.IsSuccessStatusCode != true)
                .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(7),
                        TimeSpan.FromSeconds(9),
                    },
                    (outcome, timespan, retryCount, context) =>
                    {
                        var httpClientFactory = provider.GetService<IHttpClientFactory>();
                        var client = httpClientFactory.CreateClient("notify");
                        var requestMsg = message.Content.ReadAsStringAsync().Result;
                        var formData = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("message", $"{Environment.NewLine}API位置:{ outcome.Result.RequestMessage.RequestUri}{Environment.NewLine} 傳入參數: {requestMsg} {Environment.NewLine} HTTP狀態:{outcome.Result.ReasonPhrase} {Environment.NewLine} 重送次數:{retryCount}")
                        });

                        return client.PostAsync("notify", formData);
                    });
        }
    }
}