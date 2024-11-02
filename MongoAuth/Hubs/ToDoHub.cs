using Microsoft.AspNetCore.SignalR;
using MongoAuth.Shared.Models;
using MongoAuth.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MongoAuth.Hubs
{
    public class ToDoHub : Hub
    {
        private readonly MongoDBService _mongoToDoService;
        //private IConfiguration configuration;

        public ToDoHub(MongoDBService mongoToDoService)
        {
            _mongoToDoService = mongoToDoService;
        }

        public async Task<List<ToDo>> ListPending()
        {
            return await _mongoToDoService.ReadPending();
        }

        public async Task<List<ToDo>> ListCompleted()
        {
            return await _mongoToDoService.ReadCompleted();
        }

        public async Task CreateToDo(string Title, DateTime? TaskAddedDate)
        {
            ToDo task = new ToDo()
            {
                Title = Title,
                TaskAddedDate = TaskAddedDate
            };
            await _mongoToDoService.CreateToDo(task);
            await Clients.All.SendAsync("Created", Title);
        }

        public async Task CompleteToDo(ToDo task)
        {
            await _mongoToDoService.CompleteToDo(task);
            await Clients.All.SendAsync("Completed", task.Title);
        }

        public async Task RemoveToDo(ToDo task)
        {
            await _mongoToDoService.RemoveToDo(task);
            await Clients.All.SendAsync("Removed", task.Title);
        }
    }
}
