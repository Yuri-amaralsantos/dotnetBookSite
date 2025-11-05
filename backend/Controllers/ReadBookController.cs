using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using bookApi.Data;
using bookApi.Models;
using System.Net.Http;
using System.Text.Json;
using bookApi.DTOs;


[ApiController]
[Route("Api/[controller]")]
[Authorize]
public class ReadBooksController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly HttpClient _httpClient;

    public ReadBooksController(AppDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] CreateReadBookDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Comment) && dto.Rating == null)
        {
            return BadRequest(new { message = "Você deve fornecer pelo menos uma nota ou comentário." });
        }

        var review = new ReadBook
        {
            GoogleBookId = dto.GoogleBookId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
        };

        _context.ReadBooks.Add(review);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            review.Id,
            review.GoogleBookId,
            review.Rating,
            review.Comment,
            review.UserId
        });
    }

   [HttpGet("user")]
    public async Task<IActionResult> GetUserReviews()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        var reviews = await _context.ReadBooks
            .Include(r => r.User)
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.ReadAt)
            .ToListAsync();

        var results = new List<object>();

        foreach (var r in reviews)
        {
         
            var bookUrl = $"https://www.googleapis.com/books/v1/volumes/{r.GoogleBookId}";
            var bookResponse = await _httpClient.GetAsync(bookUrl);

            if (!bookResponse.IsSuccessStatusCode)
            {
                results.Add(new
                {
                    r.Id,
                    r.GoogleBookId,
                    r.Rating,
                    r.Comment,
                    r.ReadAt,
                    Book = (object?)null
                });
                continue;
            }

            using var contentStream = await bookResponse.Content.ReadAsStreamAsync();
            var jsonDoc = await JsonDocument.ParseAsync(contentStream);
            var volumeInfo = jsonDoc.RootElement.GetProperty("volumeInfo");

            var title = volumeInfo.TryGetProperty("title", out var titleProp) ? titleProp.GetString() : null;
            var authors = volumeInfo.TryGetProperty("authors", out var authorsProp)
                ? authorsProp.EnumerateArray().Select(a => a.GetString()).ToList()
                : new List<string>();
            var image = volumeInfo.TryGetProperty("imageLinks", out var imageProp)
                && imageProp.TryGetProperty("thumbnail", out var thumbProp)
                ? thumbProp.GetString()
                : null;

            results.Add(new
            {
                r.Id,
                r.GoogleBookId,
                r.Rating,
                r.Comment,
                r.ReadAt,
                Book = new
                {
                    Title = title,
                    Authors = authors,
                    Image = image
                }
            });
        }

        return Ok(results);
    }


    [HttpGet("book/{googleBookId}")]
    public async Task<IActionResult> GetReviews(string googleBookId)
    {
        var reviews = await _context.ReadBooks
            .Include(r => r.User)
            .Where(r => r.GoogleBookId == googleBookId)
            .Select(r => new
            {
                r.Id,
                r.GoogleBookId,
                r.Rating,
                r.Comment,
                r.UserId,
                UserName = r.User.Username
            })
            .ToListAsync();

        return Ok(reviews);
    }
}
