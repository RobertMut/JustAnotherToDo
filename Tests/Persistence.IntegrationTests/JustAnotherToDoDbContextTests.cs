using System;
using System.Threading.Tasks;
using JustAnotherToDo.Domain.Entities;
using JustAnotherToDo.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Shouldly;

namespace Persistence.IntegrationTests
{
    public class Tests
    {
        private JustAnotherToDoDbContext _context;
        private Guid _profileId = Guid.NewGuid();
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<JustAnotherToDoDbContext>()
                .UseInMemoryDatabase("InMemory").Options;
            _context = new JustAnotherToDoDbContext(options);
            Guid CategoryId = Guid.NewGuid();
            _context.Categories.Add(new Category
            {
                Id = CategoryId,
                Name = "Test",
                Color = "#FF0000",
                ProfileId = _profileId,
            });
            _context.ToDos.Add(new ToDo
            {
                Name = "Test",
                CreationDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                CategoryId = CategoryId,
                ProfileId = _profileId,
            });
            _context.SaveChanges();
        }

        [Test]
        public async Task ShouldAddNewTodo()
        {
            var todo = new ToDo
            {
                Name = "New todo",
                CreationDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                ProfileId = _profileId
            };
            _context.ToDos.Add(todo);
            await _context.SaveChangesAsync();
            var get = await _context.ToDos.FirstOrDefaultAsync(n => n.Name == todo.Name);
            get.ShouldNotBeNull();
            get.Id.ShouldNotBe(Guid.Empty);
            get.Name.ShouldBe(todo.Name);
            get.CreationDate.ShouldBe(todo.CreationDate);
            get.EndDate.ShouldBe(todo.EndDate);
        }
        [Test]
        public async Task ShouldAddNewCategory()
        {
            var category = new Category
            {
                Name = "New category",
                Color = "#00FF00",
                ProfileId = _profileId,
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            var get = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);
            get.ShouldNotBeNull();
            get.Id.ShouldNotBe(Guid.Empty);
            get.Name.ShouldBe(category.Name);
            get.Color.ShouldBe(category.Color);
        }
        [Test]
        public async Task ShouldUpdateTodo()
        {
            var todo = await _context.ToDos.FirstOrDefaultAsync(t => t.Name == "Test");
            todo.ShouldNotBeNull();
            todo.Name = "Edited Test";
            await _context.SaveChangesAsync();
            var editedTodo = await _context.ToDos.FirstOrDefaultAsync(t => t.Name == "Edited Test");
            editedTodo.ShouldNotBeNull();
            editedTodo.Name.ShouldBe("Edited Test");
        }
        [Test]
        public async Task ShouldUpdateCategory()
        {
            var category = await _context.Categories.FirstOrDefaultAsync(t => t.Name == "Test");
            category.ShouldNotBeNull();
            category.Name = "Edited category";
            await _context.SaveChangesAsync();
            var editedCategory = await _context.Categories.FirstOrDefaultAsync(t => t.Name == "Edited category");
            editedCategory.ShouldNotBeNull();
            editedCategory.Name.ShouldBe("Edited category");
        }
    }
}