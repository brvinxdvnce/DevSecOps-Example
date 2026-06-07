namespace DevSecOps_Example;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

[ApiController]
[Route("api/[controller]")]
public class VulnerableController : ControllerBase
{
    // SAST: опасный вызов Process.Start с пользовательским вводом
    /*[HttpGet("exec")]
    public IActionResult RunCommand(string cmd)
    {
        var process = Process.Start("cmd.exe", "/c " + cmd);  // Semgrep найдёт это
        return Ok($"Executed: {cmd}");
    }*/

    // SAST + DAST: XSS через возврат сырого HTML
    [HttpGet("greet")]
    public IActionResult Greet(string name)
    {
        var html = $"<html><body><h1>Hello, {name}</h1></body></html>"; // отражаемый XSS
        return Content(html, "text/html");
    }

    // DAST: path traversal (чтение любых файлов)
    [HttpGet("readfile")]
    public IActionResult ReadFile(string filename)
    {
        // Не санитизирует путь — можно прочитать, например, /etc/passwd или appsettings.json
        var content = System.IO.File.ReadAllText(filename);
        return Ok(content);
    }

    // SAST: вставка в заголовок (CRLF injection)
    [HttpGet("redirect")]
    public IActionResult Redirect(string url)
    {
        // Открытый редирект + возможность вставить заголовки через %0d%0a
        Response.Headers.Add("Location", url);
        return StatusCode(302);
    }

    // SAST: SQL-инъекция без реальной БД (эмуляция)
    [HttpGet("search")]
    public IActionResult Search(string input)
    {
        // Хотя БД нет, Semgrep всё равно может сработать на конкатенации в запросе
        var query = "SELECT * FROM Users WHERE Name = '" + input + "'";
        return Ok($"Executing: {query}");
    }
}