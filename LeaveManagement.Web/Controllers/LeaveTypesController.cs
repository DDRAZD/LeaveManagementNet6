using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using AutoMapper;
using LeaveManagement.Web.Contracts;
using Microsoft.AspNetCore.Authorization;
using LeaveManagement.Web.Constants;

namespace LeaveManagement.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class LeaveTypesController : Controller
    {
     //   private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepositiry;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        //constructor with dependancy injection - the program.cs has in Services already the injection defined (again, no UnityConfig in DotNet Core)
        public LeaveTypesController(IMapper mapper, ILeaveTypeRepository leaveTypeRepositiry, ILeaveAllocationRepository leaveAllocationRepository)
        {
            //_context = context;
            this.mapper = mapper;
            this.leaveTypeRepositiry = leaveTypeRepositiry;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index() //returns Task<IActionResult>
        {
       //     var leaveTypes = this.mapper.Map<List<LeaveTypeVM>>(await _context.LeaveTypes.ToListAsync()); //in sql this is "Select * From LeaveType"
            var leaveTypes = this.mapper.Map<List<LeaveTypeVM>>(await this.leaveTypeRepositiry.GetAllAsync());

            if (leaveTypes != null)
            {
                return View(leaveTypes);
            }
            else
            {
                Problem("Entity set 'ApplicationDbContext.LeaveTypes'  is null.");
                return View();                
            }


           /* return _context.LeaveTypes != null ? 
                          View(await _context.LeaveTypes.ToListAsync()) : //in sql this is "Select * From LeaveType"
                          Problem("Entity set 'ApplicationDbContext.LeaveTypes'  is null.");*/
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var leaveType = await this.leaveTypeRepositiry.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            LeaveTypeVM leaveTypeVM = mapper.Map<LeaveTypeVM>(leaveType);   

            return View(leaveTypeVM);
        }

        // GET: LeaveTypes/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create(LeaveTypeVM leaveTypeVM)
        {
            if (ModelState.IsValid)
            {
                LeaveType leavetypeToAdd = this.mapper.Map<LeaveType>(leaveTypeVM);
                leavetypeToAdd.DateCreated = DateTime.Now;
                leavetypeToAdd.DateModified = DateTime.Now;
                await  this.leaveTypeRepositiry.AddAsync(leavetypeToAdd);
             //   _context.Add(leavetypeToAdd);
              //  await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);//if model state is not valid 
        }

        // GET: LeaveTypes/Edit/5
       
        public async Task<IActionResult> Edit(int? id)
        {
            var leaveType = await this.leaveTypeRepositiry.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            LeaveTypeVM leaveTypeVM = mapper.Map<LeaveTypeVM>(leaveType);
            return View(leaveTypeVM);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Edit(int id,  LeaveTypeVM leaveTypeVM)
        {
            if (id != leaveTypeVM.Id) //this is a fail safe - the ID already exists in leaveTypeVM but if someone hijacked the session, this can be manipulated with
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try //all done in try-catch for the case someone else is also submitting at same time
                {
                    LeaveType leaveType = this.mapper.Map<LeaveType>(leaveTypeVM);
                    leaveType.DateModified = DateTime.Now;
                    await this.leaveTypeRepositiry.UpdateAsync(leaveType);
                  //  _context.Update(leaveType);
                  //  await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException) //if two people are updating at same time, that is the issue this catches
                {
                    if (await this.leaveTypeRepositiry.Exists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }

        // GET: LeaveTypes/Delete/5
       

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           
           await this.leaveTypeRepositiry.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave (int id)
        {
           
            await leaveAllocationRepository.LeaveAllocation(id);
            return RedirectToAction(nameof(Index));

        }


    }
}
