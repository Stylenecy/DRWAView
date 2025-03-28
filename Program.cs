using Microsoft.AspNetCore.Mvc;
using SimpleRESTApi.Data;
using SimpleRESTApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Dependency injection 
builder.Services.AddScoped<IInstructor, InstructorADO>();
builder.Services.AddSingleton<Icategory, CategoryADO>();
builder.Services.AddSingleton<ICourse, CourseADO>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Categories API
app.MapGet("api/v1/categories", (Icategory categoryData) =>
{
    var categories = categoryData.GetCategories();
    return Results.Ok(categories);
});

app.MapGet("api/v1/categories/{id}", (Icategory categoryData, int id) =>
{
    var category = categoryData.GetCategoryById(id);
    return category is not null ? Results.Ok(category) : Results.NotFound();
});

app.MapPost("api/v1/categories", (Icategory categoryData, Category category) =>
{
    var newCategory = categoryData.AddCategory(category);
    return Results.Created($"/api/v1/categories/{newCategory.CategoryId}", newCategory);
});

app.MapPut("api/v1/categories", (Icategory categoryData, Category category) =>
{
    var updatedCategory = categoryData.UpdateCategory(category);
    return Results.Ok(updatedCategory);
});

app.MapDelete("api/v1/categories/{id}", (Icategory categoryData, int id) =>
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

app.MapGet("api/v1/courses", (ICourse courseData) =>
{
    var courses = courseData.GetCourses();
    return courses;
});

app.MapGet("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    var course = courseData.GetCourseById(id);
    return course is not null ? Results.Ok(course) : Results.NotFound();
});

app.MapPost("api/v1/courses", (ICourse courseData, Course course) =>
{
    var newCourse = courseData.AddCourse(course);
    return newCourse;
});

app.MapPut("api/v1/courses", (ICourse courseData, Course course) =>
{
    var updatedCourse = courseData.UpdateCourse(course);
    return updatedCourse;
});

app.MapDelete("api/v1/courses/{id}", (ICourse courseData, int id) =>
{
    courseData.DeleteCourse(id);
    return Results.NoContent();
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}