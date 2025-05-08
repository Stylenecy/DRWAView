using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simple_API.data;
using Simple_API.Data;
using Simple_API.models;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency injection 
builder.Services.AddScoped<IInstructor, InstructorADO>();
builder.Services.AddSingleton<ICategory, CategoryADO>();
builder.Services.AddScoped<ICourses, CourseEF>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Categories API
app.MapGet("api/v1/categories", (ICategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return Results.Ok(categories);
});

app.MapGet("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    var category = categoryData.GetCategory(id);
    return category is not null ? Results.Ok(category) : Results.NotFound();
});

app.MapPost("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var newCategory = categoryData.AddCategory(category);
    return Results.Created($"/api/v1/categories/{newCategory.CategoryId}", newCategory);
});

app.MapPut("api/v1/categories", (ICategory categoryData, Category category) =>
{
    var updatedCategory = categoryData.UpdateCategory(category);
    return Results.Ok(updatedCategory);
});

app.MapDelete("api/v1/categories/{id}", (ICategory categoryData, int id) =>
{
    categoryData.DeleteCategory(id);
    return Results.NoContent();
});

// Instructors API
app.MapGet("api/v1/instructors", (IInstructor instructorData) =>
{
    var instructors = instructorData.GetInstructors();
    return Results.Ok(instructors);
});

app.MapGet("api/v1/instructors/{id}", (IInstructor instructorData, int id) =>
{
    var instructor = instructorData.GetInstructorById(id);
    return instructor is not null ? Results.Ok(instructor) : Results.NotFound();
});

app.MapPost("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    var newInstructor = instructorData.AddInstructor(instructor);
    return Results.Created($"/api/v1/instructors/{newInstructor.InstructorId}", newInstructor);
});

app.MapPut("api/v1/instructors", (IInstructor instructorData, Instructor instructor) =>
{
    var updatedInstructor = instructorData.UpdateInstructor(instructor);
    return Results.Ok(updatedInstructor);
});

app.MapDelete("api/v1/instructors/{id}", (IInstructor instructorData, int id) =>
{
    instructorData.DeleteInstructor(id);
    return Results.NoContent();
});

app.MapGet("api/v1/courses", (ICourses course) =>
{
    return course.GetCourses();
});

app.MapGet("api/v1/courses/{id}", (ICourses course, int id) =>
{
    return course.GetCourse(id);
});

app.MapPost("api/v1/courses", (ICourses courseData, Course course) =>
{
    return courseData.AddCourse(course);
});


app.MapPut("api/v1/courses", (ICourses courseData, Course course) =>
{
    return courseData.UpdateCourse(course);
});

app.MapDelete("api/v1/courses/{id}", (ICourses courseData, int id) =>
{
    courseData.DeleteCourse(id);
    return "Course deleted successfully";
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}