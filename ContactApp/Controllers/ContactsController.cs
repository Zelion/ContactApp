﻿using ContactApp.Data.Services.Interfaces;
using ContactApp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ContactApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;

        public ContactsController(
            IContactService contactService
            )
        {
            _contactService = contactService;
        }

        // GET: Contacts
        public async Task<ActionResult<IEnumerable<Contact>>> Index()
        {
            var contacts = await _contactService.Get();
            if (contacts.IsNullOrEmpty())
            {
                return NotFound();
            }

            return View(contacts);
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

        //// POST: Contacts/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Age,Address,Phone,Created,LastUpdate,CreatedBy,LastUpdateBy")] Contact contact)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(contact);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        //// GET: Contacts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Contacts == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contacts.FindAsync(id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(contact);
        //}

        //// POST: Contacts/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Age,Address,Phone,Created,LastUpdate,CreatedBy,LastUpdateBy")] Contact contact)
        //{
        //    if (id != contact.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(contact);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ContactExists(contact.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(contact);
        //}

        //// GET: Contacts/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Contacts == null)
        //    {
        //        return NotFound();
        //    }

        //    var contact = await _context.Contacts
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(contact);
        //}

        //// POST: Contacts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Contacts == null)
        //    {
        //        return Problem("Entity set 'ContactAppContext.Contacts'  is null.");
        //    }
        //    var contact = await _context.Contacts.FindAsync(id);
        //    if (contact != null)
        //    {
        //        _context.Contacts.Remove(contact);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ContactExists(int id)
        //{
        //    return (_context.Contacts?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}