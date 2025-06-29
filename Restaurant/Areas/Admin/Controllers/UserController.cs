﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restourant.Areas.Admin.Models;
using Restourant.DataContext.Entities;
using System.Threading.Tasks;
using Restaurant.Areas.Admin.Controllers;
using Restourant.Areas.Admin.Data;
using Restourant.DataContext;

namespace Restourant.Areas.Admin.Controllers
{
    public class UserController : AdminController
    {
        private readonly AppDbContext _DbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
    
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                };
                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }


        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var userCreateViewModel = new UserCreateViewModel
            {
                Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList(),
                Username = string.Empty,
                FullName = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                ConfirmPassword = string.Empty
            };

            return View(userCreateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateViewModel userCreateViewModel)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            if (!ModelState.IsValid)
            {
                userCreateViewModel.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();
                return View(userCreateViewModel);
            }

            var user = new AppUser
            {
                UserName = userCreateViewModel.Username,
                Email = userCreateViewModel.Email,
                FullName = userCreateViewModel.FullName
            };

            var result = await _userManager.CreateAsync(user, userCreateViewModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                userCreateViewModel.Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();
                return View(userCreateViewModel);
            }

            foreach (var item in userCreateViewModel.SelectedRole)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, item);
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    userCreateViewModel.Roles = roles.Select(r => new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    }).ToList();
                    return View(userCreateViewModel);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var userEditViewModel = new UserCreateViewModel
            {
                Roles = roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList(),
                Username = string.Empty,
                FullName = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                ConfirmPassword = string.Empty
            };
            return View(userEditViewModel);
        }
        public async Task<IActionResult> Details()
        {
            var user = await _DbContext.Users.ToListAsync();
            return View(user);
        }
    }
}
