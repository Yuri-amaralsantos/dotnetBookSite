using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using bookApi.Data;
using bookApi.Models;
using bookApi.DTOs;


[ApiController]
[Route("Api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly HttpClient _httpClient = new();

    [HttpGet("{googleBookId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetBook(string googleBookId)
    {
        using var _httpClient = new HttpClient();
        var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes/{googleBookId}");

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        
        return Content(json, "application/json");
    }


    [HttpGet("search")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchBooks([FromQuery] string q)
    {
        var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes?q={q}");

        if (!response.IsSuccessStatusCode)
            return NotFound();

        var json = await response.Content.ReadAsStringAsync();
        return Content(json, "application/json");
    }
}
