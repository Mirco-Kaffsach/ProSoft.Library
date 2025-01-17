using System.Net;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Library.Core.Contracts;
using ProSoft.Library.Core.Models;
using ProSoft.Results;

namespace ProSoft.Library.Api.Controllers;

[Route("tags")]
[ApiController]
[Produces("application/json")]
public sealed class TagController : ControllerBase
{
    private readonly ILogger<TagController> _logger;
    private readonly ITagManager _tagManager;

    public TagController(ILogger<TagController> logger, ITagManager tagManager)
    {
        _logger = logger;
        _tagManager = tagManager;

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("TagController initialized successfully.");
        }
    }

    [HttpGet("")]
    public async Task<ApiPagedResult<Tag>> GetTagsAsync(CancellationToken cancellationToken)
    {
        var query = await _tagManager.GetTagListAsync(cancellationToken);
        
        return new ApiPagedResult<Tag>(HttpStatusCode.OK, query, 10, 1);
    }

    [HttpGet("{systemid}")]
    public async Task<ApiResult<Tag>> GetTagAsync(Guid systemId, CancellationToken cancellationToken)
    {
        var result = await _tagManager.GetTagAsync(systemId, cancellationToken);

        return new ApiResult<Tag>(HttpStatusCode.OK, result, ResultType.Success);
    }
}
