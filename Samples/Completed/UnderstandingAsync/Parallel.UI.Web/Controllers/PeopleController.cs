using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskAwait.Library;
using TaskAwait.Shared;

namespace Parallel.UI.Web.Controllers
{
    public class PeopleController : Controller
    {
        PersonReader reader = new PersonReader();

        public async Task<ViewResult> WithTask()
        {
            ViewData["Title"] = "Using Task (parallel)";
            ViewData["RequestStart"] = DateTime.Now;

            try
            {
                List<int> ids = await reader.GetIdsAsync();
                var people = new ConcurrentQueue<Person>();
                var taskList = new List<Task>();

                foreach (int id in ids)
                {
                    Task<Person> personTask = reader.GetPersonAsync(id);
                    taskList.Add(personTask);
                    Task continuationTask = personTask.ContinueWith(task =>
                    {
                            Person person = task.Result;
                            people.Enqueue(person);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                    taskList.Add(continuationTask);
                }

                await Task.WhenAll(taskList);

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

        public async Task<ViewResult> WithAwait()
        {
            ViewData["Title"] = "Using async/await (not parallel)";
            ViewData["RequestStart"] = DateTime.Now;
            try
            {
                List<int> ids = await reader.GetIdsAsync();
                var people = new List<Person>();

                foreach(int id in ids)
                {
                    people.Add(await reader.GetPersonAsync(id));
                }

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

        public async Task<ViewResult> WithTaskPartialError()
        {
            ViewData["Title"] = "Partial Error";
            ViewData["RequestStart"] = DateTime.Now;

            try
            {
                List<int> ids = await reader.GetIdsAsync();
                var people = new ConcurrentQueue<Person>();
                var taskList = new List<Task>();

                foreach (int id in ids)
                {
                    Task<Person> personTask = reader.GetPersonAsyncWithFailures(id);
                    taskList.Add(personTask);
                    Task continuationTask = personTask.ContinueWith(task =>
                    {
                        Person person = task.Result;
                        people.Enqueue(person);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                    taskList.Add(continuationTask);
                }

                await Task.WhenAll(taskList);

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

        public async Task<ViewResult> WithTaskFullError()
        {
            ViewData["Title"] = "Full Error";
            ViewData["RequestStart"] = DateTime.Now;

            try
            {
                List<int> ids = await reader.GetIdsAsync();
                var people = new ConcurrentQueue<Person>();
                var taskList = new List<Task>();

                foreach (int id in ids)
                {
                    Task<Person> personTask = reader.GetPersonAsyncWithFailures(id);
                    taskList.Add(personTask);
                    Task continuationTask = personTask.ContinueWith(task =>
                    {
                        Person person = task.Result;
                        people.Enqueue(person);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                    taskList.Add(continuationTask);
                }

                IReadOnlyCollection<Exception> errors = null;
                await Task.WhenAll(taskList)
                    .ContinueWith(t =>
                    {
                        errors = t.Exception.Flatten().InnerExceptions;
                    }, TaskContinuationOptions.OnlyOnFaulted);

                if (errors != null)
                    return View("Error", errors);

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
