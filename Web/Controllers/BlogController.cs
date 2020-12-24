using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Dtos;
using Data.Interfaces;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;
        private readonly IBlogRepository _Repository;
        private readonly IMapper _mapper;

        public BlogController(IBlogRepository repository, IMapper mapper)
        {
            _Repository = repository;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var a = await _Repository.GetByIdAsync(1);
                var b = await _Repository.GetByIsActive();
                var model = _mapper.Map<List<BlogCategory>, List<BlogCategoryDto>>(await _Repository.GetByIsActive());
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
