﻿using System.Net.Http.Headers;
using System.Text.Json;

namespace GreekShoping.Web.Utils;

public static class HttpClientExtensions
{
    private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
    public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) throw
                 new ApplicationException(
                     $"Something went wrong calling the API: " +
                     $"{response.ReasonPhrase}");

        var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return JsonSerializer.Deserialize<T>(dataAsString,
            new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true });
    }

    public static Task<HttpResponseMessage> PostAsJson<T>(
        this HttpClient httpClient,
        string url,
        T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = contentType;
        return httpClient.PostAsync(url, content);
    }

    public static Task<HttpResponseMessage> PutAsJson<T>(
        this HttpClient httpClient,
        string url,
        T data)
    {
        var dataAsString = JsonSerializer.Serialize(data);
        var content = new StringContent(dataAsString);
        content.Headers.ContentType = contentType;
        return httpClient.PutAsync(url, content);
    }
}

//public static class HttpClientExtensions
//{
//    private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");
//    public static async Task<T> ReadContentAs<T>(
//        this HttpResponseMessage response)
//    {
//        if (!response.IsSuccessStatusCode) throw
//                new ApplicationException(
//                    $"Something went wrong calling the API: " +
//                    $"{response.ReasonPhrase}");
//        var dataAsSring = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
//        return JsonSerializer.Deserialize<T>(dataAsSring,
//            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
//    }

//    public static Task<HttpRequestMessage> PostAsJson<T>(
//        this HttpClient httpClient,
//        string url,
//        T data)
//    {
//        var dataAsSring = JsonSerializer.Serialize(data);
//        var content = new StringContent(dataAsSring);
//        content.Headers.ContentType = contentType;
//        return httpClient.PostAsync(url, content);
//    }

//    public static Task<HttpRequestMessage> PutAsJson<T>(
//        this HttpClient httpClient,
//        string url,
//        T data)
//    {
//        var dataAsSring = JsonSerializer.Serialize(data);
//        var content = new StringContent(dataAsSring);
//        content.Headers.ContentType = contentType;
//        return httpClient.PutAsync(url, content);
//    }
//}