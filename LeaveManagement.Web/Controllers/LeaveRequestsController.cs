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
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILogger<LeaveRequestsController> logger;

        public LeaveRequestsController(ApplicationDbContext context, 
            ILeaveRequestRepository leaveRequestRepository,
            ILogger<LeaveRequestsController> logger)
        {
            _context = context;
           // this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
            this.logger = logger;
        }


       [Authorize(Roles =Roles.Administrator)]
        // GET: LeaveRequests
        public async Task<IActionResult> Index()
        {
           var model = await this.leaveRequestRepository.GetAdminLeaveRequestList();
           return View(model);
            //var applicationDbContext = _context.LeaveRequests.Include(l => l.LeaveType);
          //  return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> MyLeave()
        {
            var model = await leaveRequestRepository.GetMyLeaveDetails();
            if(model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: LeaveRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestAsync(id);
            
            return View(leaveRequest);
        }


        /// <summary>
        /// approving the request - buttons for approval and rejection are located within a form in the details view
        /// </summary>
        /// <returns></returns>
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveRequest(int id, bool Approved)
        {
            try
            {
                await leaveRequestRepository.ChangeApprovalStatus(id, Approved);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Something happened in the ApproveRequest method");//login manually
                throw;//the throw (unhandled exception that collapses the program) will be logged by the Error method in home controller
            }
            
            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelRequest(int Id)
        {
            try
            {
                await leaveRequestRepository.CancelLeaveRequest(Id);
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToAction(nameof(MyLeave));
        }

        // GET: LeaveRequests/Create
        public IActionResult Create()
        {
            var model = new LeaveRequestCreateVM();
            model.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name");
           
            
     
            return View(model);
        }

        // POST: LeaveRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // var leaveRequest = mapper.Map<LeaveRequest>(model);
                    //   await leaveRequestRepository.AddAsync(leaveRequest);

                    bool create = await leaveRequestRepository.CreateLeaveRequest(model);
                    if(create == true)
                    {
                        return RedirectToAction(nameof(MyLeave));
                    }
                    else
                    {
                        ModelState.TryAddModelError(string.Empty, "not enough days to create request");
                    }
                }
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, "an error has occored in the leave request controller as an exception");
            }
            //this is in case we need to reload - the list is not tracked in the form so need to load it
            model.LeaveTypes = new SelectList(_context.LeaveTypes, "Id", "Name", model.LeaveTypeId);
            return View(model);

        }

        // GET: LeaveRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
            return View(leaveRequest);
        }

        // POST: LeaveRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,LeaveTypeId,DateRequested,RequestComments,RequestingEmployeeId,Approved,Cancelled,Id,DateCreated,DateModified")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveRequestExists(leaveRequest.Id))
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
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Id", leaveRequest.LeaveTypeId);
            return View(leaveRequest);
        }

        // GET: LeaveRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveRequests == null)
            {
                return NotFound();
            }

            var leaveRequest = await _context.LeaveRequests
                .Include(l => l.LeaveType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        // POST: LeaveRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveRequests == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LeaveRequests'  is null.");
            }
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);
            if (leaveRequest != null)
            {
                _context.LeaveRequests.Remove(leaveRequest);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveRequestExists(int id)
        {
          return _context.LeaveRequests.Any(e => e.Id == id);
        }
    }
}
