using ContactApp.Data.Services.Interfaces;
using ContactApp.Domain.DTOs;
using ContactApp.Domain.Entities;
using ContactApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ContactApp.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContactService _contactService;

        public ContactsController(
            UserManager<ApplicationUser> userManager,
            IContactService contactService
        )
        {
            _userManager = userManager;
            _contactService = contactService;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(string search)
        {
            var contactVM = new ContactViewModel();

            int.TryParse(_userManager.GetUserId(User), out int userId);
            var contactDTOs = await _contactService.Get(userId, search);
            if (contactDTOs.IsNullOrEmpty())
            {
                return View(contactVM);
            }

            contactVM.Contacts = contactDTOs;
            return View(contactVM);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int.TryParse(_userManager.GetUserId(User), out int userId);
            var contactDTO = await _contactService.GetById(id, userId);
            if (contactDTO == null)
            {
                return View(contactDTO);
            }

            return View(contactDTO);
        }

        #region Create
        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactDTO contactDTO)
        {
            if (contactDTO == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                int.TryParse(_userManager.GetUserId(User), out int userId);
                await _contactService.AddAsync(contactDTO, userId);

                return RedirectToAction(nameof(Index));
            }

            return View(contactDTO);
        }
        #endregion

        #region Edit
        //// GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int.TryParse(_userManager.GetUserId(User), out int userId);
            var contact = await _contactService.GetById(id, userId);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        //// POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ContactDTO contactDTO)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(_userManager.GetUserId(User), out int userId);
                await _contactService.UpdateAsync(contactDTO, userId);

                return RedirectToAction(nameof(Index));
            }

            return View(contactDTO);
        }
        #endregion

        #region Delete
        //// GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int.TryParse(_userManager.GetUserId(User), out int userId);
            var contact = await _contactService.GetById(id, userId);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        //// POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int.TryParse(_userManager.GetUserId(User), out int userId);
            await _contactService.DeleteAsync(id, userId);

            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}
