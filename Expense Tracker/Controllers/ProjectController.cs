using AutoMapper;
using Expense_Tracker.DataAccess.Interfaces;
using Expense_Tracker.Models;
using ExpenseTracker.Domain.Entities;
using ExpenseTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker.Controllers
{
    public class ProjectController(IProjectRepository projectRepository, IMapper mapper) : Controller
    {
        public IActionResult Index()
        {
            var projects = projectRepository.GetAll();
            var result = mapper.Map<List<ProjectDto>>(projects);
            return View(result);
        }

        public IActionResult Look(int id)
        {
            PopulateCurrencies();
            var project = projectRepository.Get(id);
            var result=mapper.Map<ProjectDto>(project); 
            return View(result);
        }

        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCurrencies();
            if (id == 0)
                return View(new ProjectDto());
            else
            {
                var project = projectRepository.Get(id);
                var result = mapper.Map<ProjectDto>(project);
                return View(result);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit([Bind("Id,Title, Description,StartDate, EndDate,TargetCurrency,Note, InvoiceNumber, InvoiceDocUrl, InvoiceAmount," +
            "InvoiceTargetCurrency, InvoiceIssueDate")] ProjectDto dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.Id == 0)
                    projectRepository.Add(new Project(dto.Title, dto.Description, dto.StartDate, dto.EndDate, dto.TargetCurrency, dto.Note, dto.InvoiceNumber, dto.InvoiceDocUrl,
                        dto.InvoiceAmount, dto.InvoiceTargetCurrency, dto.InvoiceIssueDate));
                else
                {
                    var project = projectRepository.Get(dto.Id);
                    project.Title = dto.Title;
                    project.Description = dto.Description;
                    project.StartDate = dto.StartDate;
                    project.EndDate = dto.EndDate;
                    project.TargetCurrency = dto.TargetCurrency;
                    project.Note = dto.Note;
                    project.InvoiceNumber= dto.InvoiceNumber;
                    project.InvoiceDocUrl = dto.InvoiceDocUrl;
                    project.InvoiceAmount = dto.InvoiceAmount;
                    project.InvoiceTargetCurrency = dto.InvoiceTargetCurrency;
                    project.InvoiceIssueDate = dto.InvoiceIssueDate;
                    projectRepository.Update(project);
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateCurrencies();
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var project = projectRepository.Get(id);
            if (project != null)
            {
                projectRepository.Delete(project);
            }

            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCurrencies()
        {
            ViewBag.CurrencyList = Enum.GetValues(typeof(Currency))
                                .Cast<Currency>()
                                .Select(e => new { Text = e.ToString(), Value = (int)e })
                                .ToList();
        }
    }
}
