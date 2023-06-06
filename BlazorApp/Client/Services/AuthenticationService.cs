using System.Net.Http.Headers;
using System.Net.Http.Json;
using BlazorApp.Application.RequestModels;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp.Client.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;

    public AuthenticationService(HttpClient httpClient, AuthenticationStateProvider authStateProvider,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _localStorage = localStorage;
    }

    public async Task<string> Login(LoginRequest loginRequest)
    {
        var loginResult = await _httpClient.PostAsJsonAsync("api/Auth/Login", loginRequest);

        return await SetTokenToStorage(loginResult);
    }

    private async Task<string> SetTokenToStorage(HttpResponseMessage loginResult)
    {
        if (!loginResult.IsSuccessStatusCode)
            return string.Empty;

        var token = await loginResult.Content.ReadAsStringAsync();

        await _localStorage.SetItemAsync("accessToken", token);
        ((AuthProvider.AuthProvider) _authStateProvider).NotifyUserAuthentication(token);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        return token;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("accessToken");
        ((AuthProvider.AuthProvider) _authStateProvider).NotifyUserLogout();
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<string> Register(RegisterRequest registerRequest)
    {
        var loginResult = await _httpClient.PostAsJsonAsync("api/Auth/Register", registerRequest);
        return await SetTokenToStorage(loginResult);
    }
}