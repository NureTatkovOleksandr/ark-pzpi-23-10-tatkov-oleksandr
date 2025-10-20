public async Task<string> DownloadAsync(string url)
{
    using var client = new HttpClient();
    return await client.GetStringAsync(url);
}
