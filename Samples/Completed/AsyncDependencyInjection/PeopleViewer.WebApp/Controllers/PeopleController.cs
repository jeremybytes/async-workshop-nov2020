using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;

namespace PeopleViewer.WebApp.Controllers
{
    public class PeopleController : Controller
    {
        IPersonReader reader;

        public PeopleController(IPersonReader reader)
        {
            this.reader = reader;
        }

        public Task<ViewResult> WithTask()
        {
            ViewData["Title"] = "Using Task";
            ViewData["RequestStart"] = DateTime.Now;

            Task<IReadOnlyCollection<Person>> peopleTask = reader.GetPeopleAsync();

            Task<ViewResult> resultTask = peopleTask.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    var errors = task.Exception!.Flatten().InnerExceptions;
                    return View("Error", errors);
                }

                IReadOnlyCollection<Person> people = task.Result;
                ViewData["RequestEnd"] = DateTime.Now;
                return View("Index", people);
            });

            return resultTask;
        }

        public async Task<ViewResult> WithAwait()
        {
            ViewData["Title"] = "Using async/await";
            ViewData["RequestStart"] = DateTime.Now;
            try
            {
                var people = await reader.GetPeopleAsync();
                return View("Index", people);
            }
            catch (Exception ex)
            {
                var errors = new List<Exception>() { ex };
                return View("Error", errors);
            }
            finally
            {
                ViewData["RequestEnd"] = DateTime.Now;
            }
        }

        public async Task<ViewResult> GetPerson(int id)
        {
            ViewData["Title"] = "Single Person";
            ViewData["RequestStart"] = DateTime.Now;
            try
            {
                var person = await reader.GetPersonAsync(id);
                if (person == null || person?.Id == 0)
                {
                    throw new KeyNotFoundException($"Person ID: {id} not found");
                }
                var people = new List<Person>();
                people.Add(person);
                return View("Index", people);
            }
            catch (Exception ex)
            {
                var errors = new List<Exception>() { ex };
                return View("Error", errors);
            }
            finally
            {
                ViewData["RequestEnd"] = DateTime.Now;
            }
        }
    }
}
