using AutoMapper;
using ContactApp.Data.Services.Interfaces;
using ContactApp.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ContactApp.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IContactService _contactService;

        public ContactsController(
            IMapper mapper,
            IContactService contactService
        )
        {
            _mapper = mapper;
            _contactService = contactService;
        }

        // GET: Contacts
        public async Task<ActionResult<IEnumerable<ContactDTO>>> Index()
        {
            var contacts = await _contactService.Get();
            if (contacts.IsNullOrEmpty())
            {
                return NotFound();
            }

            var contactDTOs = _mapper.Map<IEnumerable<ContactDTO>>(contacts);

            return View(contactDTOs);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactService.GetById(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

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
                await _contactService.AddAsync(contactDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(contactDTO);
        }

        //// GET: Contacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactService.GetById(id);
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
                await _contactService.UpdateAsync(contactDTO);

                return RedirectToAction(nameof(Index));
            }

            return View(contactDTO);
        }

        //// GET: Contacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _contactService.GetById(id);
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
            await _contactService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
