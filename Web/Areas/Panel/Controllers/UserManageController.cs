using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Dtos;
using Data.Interfaces;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Areas.Panel.Controllers
{
    [Area("Panel")]
    public class UserManageController : Controller
    {
        private readonly UserManager<AppUser> _userManage;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UserManageController(UserManager<AppUser> userManage, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManage = userManage;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var getUsers = await _userManage.Users.ToListAsync();
             //   var roles = _roleManager.Roles.ToList();
                foreach (var user in getUsers)
                        if(await _userManage.IsInRoleAsync(user, "Admin"))
                            user.Roles = "مدیر";
                var query = _mapper.Map<List<AppUser>, List<UserListDto>>(getUsers);
                return Ok(query);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
