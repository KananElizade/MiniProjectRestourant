
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restourant.DataContext;
using Restourant.DataContext.Entities;
using Restourant.DataContext;

namespace Restourant.ViewComponents;
public class ReservationViewComponent : ViewComponent
{
    private readonly AppDbContext _DbContext;

    public ReservationViewComponent(AppDbContext context)
    {
        _DbContext = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var tableCapacities = await _DbContext.Tables
            .ToListAsync();

        return View(tableCapacities);
    }



}
