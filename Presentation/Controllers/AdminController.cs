using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Repositories;
using Presentation.Models.ViewModels;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        private readonly TicketDBRepository _ticketRepository;

        public AdminController(TicketDBRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public IActionResult ListTickets()
        {
            var tickets = _ticketRepository.GetTicket().ToList();
            var viewModelList = tickets.Select(t => new AdminTicketListViewModel
            {
                Name = t.Name,
                Surname = t.Surname,
                Row = t.Row,
                Column = t.Column,
                Cancelled = t.Cancelled,
                PassportNo = t.PassportNo,
                PricePaid = t.PricePaid


            }).ToList();

            return View(viewModelList);
        }
        
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated == false)
            {

                TempData["Error"] = "Access Denied";

                return RedirectToAction("Error", "Home");
            }


            return View();
        }
    }
}

