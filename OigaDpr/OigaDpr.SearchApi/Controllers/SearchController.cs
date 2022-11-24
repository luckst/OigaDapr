﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OigaDpr.SearchApi.Models;
using OigaDpr.SearchApi.Services;

namespace OigaDpr.SearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet()]
        public async Task<IEnumerable<User>> Search([FromQuery] string? filters)
        {
            return await _searchService.Search(filters ?? "");
        }
    }
}
